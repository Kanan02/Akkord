using Application.Constants;
using Application.Entities;
using Application.Enums;
using Application.Exceptions;
using Application.Helpers;
using Application.Interfaces.IServices;
using Application.Interfaces.IServices.Security;
using Application.Interfaces.IUoW;
using Application.Models.AppSetting;
using Application.Models.Request;
using Application.Models.Request.Base;
using Application.Models.Request.Ui;
using Application.Services.Base;
using Application.Spesifications.Base;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CrmTaskService : BaseService<CrmTask>, ICrmTaskService
    {
        private readonly IAccessValidator _accessValidator;
        private readonly ISecurityService _securityService;
        private readonly ISalePointService _salePointService;
        private readonly IConfiguration _configuration;
        private readonly AppNotification _appNotify;

        public CrmTaskService(IUoW unitOfWork,
            IAccessValidator accessValidator,
            ISecurityService securityService,
            ISalePointService salePointService,
            IConfiguration configuration) : base(unitOfWork)
        {
            _accessValidator = accessValidator;
            _securityService = securityService;
            _salePointService = salePointService;
            _configuration = configuration;
            _appNotify = AppSettingHelper.BindSetting<AppNotification>(_configuration);
        }

        protected override ISpecification<CrmTask> FilterList(BaseReq<CrmTask> request, ISpecification<CrmTask> spec)
        {
            var req = (CrmTaskReq)request;

            var value = req.Value;
            _accessValidator.SetUserIdByRole(ref value, nameof(CrmTask.SaleManagerId));

            if (req.IncludeSalePoint)
            {
                var regionInclude = $"{nameof(CrmTask.SalePoint)}.{nameof(SalePoint.Region)}";
                spec.IncludeStrings.Add(regionInclude);

                if (req.IncludePortfolios)
                {
                    var portfolioInclude = $"{nameof(CrmTask.SalePoint)}.{nameof(SalePoint.SalePointPortfolioes)}";
                    spec.IncludeStrings.Add(portfolioInclude);
                }
            }

            if (req.IncludePhoto)
            {
                var photoInclude = $"{nameof(CrmTask.Photos)}.{nameof(CrmTaskPhoto.Photo)}";
                spec.IncludeStrings.Add(photoInclude);
            }

            if (req.IncludeNotification)
            // spec.Includes.Add(x => x.Notification);
            {
                var includeNotByToUser = $"{nameof(CrmTask.Notification)}.{nameof(Notification.To)}";
                spec.IncludeStrings.Add(includeNotByToUser);
            }

            if (req.FromDt != null)
                spec.Filters.Add(x => x.VisitDt >= req.FromDt);

            if (req.ToDt != null)
                spec.Filters.Add(x => x.VisitDt <= req.ToDt);

            if (value != null)
            {
                if (value.Id != 0)
                    spec.Filters.Add(x => x.Id == value.Id);

                if (value.Status > 0)
                    spec.Filters.Add(x => x.Status == value.Status);

                if (value.SaleManagerId!=null && value.SaleManagerId!=Guid.Empty)
                    spec.Filters.Add(x => x.SaleManagerId == value.SaleManagerId);
            }

            return base.FilterList(request, spec);
        }

        public override async Task<CrmTask> Save(CrmTask entity)
        {
            entity.ValidateSave();
            await CheckManagerSalePoint(entity);

            if (entity.Id == 0)
            {
                await SetSaveDefaults(entity);
                _unitOfWork.Repository<CrmTask>().Add(entity);
                await SaveUoW();
                return entity; // await base.Save(entity);
            }
            else
                return await UpdateTask(entity);

        }

        private async Task CheckManagerSalePoint(CrmTask task)
        {
            var currManagerId = _securityService.GetCurrUserId();

            if (IsAdmin())
            {
                if (task.SaleManagerId == null || task.SaleManagerId.Value == Guid.Empty)
                    throw new AkkordException("sale_manager_required_for_admin");

                await ValiadateManagerPoint(task, task.SaleManagerId.Value);
                return;
            }

            await ValiadateManagerPoint(task, currManagerId);
        }

        private async Task ValiadateManagerPoint(CrmTask task, Guid managerId)
        {
            var salePoints = await GetManagerPoints(managerId, task.SalePointId);

            if (salePoints == null || salePoints.Count() == 0)
                throw new AkkordException("invalid_sale_point_for_manager");
        }

        private async Task SetSaveDefaults(CrmTask entity)
        {

            // set saleMnagerId if user is saleManager
            //_accessValidator.SetUserIdByRole(ref entity, nameof(CrmTask.SaleManagerId));

            entity.InsertedUserId = _securityService.GetCurrUserId();
            entity.InsertedDt = DateTime.Now;
            if (IsAdmin())
            {
                SetAdminSaveDefaults(entity);
                await NotifyNewTask(entity);
            }
            else
            {
                SetManagerSaveDefaults(entity);
                _unitOfWork.Repository<CrmTask>().Add(entity);
            }


            //entity.Status = isAdmin ? CrmTaskStatus.New : CrmTaskStatus.Assigned;
        }

        private void SetAdminSaveDefaults(CrmTask entity)
        {
            if (entity.SaleManagerId == null || entity.SaleManagerId == Guid.Empty)
                throw new AkkordException("sale_manager_required_for_admin");

            entity.Status = CrmTaskStatus.New;
        }

        private void SetManagerSaveDefaults(CrmTask entity)
        {
            entity.SaleManagerId = _securityService.GetCurrUserId();
            entity.Status = CrmTaskStatus.Assigned;
        }

        private async Task<CrmTask> UpdateTask(CrmTask newTask)
        {
            var task = await _unitOfWork.Repository<CrmTask>().GetByIdAsync(newTask.Id);

            if (IsAdmin())
            {
                task.SaleManagerId = newTask.SaleManagerId;
                //task.Status = newTask.Status;
            }

            task.PurposeOfVisit = newTask.PurposeOfVisit;
            task.SalePointId = newTask.SalePointId;
            task.VisitDt = newTask.VisitDt;

            await _unitOfWork.SaveChangesAsync();
            return task;

        }

        private bool IsAdmin() => _securityService.IsHaveRole(RoleConstant.Admin);

        public async Task<CrmtaskDetailUi> GetDetail(int taskId)
        {
            var req = new CrmTaskReq
            {
                IncludePhoto = true,
                IncludePortfolios = true,
                IncludeSalePoint = true,
                Value = new CrmTask
                {
                    Id = taskId
                }
            };

            var task = (await GetTaskById(taskId)).FirstOrDefault(); ;

            if (task == null)
                throw new AkkordException("task_not_found_by_given_id");

            var detial = new CrmtaskDetailUi(task);
            return detial;
        }

        private Task<IReadOnlyList<CrmTask>> GetTaskById(int taskId)
        {
            var req = new CrmTaskReq
            {
                IncludePhoto = true,
                IncludePortfolios = true,
                IncludeSalePoint = true,
                Value = new CrmTask
                {
                    Id = taskId
                }
            };
            return GetListByFilter(req);
        }

        private Task<IReadOnlyList<SalePoint>> GetManagerPoints(Guid managerId, int salePointId)
        {
            var req = new SalePointReq
            {
                Value = new SalePoint
                {
                    Id = salePointId,
                    SaleManagerId = managerId
                }
            };

            return _salePointService.GetListByFilter(req);
        }

        public async Task<CrmtaskDetailUi> Close(CrmtaskDetailUi req)
        {
            if (!_securityService.IsHaveRole(RoleConstant.SaleManager))
                throw new AkkordException("only_sale_manager_can_close_task");


            var task = (await GetTaskById(req.Id)).FirstOrDefault();

            if (task == null)
                throw new AkkordException("task_not_found");

            if(task.Status == CrmTaskStatus.New && !IsAdmin())
                throw new AkkordException("you_must_accept_or_rejet_task_notification");

            if (task.SaleManagerId != _securityService.GetCurrUserId())
                throw new AkkordException("invlid_sale_manager_for_this_task");

            //if (task.Status == CrmTaskStatus.New)
            //    throw new AkkordException("task_was_not_assigned");

            if (task.Status == CrmTaskStatus.Close)
                throw new AkkordException("task_already_closed");

            if (task.Status == CrmTaskStatus.Cancel)
                throw new AkkordException("task_already_cancelled");

            var salePoint = task.SalePoint;

            task.VisitDt = req.VisitDt;
            //task.SalePoint.RegionId = req.RegionId.Value;
            //task.SalePoint.City = req.City;
            //task.SalePoint.CodeName = req.CodeName;
            task.PurposeOfVisit = req.PurposeOfVisit;
            task.SalePoint.GpsX = req.GpsX.Value;
            task.SalePoint.GpsY = req.GpsY.Value;
            task.SalePoint.ShopCement = req.ShopCement;

            if (req.Status == CrmTaskStatus.Close || req.Status == CrmTaskStatus.New)
            {
                if (req.Portfolios == null || req.Portfolios.Count == 0)
                {
                    if (req.SalePointStatus == SalePointStatus.Closed)
                        task.SalePoint.Status = SalePointStatus.Closed;
                    else
                        throw new AkkordException("portfolio_required");
                }

                if (req.Status == CrmTaskStatus.Close)
                {
                    if (req.CurrGpsX == null || req.CurrGpsY == null)
                        throw new AkkordException("curr_location_required");

                    var isValidLocation = CoordinateHelper.IsValidLocation((double)salePoint.GpsX, (double)salePoint.GpsY, req.CurrGpsX.Value, req.CurrGpsY.Value);

                    if (!isValidLocation)
                        throw new AkkordException("invalid_location");
                }

                //if (task.Status == CrmTaskStatus.New)
                //{
                //    var assignment = await _unitOfWork.Repository<CrmTaskAssignment>().GetByIdAsync(task.Id);
                //    assignment.Status = req.Status == CrmTaskStatus.Cancel
                //        ? TaskAssignmentStatus.Reject
                //        : TaskAssignmentStatus.Accepted;
                //}

                task.Status = CrmTaskStatus.Close;
                task.ClosedDt = DateTime.Now;


                if (string.IsNullOrEmpty(req.MeetingResult) || string.IsNullOrEmpty(req.MeetingResult.Trim()))
                    throw new AkkordException("meetin_result_required");

                task.MeetingResult = req.MeetingResult;
            }
            else if (req.Status == CrmTaskStatus.Cancel)
            {
                if (string.IsNullOrEmpty(req.MeetingResult))
                    throw new AkkordException("meeting_result_required_for_close_task");

                task.Status = CrmTaskStatus.Cancel;
            }


            await SaveTaskPortfolios(req, task);
            await SaveTaskPhoto(req, task);

            await _unitOfWork.SaveChangesAsync();
            return new CrmtaskDetailUi(task);
        }

        private async Task SaveTaskPhoto(CrmtaskDetailUi req, CrmTask task)
        {
            var uiPhotos = req.Photos.Select(p => new CrmTaskPhoto { PhotoId = p.Id, CrmTaskId = req.Id }).ToList();
            var photoList = RelationSaveHelper.GetUpdatedList(uiPhotos, task.Photos, nameof(CrmTaskPhoto.PhotoId));


            var deletedPhoto = task.Photos.Where(p => photoList.deleted.Any(x => x.PhotoId == p.PhotoId))
                .Select(x => x.Photo)
                .ToList();
            var savedPhoto = req.Photos.Where(p => !photoList.deleted.Any(x => x.PhotoId == p.Id)).Select(x => new Photo
            {
                Id = x.Id,
                Src = x.Src,
                Base64 = x.Base64
            }).ToList();

            List<Photo> newPhotos = new List<Photo>();

            foreach (var p in savedPhoto)
            {
                if (p.Id == 0)
                {
                    p.CrmPhoto = new CrmTaskPhoto { CrmTaskId = task.Id };
                    newPhotos.Add(p);
                }
            }

            await _unitOfWork.Repository<Photo>().AddAllAsync(newPhotos);
            _unitOfWork.Repository<Photo>().RemoveAll(deletedPhoto);

            PhotoHelper.DeleteFiles(deletedPhoto);
            PhotoHelper.SaveFiles(savedPhoto);

        }

        private async Task SaveTaskPortfolios(CrmtaskDetailUi req, CrmTask task)
        {
            var salePoint = task.SalePoint;
            var newPortfolioes = req.Portfolios.Select(p => new SalePointPortfolio { PortfolioId = p, SalePointId = task.SalePointId }).ToList();
            var save = await RelationSaveHelper.SaveOnaToManyList(newPortfolioes, salePoint.SalePointPortfolioes, nameof(SalePointPortfolio.PortfolioId), _unitOfWork);
        }

        private async Task NotifyNewTask(CrmTask task)
        {
            var salePoint = await _unitOfWork.Repository<SalePoint>().GetByIdAsync(task.SalePointId);
            task.Notification =  new Notification
                {
                    //IsRead = false,
                    Status = NotificationStatus.Pending,
                    FromId = _securityService.GetCurrUserId(),
                    ToId = task.SaleManagerId.Value,
                    //CrmTaskId = task.Id,
                    Content = String.Format(_appNotify.TaskContent,salePoint.CodeName,task.VisitDt.Value.ToString("dd.MM.yyyy"))
            };
        }


    }
}
