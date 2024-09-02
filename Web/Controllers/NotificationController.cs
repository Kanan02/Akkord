using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Entities;
using Application.Interfaces.IServices;
using Application.Models.Request.Base;
using Application.Models.Request.Ui;
using Application.Models.Response;
using Application.Models.Response.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    [Authorize]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public Task<IReadOnlyList<NotificationDto>> GetAll([FromBody] PagingOptions pager) => _notificationService.Get(pager);

        public Task<ApiResponse> Read([FromBody]NotificationDto req) => _notificationService.Read(req);
    }
}
