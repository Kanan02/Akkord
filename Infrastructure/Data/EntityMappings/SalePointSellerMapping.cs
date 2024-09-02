using Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.EntityMappings
{
    public class SalePointSellerMapping : IEntityTypeConfiguration<SalePointSeller>
    {
        public void Configure(EntityTypeBuilder<SalePointSeller> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");

            builder.Property(x => x.DateOfBirth).HasColumnName("date_of_birth");
            builder.Property(x => x.Fullname).HasColumnName("fullname").HasColumnType("nvarchar(50)");
            builder.Property(x => x.Msisdn).HasColumnName("msisdn").HasColumnType("nvarchar(30)");
            builder.Property(x => x.SalePointId).HasColumnName("sale_point_id");

            builder.HasOne(x => x.SalePoint).WithMany(x => x.SalePointSellers).HasForeignKey(x => x.SalePointId);
            builder.ToTable("sale_point_seller");
        }
    }
}
