using Application.Entities;
using Application.Enums;
using Application.Exceptions;
using Application.Interfaces.IServices;
using Application.Interfaces.IServices.Security;
using Application.Interfaces.IUoW;
using Application.Models.Request;
using Application.Models.Request.Base;
using Application.Models.Request.Ui;
using Application.Models.Response;
using Application.Models.Response.Base;
using Application.Services.Base;
using Application.Spesifications.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class NotificationService : BaseService<Notification>, INotificationService
    {
        private readonly IAccessValidator _accessValidator;
        public NotificationService(IAccessValidator accessValidator, IUoW unitOfWork) : base(unitOfWork)
        {
            _accessValidator = accessValidator;
        }

        protected override ISpecification<Notification> FilterList(BaseReq<Notification> request, ISpecification<Notification> spec)
        {
            var req = (NotificationReq)request;
            var value = req.Value;

            //_accessValidator.SetUserIdByRole(ref value, nameof(CrmTask.SaleManagerId));

            //if (req.IncludeSaleManager)
            //    spec.Includes.Add(x => x.SaleManager);

            //if (req.IncludeTask)
            //    spec.Includes.Add(x => x.CrmTask);

            if (req.IncludeSalePoint)
                spec.Includes.Add(x => x.SalePoint);

            //if (req.IsRead != null)
            //    spec.Filters.Add(x => x.IsRead == req.IsRead);

            if (value != null)
            {
                if (value.ToId != null && value.ToId != Guid.Empty)
                    spec.Filters.Add(x => x.ToId == value.ToId);
            }

            if (req.Pager != null)
                spec.ApplyPaging(req.Pager.Skip, req.Pager.PageSize);

            if (req.orderByDescExpression != null)
                spec.ApplyOrderByDescending(req.orderByDescExpression);

            return base.FilterList(request, spec);
        }


        public async Task<IReadOnlyList<NotificationDto>> Get(PagingOptions pager)
        {

            var req = new NotificationReq
            {
                //IsRead = false,
                orderByDescExpression = (x) => x.Id,
                Pager = pager,
                Value = new Notification
                {
                    ToId = _accessValidator.GetCurrUserId()
                }
            };

            var result = await GetListByFilter<NotificationDto>(req);
            return result;
        }

        public async Task<ApiResponse> Read(NotificationDto req)
        {
            if (req == null)
                throw new AkkordException("null_request");

            if (req.Status != NotificationStatus.Accept && req.Status != NotificationStatus.Reject)
                throw new AkkordException("incorrect_notification_status");

            var notification = await _unitOfWork.Repository<Notification>().GetByIdAsync(req.Id);
            if (notification == null)
                throw new AkkordException("notification_not_found");

            var currUserId = _accessValidator.GetCurrUserId();
            if (notification.ToId != currUserId)
                throw new AkkordException("access_denied_for_this_user");

            if(notification.Status != Enums.NotificationStatus.Pending)
                throw new AkkordException("notification_already_completed");

            var crmTask = await _unitOfWork.Repository<CrmTask>().GetByIdAsync(notification.CrmTaskId);
            if (crmTask == null)
                throw new AkkordException("crm_task_not_found_for_notification");

            notification.Status = req.Status;

            if (req.Status == NotificationStatus.Accept)
            {
                crmTask.SaleManagerId = _accessValidator.GetCurrUserId();
                crmTask.Status = CrmTaskStatus.Assigned;
            }
            else if (req.Status == NotificationStatus.Reject)
            {
                crmTask.Status = CrmTaskStatus.Close;
            }


            //notification.IsRead = true;
            await SaveUoW();

            return new ApiResponse();
        }
    }
}
