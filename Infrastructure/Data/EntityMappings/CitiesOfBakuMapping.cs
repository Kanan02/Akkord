using Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.EntityMappings
{
    public class CityOfBakuMapping : IEntityTypeConfiguration<CitiesOfBaku>
    {
        public void Configure(EntityTypeBuilder<CitiesOfBaku> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id").ValueGeneratedNever();

            builder.Property(x => x.Name).HasColumnName("name").HasColumnType("nvarchar(100)");
            builder.ToTable("cities_of_baku");
        }
    }
}
