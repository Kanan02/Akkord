using Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.EntityMappings
{
    public class SalePointPortfolioMapping : IEntityTypeConfiguration<SalePointPortfolio>
    {
        public void Configure(EntityTypeBuilder<SalePointPortfolio> builder)
        {
            builder.HasKey(x => new { x.PortfolioId, x.SalePointId });

            builder.Property(x => x.PortfolioId).HasColumnName("protfolio_id");
            builder.Property(x => x.SalePointId).HasColumnName("sale_pointId");

            builder.HasOne(x => x.Portfolio).WithMany(x => x.SalePointPortfolioes).HasForeignKey(x => x.PortfolioId);
            builder.HasOne(x => x.SalePoint).WithMany(x => x.SalePointPortfolioes).HasForeignKey(x => x.SalePointId);

            builder.ToTable("sale_point_portfolio");
        }
    }
}
