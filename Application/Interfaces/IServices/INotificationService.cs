using Application.Entities;
using Application.Interfaces.IServices.Base;
using Application.Models.Request.Base;
using Application.Models.Request.Ui;
using Application.Models.Response;
using Application.Models.Response.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IServices
{
    public interface INotificationService : IBaseService<Notification>
    {
        Task<IReadOnlyList<NotificationDto>> Get(PagingOptions pager);
        Task<ApiResponse> Read(NotificationDto req);
    }
}
