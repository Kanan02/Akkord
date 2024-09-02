using Application.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models.Request.Ui
{
    public class CrmTaskUi
    {
        public int Id { get; set; }
        public int SalePointId { get; set; }
        //public PurposeOfVisit PurposeOfVisit { get; set; }
        public CrmTaskStatus Status { get; set; }
        //public string MeetingResult { get; set; }

        //public DateTime InsertedDt { get; set; }
        //public DateTime? VisitDt { get; set; }

        //public Guid InsertedUserId { get; set; }
        public Guid? SaleManagerId { get; set; }
        public SaleSegment SaleSegment { get; set; }
        public DateTime? FromDt { get; set; }
        public DateTime? ToDt { get; set; }
        //public int RegionId { get; set; }
    }
}
