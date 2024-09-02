using Application.Config;
using Application.Entities;
using Application.Enums;
using Application.Models.Response.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Models.Response
{
    public class SalePointDto : IListFilterDto<SalePoint, SalePointDto>
    {
        public int Id { get; set; }
        public SaleSegment SaleSegment { get; set; }
        public string CodeName { get; set; }
        public int Region { get; set; }
        public string SaleManager { get; set; }
        public string City { get; set; }
        public decimal GpsX { get; set; }
        public decimal GpsY { get; set; }

        // sale segment retail
        public ShopType ShopType { get; set; }
        public ShopClassification ShopClassification { get; set; }

        // sale segment corporate
        public CorporateSegment CorporateSegment { get; set; }
        public ConstructionType ConstructionType { get; set; }
        public string ConstBetonSupplier { get; set; }
        public string PhotoSrc { get; set; }
        public decimal ShopCement { get; set; }
        public List<string> PhotoSrcs { get; set; }

        public List<int?> Portfolios { get; set; }

        public SalePointDto SetDto(SalePoint entity)
        {
            Id = entity.Id;
            SaleSegment = entity.SaleSegment;
            CodeName = entity.CodeName;
            Region = entity.Region!= null ? entity.Region.Id : 0;
            SaleManager = $"{entity.SaleManager?.FirstName} {entity.SaleManager?.LastName}";
            City = entity.City;
            ShopType = entity.ShopType;
            ShopClassification = entity.ShopClassification;
            CorporateSegment = entity.CorporateSegment;
            ConstructionType = entity.ConstructionType;
            ConstBetonSupplier = entity.ConstBetonSupplier;
            GpsX = entity.GpsX;
            GpsY = entity.GpsY;
            ShopCement = entity.ShopCement;
            //Portfolios = entity.SalePointPortfolioes != null && entity.SalePointPortfolioes.Count() > 0
            //    ? entity.SalePointPortfolioes.Select(x => new PortfolioDto(x.Portfolio)).ToList() : new List<PortfolioDto>();
            Portfolios = entity.SalePointPortfolioes != null && entity.SalePointPortfolioes.Count() > 0
                ? entity.SalePointPortfolioes.Select(x => x.Portfolio?.Id).ToList() : new List<int?>();
            PhotoSrc = entity.Photo != null ? $"{ProjectSetting.PhotoPath}{entity.Photo.Src}" : "";
            PhotoSrcs = entity.Photos!=null ? entity.Photos.Where(p => p.Photo != null).Select(p => $"{ProjectSetting.PhotoPath}{p.Photo.Src}").ToList() :  new List<string>();
            return this;
        }
    }
}
