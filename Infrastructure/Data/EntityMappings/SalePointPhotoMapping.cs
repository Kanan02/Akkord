using Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.EntityMappings
{
    public class SalePointPhotoMapping : IEntityTypeConfiguration<SalePointPhoto>
    {
        public void Configure(EntityTypeBuilder<SalePointPhoto> builder)
        {
            builder.HasKey(x => new { x.SalePointId, x.PhotoId });

            builder.Property(x => x.SalePointId).HasColumnName("sale_point_id");
            builder.Property(x => x.PhotoId).HasColumnName("photo_id");

            builder.HasOne(x => x.SalePoint).WithMany(x => x.Photos).HasForeignKey(x => x.SalePointId);
            builder.HasOne(x => x.Photo).WithOne(x => x.SalePointPhoto).HasForeignKey<SalePointPhoto>(x => x.PhotoId);

            builder.ToTable("sale_point_photo");
        }
    }
}
