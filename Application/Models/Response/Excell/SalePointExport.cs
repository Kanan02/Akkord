using Application.Entities;
using Application.Models.Response.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models.Response.Excell
{
    public class SalePointExport : IListFilterDto<SalePoint, SalePointExport>
    {

        public int Id { get; set; }
        public string SaleSegment { get; set; }
        public string CodeName { get; set; }
        public string Region { get; set; }
        public string SaleManager { get; set; }
        public string City { get; set; }
        //public decimal GpsX { get; set; }
        //public decimal GpsY { get; set; }

        // sale segment retail
        public string ShopType { get; set; }
        public string ShopClassification { get; set; }

        // sale segment corporate
        public string CorporateSegment { get; set; }
        public string ConstructionType { get; set; }
        public string ConstBetonSupplier { get; set; }

        public SalePointExport SetDto(SalePoint entity)
        {
            Id = entity.Id;
            SaleSegment = nameof(entity.SaleSegment);
            CodeName = entity.CodeName;
            Region = entity.Region != null ? entity.Region.Name : "";
            SaleManager = $"{entity.SaleManager?.FirstName} {entity.SaleManager?.LastName}";
            City = entity.City;
            ShopType = entity.ShopType.ToString();
            ShopClassification = entity.ShopClassification.ToString();
            CorporateSegment = entity.CorporateSegment.ToString();
            ConstructionType = entity.ConstructionType.ToString();
            ConstBetonSupplier = entity.ConstBetonSupplier;
            //GpsX = entity.GpsX;
            //GpsY = entity.GpsY;
            return this;
        }

    }
}
