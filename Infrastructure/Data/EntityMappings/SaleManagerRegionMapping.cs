using Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.EntityMappings
{
    public class SaleManagerRegionMapping : IEntityTypeConfiguration<SaleManagerRegion>
    {
        public void Configure(EntityTypeBuilder<SaleManagerRegion> builder)
        {
            builder.HasKey(x => new { x.RegionId, x.SaleManagerId });

            builder.Property(x => x.RegionId).HasColumnName("region_id");
            builder.Property(x => x.SaleManagerId).HasColumnName("sale_manager_id");

            builder.HasOne(x => x.SaleManager).WithMany(x => x.SaleMangerRegions).HasForeignKey(x => x.SaleManagerId);
            builder.HasOne(x => x.Region).WithMany(x => x.SaleMangerRegions).HasForeignKey(x => x.RegionId);

            builder.ToTable("sale_manager_region");
        }
    }
}
