using Application.Entities;
using Application.Enums;
using Application.Models.Request.Base;
using Application.Models.Request.Ui;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models.Request
{
    public class CrmTaskReq : PagingReq<CrmTask>
    {
        public bool IncludeSalePoint { get; set; }
        public bool IncludeNotification { get; set; }
        public bool IncludePortfolios { get; set; }
        public bool IncludePhoto { get; set; }
        public bool InlcudeInsertedUser { get; set; }
        public bool IncludeAssignedUser { get; set; }
        public SaleSegment SaleSegment { get; set; }
        public DateTime? FromDt { get; set; }
        public DateTime? ToDt { get; set; }

        public CrmTaskReq() { }
        public CrmTaskReq(CrmTaskUi req) {
            SaleSegment = req.SaleSegment;
            FromDt = req.FromDt;
            ToDt = req.ToDt;
            Value = new CrmTask
            {
                Id = req.Id,
                SalePointId = req.SalePointId,
                Status = req.Status,
                SaleManagerId = req.SaleManagerId
            };
        }



    }
}
