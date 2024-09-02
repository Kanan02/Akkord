using Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.EntityMappings
{
    public class ProductPortfolioMapping : IEntityTypeConfiguration<ProductPortfolio>
    {
        public void Configure(EntityTypeBuilder<ProductPortfolio> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");

            builder.Property(x => x.Name).HasColumnName("name").HasColumnType("nvarchar(500)");
            builder.ToTable("product_portfolio");
        }
    }
}
