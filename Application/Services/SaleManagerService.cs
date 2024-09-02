using Application.Constants;
using Application.Entities;
using Application.Entities.Security;
using Application.Enums;
using Application.Exceptions;
using Application.Helpers;
using Application.Interfaces.IRepository;
using Application.Interfaces.IServices;
using Application.Interfaces.IServices.Security;
using Application.Interfaces.IUoW;
using Application.Models.Request;
using Application.Models.Request.Base;
using Application.Models.Request.Ui;
using Application.Models.Response.Base;
using Application.Services.Base;
using Application.Spesifications.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SaleManagerService : BaseService<SaleManager>, ISaleManagerService
    {

        private readonly IRoleService _roleService;
        public SaleManagerService(IUoW unitOfWork, IRoleService roleService) : base(unitOfWork)
        {
            _roleService = roleService;
        }

        public override async Task<SaleManager> Save(SaleManager entity)
        {
            ValidateSave(entity);
            if (entity.Id == Guid.Empty)
            {
                await CheckDuplicateUserName(entity.User.Username);
                entity.Status = ActivationStatus.Active;
                entity.User.UserRoles = new List<UserRole> {
                    new UserRole {
                    RoleId = (await _roleService.GetByName(RoleConstant.SaleManager)).Id
                    }
                };

                await base.Save(entity);
            }
            else
                await UpdateManager(entity);

            return entity;
        }

        private void ValidateSave(SaleManager entity)
        {
            if (entity == null)
                throw new AkkordException("sent_obj_is_null");

            entity.ValidateSave();
        }

        private async Task CheckDuplicateUserName(string username, Guid? userId = null)
        {
            var user = await GetByUsername(username);

            // insert check
            if (user != null && userId == null)
                throw new AkkordException("duplicate_username");

            // update check
            if (user != null && user.Id != userId.Value)
                throw new AkkordException("duplicate_username");
        }

        private async Task<SaleManager> GetByUsername(string username)
        {

            var saleManagerReq = new SaleManagerReq
            {
                IncludeUser = true,
                IncludeRegions = true,
                Value = new SaleManager
                {
                    User = new User
                    {
                        Username = username
                    }
                }
            };

            var result = await GetListByFilter(saleManagerReq, false);
            return result.FirstOrDefault();
        }

        private async Task UpdateManager(SaleManager entity)
        {
            entity.User.Id = entity.Id;

            await CheckDuplicateUserName(entity.User.Username, entity.Id);

            var manager = await GetByRegions(entity.Id);

            SetRegionsManagerId(entity);

            await RelationSaveHelper.SaveOnaToManyList(entity.SaleMangerRegions, manager.SaleMangerRegions, nameof(SaleManagerRegion.RegionId), _unitOfWork);
            //await UpdateSaleManagerRegions(entity,manager);
            _unitOfWork.Repository<SaleManager>().Update(entity, manager);
            _unitOfWork.Repository<User>().Update(entity.User);
            await SaveUoW();
        }

        private void SetRegionsManagerId(SaleManager entity)
        {
            if (entity.SaleMangerRegions == null)
                entity.SaleMangerRegions = new List<SaleManagerRegion>();
            else
                entity.SaleMangerRegions.ForEach(sm => sm.SaleManagerId = entity.Id);
        }

        private async Task<SaleManager> GetByRegions(Guid saleManagerId)
        {

            var saleManagerReq = new SaleManagerReq
            {
                IncludeRegions = true,
                Value = new SaleManager
                {
                    Id = saleManagerId
                }
            };

            var result = await GetListByFilter(saleManagerReq);
            return result.FirstOrDefault();
        }

        //private async Task UpdateSaleManagerRegions(SaleManager newEntity,SaleManager entity) 
        //{
        //    Func<SaleManagerRegion, List<SaleManagerRegion>, bool> condition  = (item,list) => list.All(x => item.RegionId == x.RegionId);
        //    await RelationSaveHelper.SaveOnaToManyList(newEntity.SaleMangerRegions, entity.SaleMangerRegions, condition, _unitOfWork);
        //}

        protected override ISpecification<SaleManager> FilterList(BaseReq<SaleManager> request, ISpecification<SaleManager> spec)
        {
            var req = (SaleManagerReq)request;
            var value = req.Value;

            if (req.IncludeUser)
            {
                spec.Includes.Add(x => x.User);
                if (req.Value != null && req.Value.User != null && !string.IsNullOrEmpty(req.Value.User.Username))
                    spec.Filters.Add(x => x.User.Username == req.Value.User.Username.Trim());
            }

            if (req.IncludeRegions)
            {
                var regionInclude = $"{nameof(SaleManager.SaleMangerRegions)}.{nameof(SaleManagerRegion.Region)}";
                spec.IncludeStrings.Add(regionInclude);

                if (req.RegionIds != null && req.RegionIds.Count() > 0)
                    spec.Filters.Add(x => x.SaleMangerRegions.Any(sm => req.RegionIds.Any(r => r == sm.RegionId)));
            }

            if (req.IncludeSalePoints)
                spec.Includes.Add(x => x.SalePoints);

            if (value != null)
            {
                if (value.Id != Guid.Empty)
                    spec.Filters.Add(x => x.Id == value.Id);

                if (value.Status > 0)
                    spec.Filters.Add(x => x.Status == value.Status);

                if (value.SaleSegment != 0)
                    spec.Filters.Add(x => x.SaleSegment == value.SaleSegment);
            }

            return spec;
        }

        public async Task<ApiResponse> ReplaceSalePoints(ManagerPointReplacementUi req)
        {
            if (req == null)
                throw new AkkordException("null_request");

            req.ValidateReplace();

            var fromManager = await GetReplacedManager(req.FromManager);
            var toManager = await GetReplacedManager(req.ToManager);

            await ReplaceRegions(req, fromManager, toManager);
            ReplaceSalePoints(req, fromManager);

            await _unitOfWork.SaveChangesAsync();

            return new ApiResponse();

        }

        private async Task ReplaceRegions(ManagerPointReplacementUi req,SaleManager fromManager, SaleManager toManager)
        {
            var deletedRegions = fromManager.SaleMangerRegions
                .Where(x => req.Regions.Any(r => r == x.RegionId))
                .ToList();

            var addedRegions = req.Regions
                .Where(x => toManager.SaleMangerRegions.All(r => r.RegionId != x))
                .Select(x => new SaleManagerRegion
                {
                    SaleManagerId = req.ToManager,
                    RegionId = x
                }).ToList();

            _unitOfWork.Repository<SaleManagerRegion>().RemoveAll(deletedRegions);
            await _unitOfWork.Repository<SaleManagerRegion>().AddAllAsync(addedRegions);
        }

        private void ReplaceSalePoints(ManagerPointReplacementUi req, SaleManager fromManager)
        {
            var updatedPoints = fromManager
                .SalePoints
                .Where(sp => req.Regions.Any(r => r == sp.RegionId))
                .ToList();

            updatedPoints.ForEach(p => p.SaleManagerId = req.ToManager);
        }

        private async Task<SaleManager> GetReplacedManager(Guid managerId)
        {
            var req = new SaleManagerReq
            {
                IncludeRegions = true,
                IncludeSalePoints = true,
                Value = new SaleManager {
                    Id = managerId
                }
            };

            return (await GetListByFilter(req)).FirstOrDefault();
        }

    }
}
