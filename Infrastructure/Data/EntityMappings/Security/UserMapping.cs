using Application.Entities.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.EntityMappings.Security
{
    class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");

            builder.Property(x => x.Username).HasColumnName("username").HasColumnType("nvarchar(50)");
            builder.Property(x => x.Password).HasColumnName("password").HasColumnType("nvarchar(50)");

            builder.ToTable("user");
        }
    }
}
