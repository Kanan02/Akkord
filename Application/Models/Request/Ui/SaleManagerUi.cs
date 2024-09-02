using Application.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models.Request.Ui
{
    public class SaleManagerUi : PagingUi
    {
        public bool IncludeUser { get; set; }
        public bool IncludeRegions { get; set; }
        public int[] RegionIds { get; set; }

        public Guid Id { get; set; }
        public SaleSegment SaleSegment { get; set; }
        public ActivationStatus Status { get; set; }
    }
}
