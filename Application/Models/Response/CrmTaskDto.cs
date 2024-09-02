using Application.Entities;
using Application.Enums;
using Application.Models.Response.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models.Response
{
    public class CrmTaskDto : IListFilterDto<CrmTask, CrmTaskDto>
    {
        public int Id { get; set; }
        public int SalePointId { get; set; }
        public PurposeOfVisit PurposeOfVisit { get; set; }
        public CrmTaskStatus Status { get; set; }
        //public string MeetingResult { get; set; }
        public int? RegionId { get; set; }
        public string City { get; set; }
        public string SalePointCodeName { get; set; }
        public DateTime? VisitDt { get; set; }
        public Guid ToId { get; set; }
        public NotificationStatus NotificationStatus { get; set; }
        public string SaleManagerName { get; set; }

        public CrmTaskDto SetDto(CrmTask entity)
        {
            Id = entity.Id;
            SalePointId = entity.SalePointId;
            SalePointCodeName = entity?.SalePoint?.CodeName;
            PurposeOfVisit = entity.PurposeOfVisit;
            Status = entity.Status;
            RegionId = entity?.SalePoint?.Region?.Id;
            City = entity?.SalePoint?.City;
            VisitDt = entity.VisitDt;

            if (entity.Notification != null)
            {
                ToId = entity.Notification.ToId;
                NotificationStatus = entity.Notification.Status;
                SaleManagerName = $"{entity.Notification.To?.Username}";
            }

            return this;
        }

        //public DateTime InsertedDt { get; set; }
        //public DateTime? VisitDt { get; set; }
        //public DateTime? ClosedDt { get; set; }

        //public Guid InsertedUserId { get; set; }
        //public Guid? AssignedUserId { get; set; }
    }
}
