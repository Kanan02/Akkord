using Application.Entities;
using Application.Enums;
using Application.Models.Response.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models.Response
{
    public class NotificationDto :  IListFilterDto<Notification, NotificationDto>
    {
        public int Id { get; set; }
        public string Content { get; set; }
        //public bool IsRead { get; set; }
        public int? SalePointId { get; set; }
        public int? CrmTaskId { get; set; }
        public NotificationStatus Status { get; set; }
        //public Guid SaleManagerId { get; set; }

        public NotificationDto(){}
        //public NotificationDto(Notification notification)
        //{
        //    Content = notification.Content;
        //    IsRead = notification.IsRead;
        //    SalePointId = notification.SalePointId;
        //    CrmTaskId = notification.CrmTaskId;
        //}

        public NotificationDto SetDto(Notification entity)
        {
            Id = entity.Id;
            Content = entity.Content;
            //IsRead = entity.IsRead;
            SalePointId = entity.SalePointId;
            CrmTaskId = entity.CrmTaskId;
            Status = entity.Status;
            return this;
        }
    }
}
