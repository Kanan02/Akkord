using Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.EntityMappings
{
    public class SalePointMapping : IEntityTypeConfiguration<SalePoint>
    {
        public void Configure(EntityTypeBuilder<SalePoint> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");

            builder.Property(x => x.Address).HasColumnName("address").HasColumnType("nvarchar(1000)");
            builder.Property(x => x.ConstBetonSupplier).HasColumnName("beton_supplier").HasColumnType("nvarchar(500)");
            builder.Property(x => x.City).HasColumnName("city").HasColumnType("nvarchar(100)");
            builder.Property(x => x.CodeName).HasColumnName("code_name").HasColumnType("nvarchar(500)");
            builder.Property(x => x.Comment).HasColumnName("comment").HasColumnType("nvarchar(1000)");
            builder.Property(x => x.ContactPerson).HasColumnName("contact_person").HasColumnType("nvarchar(100)");
            builder.Property(x => x.ContactPersonDateOfBirth).HasColumnName("contact_person_date_of_birth");
            builder.Property(x => x.ContactPersonMsisdn).HasColumnName("contact_person_msisdn").HasColumnType("nvarchar(30)");
            builder.Property(x => x.CorporateSegment).HasColumnName("corporate_segment"); 
            builder.Property(x => x.ConstructionType).HasColumnName("construction_type");
            builder.Property(x => x.District).HasColumnName("district").HasColumnType("nvarchar(500)");
            builder.Property(x => x.GpsX).HasColumnName("gps_x");
            builder.Property(x => x.GpsY).HasColumnName("gps_y");
            builder.Property(x => x.JuridicalName).HasColumnName("juridical_name").HasColumnType("nvarchar(50)");
            builder.Property(x => x.OwnerDateOfBirth).HasColumnName("owner_date_of_birth");
            builder.Property(x => x.OwnerMsisdn).HasColumnName("owner_msisdn").HasColumnType("nvarchar(30)");
            builder.Property(x => x.PhotoId).HasColumnName("photo_id");
            builder.Property(x => x.RegionId).HasColumnName("region_id");
            builder.Property(x => x.SaleSegment).HasColumnName("sale_segment");
            builder.Property(x => x.ShopAgg).HasColumnName("shop_agg");
            builder.Property(x => x.ShopBeton).HasColumnName("shop_beton");
            builder.Property(x => x.ShopCement).HasColumnName("shop_cement");
            builder.Property(x => x.ShopClassification).HasColumnName("shop_classification");
            builder.Property(x => x.ShopName).HasColumnName("shop_name");
            builder.Property(x => x.ShopOwnership).HasColumnName("shop_ownership");
            builder.Property(x => x.ShopType).HasColumnName("shop_type");
            builder.Property(x => x.Village).HasColumnName("village").HasColumnType("nvarchar(500)"); ;
            builder.Property(x => x.SaleManagerId).HasColumnName("sale_manager_id");
            builder.Property(x => x.Status).HasColumnName("status");

            builder.HasOne(x => x.Photo).WithMany(x => x.SalePoints).HasForeignKey(x => x.PhotoId);
            builder.HasOne(x => x.Region).WithMany(x => x.SalePoints).HasForeignKey(x => x.RegionId);
            builder.HasOne(x => x.SaleManager).WithMany(x => x.SalePoints).HasForeignKey(x => x.SaleManagerId);

            builder.ToTable("sale_point");
        }
    
    }
}
