using Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.EntityMappings
{
    public class SaleManagerMapping : IEntityTypeConfiguration<SaleManager>
    {
        public void Configure(EntityTypeBuilder<SaleManager> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id"); //.HasColumnType("bigint(20) UNSIGNED");

            builder.Property(x => x.FirstName).HasColumnName("first_name").HasColumnType("nvarchar(50)");
            builder.Property(x => x.LastName).HasColumnName("last_name").HasColumnType("nvarchar(50)");
            builder.Property(x => x.Msisdn).HasColumnName("msisdn").HasColumnType("nvarchar(20)");
            builder.Property(x => x.SaleSegment).HasColumnName("sale_segment");
            builder.Property(x => x.Status).HasColumnName("status");

            builder.HasOne(x => x.User).WithOne().HasForeignKey<SaleManager>(x => x.Id);

            builder.ToTable("sale_manager");
        }
            
    }
}
