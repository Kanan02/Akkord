using Application.Entities;
using Application.Models.Request.Base;
using Application.Models.Request.Ui;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models.Request
{
    public class SalePointReq : PagingReq<SalePoint>
    {
        public bool IncludePortfolios { get; set; }
        public bool IncludeSellers { get; set; }
        public bool IncludePhoto { get; set; }
        public bool IncludePhotos { get; set; }
        public bool IncludeRegion { get; set; }
        public bool IncludeSaleManager { get; set; }
        public List<int> RegionIds { get; set; }

        public SalePointReq()
        {

        }

        public SalePointReq(SalePointUi req)
        {
            IncludePortfolios = req.IncludePortfolioes;
            IncludeSellers = req.IncludeSellers;
            IncludePhoto = req.IncludePhoto;
            IncludePhotos = req.IncludePhotos;
            IncludeRegion = req.IncludeRegion;
            IncludeSaleManager = req.IncludeSaleManager;
            RegionIds = req.RegionIds;
            Value = new SalePoint
            {
                Id = req.Id,
                SaleManagerId = req.SaleManagerId??Guid.Empty,
                CodeName = req.CodeName,
                RegionId = req.RegionId,
                SaleSegment = req.SaleSegment,
                Status = req.Status
            };

            Pager = new PagingOptions
            {
                PageSize = (req.PageSize <= 0 || req.PageSize>100) ? 100 : req.PageSize,
                CurrentPage = req.CurrnetPage
            };
        }

    }
}
