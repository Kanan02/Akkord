using Application.Entities.Base;
using Application.Enums;
using Application.Exceptions;
using Application.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Application.Entities
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

        // sale segment retail
        public ShopType ShopType { get; set; }
        public ShopClassification ShopClassification { get; set; }

        // sale segment corporate
        public CorporateSegment CorporateSegment { get; set; }
        public ConstructionType ConstructionType { get; set; }
        public string ConstBetonSupplier { get; set; }

        public string JuridicalName { get; set; }
        public string OwnerOrDirector { get; set; }
        public string OwnerMsisdn { get; set; }
        public DateTime? OwnerDateOfBirth { get; set; }

        public string ContactPerson { get; set; }
        public string ContactPersonMsisdn { get; set; }
        public DateTime? ContactPersonDateOfBirth { get; set; }


        public string ShopName { get; set; }
        public ShopOwnership ShopOwnership { get; set; }
        public decimal ShopCement { get; set; }
        public decimal ShopBeton { get; set; }
        public decimal ShopAgg { get; set; }

        public string Comment { get; set; }

        public int? PhotoId { get; set; }
        public Guid SaleManagerId { get; set; }
        public SalePointStatus Status { get; set; }
        public SaleManager SaleManager { get; set; }

        public Photo Photo { get; set; }
        public List<SalePointPhoto> Photos { get; set; }
        public Region Region { get; set; }
        public List<SalePointPortfolio> SalePointPortfolioes { get; set; }
        public List<SalePointSeller> SalePointSellers { get; set; }

        [NotMapped]
        public string Image { get; set; }

        public void ValidateRetail()
        {
            if (ReflectionHelper.GetEnumValues<ShopClassification>().All(x => x != ShopClassification))
                throw new AkkordException("shop_classification_requried");

            if (ReflectionHelper.GetEnumValues<ShopType>().All(x => x != ShopType))
                throw new AkkordException("shop_type_requried");

            if (ReflectionHelper.GetEnumValues<ShopType>().All(x => x != ShopType))
                throw new AkkordException("shop_name_requried");

        }

        public void ValidateCorporate()
        {

            if (ReflectionHelper.GetEnumValues<CorporateSegment>().All(x => x != CorporateSegment))
                throw new AkkordException("corporate_segment_requried");

            if (CorporateSegment == CorporateSegment.Construction)
            {
                if (ReflectionHelper.GetEnumValues<ConstructionType>().All(x => x != ConstructionType))
                    throw new AkkordException("construction_type_requried_for_buisness_segment_construction");

                if (string.IsNullOrEmpty(ConstBetonSupplier))
                    throw new AkkordException("beton_supplier_requried_for_buisness_segment_construction");
            }

        }

        public void ValidateSalePoint()
        {

            if (ReflectionHelper.GetEnumValues<SalePointStatus>().All(x => x != Status))
                throw new AkkordException("invalid_status");

            if (string.IsNullOrEmpty(CodeName))
                throw new AkkordException("code_name_requried");

            if (RegionId <= 0)
                throw new AkkordException("region_required");

            if (SalePointPortfolioes.Count == 0)
                throw new AkkordException("product_portfolio_required");

            if (GpsX == 0 || GpsY == 0)
                throw new AkkordException("gps_cordinates_required");

            if (string.IsNullOrEmpty(JuridicalName))
                throw new AkkordException("juridical_name_requried");

            if (string.IsNullOrEmpty(OwnerOrDirector))
                throw new AkkordException("owner_or_director_requried");

            if (string.IsNullOrEmpty(OwnerMsisdn))
                throw new AkkordException("owner_or_director_phone_required");

            if (ShopOwnership <= 0)
                throw new AkkordException("shop_ownership_required");

            if (Id == 0 && (Photos == null || string.IsNullOrEmpty(Photos[0].Base64)))
                throw new AkkordException("image_required");

        }

        public void ValidateSellers() => SalePointSellers.ForEach(sel => sel.SalePointCreateValidate());

        public void ValidateSave()
        {
            if (SaleSegment == SaleSegment.Corporate)
                ValidateCorporate();
            else if (SaleSegment == SaleSegment.Retail)
                ValidateRetail();
            else
                throw new AkkordException("sale_segment_required_or");

            ValidateSalePoint();
            ValidateSellers();
        }

        public void ValidateAdminSave()
        {
            if (string.IsNullOrEmpty(CodeName))
                throw new AkkordException("code_name_requried");

            if (RegionId <= 0)
                throw new AkkordException("region_required");

            if (SaleManagerId == null || SaleManagerId == Guid.Empty)
                throw new AkkordException("sale_manager_required");
        }
    }



}
