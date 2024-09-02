using Core.Entities.Base;
using Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class SalePoint : Base<int>
    {
        public SaleSegment SaleSegment { get; set; }
        public string CodeName { get; set; }
        public int RegionId { get; set; }
        
        public string City { get; set; }
        public string District { get; set; }
        public string Village { get; set; }
        public string Address { get; set; }
        public decimal GpsX { get; set; }     
        public decimal GpsY { get; set; }

        // sale segment corporate
        public ShopType ShopType { get; set; }
        public ShopClassification ShopClassification { get; set; }

        // sale segment corporate
        public CorporateSegment CorporateSegment { get; set; }
        public string BetonSupplier { get; set; }

        public string JuridicalName { get; set; }
        public string OwnerOrDirector { get; set; }
        public string OwnerNumber { get; set; }
        public DateTime? OwnerDateOfBirth { get; set; }

        public string ContactPerson { get; set; }
        public string ContactPersonNumber { get; set; }
        public DateTime? ContactPersonDateOfBirth { get; set; }


        public string ShopName { get; set; }
        public ShopOwnership ShopOwnership { get; set; }
        public decimal ShopCement { get; set; }
        public decimal ShopBeton { get; set; }
        public decimal ShopAgg { get; set; }

        public string Comment { get; set; }

        public int PhotoId { get; set; }

        public Photo Photo { get; set; }
        public Region Region { get; set; }
    }
}
