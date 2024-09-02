using Application.Entities;
using Application.Models.Request.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models.Request
{
    public class RegionReq : BaseReq<Region>
    {
        public bool IncludeSaleManagerRegions { get; set; }
        public Guid SaleManagerId { get; set; }
    }
}
