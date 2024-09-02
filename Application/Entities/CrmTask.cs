using Application.Entities.Base;
using Application.Entities.Security;
using Application.Enums;
using Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Entities
{
    public class CrmTask : Base<int>
    {
        public int SalePointId { get; set; }
        public PurposeOfVisit PurposeOfVisit { get; set; }
        public CrmTaskStatus Status { get; set; }
        public string MeetingResult { get; set; }
        
        public DateTime InsertedDt { get; set; }
        public DateTime? VisitDt { get; set; }
        public DateTime? ClosedDt { get; set; }

        public Guid InsertedUserId { get; set; }
        public Guid? SaleManagerId { get; set; }

        //public CrmTaskAssignment TaskAssignment { get; set; }

        public User InsertedUser { get; set; }
        public SaleManager SaleManager { get; set; }
        public SalePoint SalePoint { get; set; }
        public List<CrmTaskPhoto> Photos { get; set; }
        public Notification Notification { get; set; }

        public void ValidateSave()
        {
            if (VisitDt == null)
                throw new AkkordException("visit_dt_required");

            if (SalePointId ==0)
                throw new AkkordException("sale_point_required");

            if (VisitDt <=DateTime.Now.AddHours(3))
                throw new AkkordException("visit_dt_incorrect_date_error");

        }

        

    }
}
