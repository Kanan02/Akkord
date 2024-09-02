using Application.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models.Request.Ui
{
    public class SalePointUi : PagingUi
    {
        // includes
        public bool IncludePortfolioes { get; set; }
        public bool IncludeSellers { get; set; }
        public bool IncludePhoto { get; set; }
        public bool IncludePhotos { get; set; }
        public bool IncludeRegion { get; set; }
        public bool IncludeSaleManager { get; set; }

        // entity fields
        public int Id { get; set; }
        public Guid? SaleManagerId { get; set; }
        public string CodeName { get; set; }
        public int RegionId { get; set; }
        public List<int> RegionIds { get; set; }
        public SaleSegment SaleSegment { get; set; }
        public SalePointStatus Status { get; set; }


    }
}
