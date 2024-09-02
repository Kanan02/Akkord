using Application.Entities;
using Application.Models.Request.Base;
using Application.Models.Request.Ui;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models.Request
{
    public class SaleManagerReq : BaseReq<SaleManager>
    {
        public bool IncludeUser { get; set; }
        public bool IncludeRegions { get; set; }
        public bool IncludeSalePoints { get; set; }
        public int[] RegionIds { get; set; }

        public SaleManagerReq() { }

        public SaleManagerReq(SaleManagerUi req)
        {
            IncludeUser = req.IncludeUser;
            IncludeRegions = req.IncludeRegions;
            RegionIds = req.RegionIds;
            Value = new SaleManager
            {
                Id = req.Id,
                Status = req.Status,
                SaleSegment = req.SaleSegment
            };

        }
    }
}
