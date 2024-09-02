using Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.EntityMappings
{
    public class CrmTaskPhotoMapping : IEntityTypeConfiguration<CrmTaskPhoto>
    {
        public void Configure(EntityTypeBuilder<CrmTaskPhoto> builder)
        {
            builder.HasKey(x => new { x.CrmTaskId, x.PhotoId });

            builder.Property(x => x.CrmTaskId).HasColumnName("crm_task_id");
            builder.Property(x => x.PhotoId).HasColumnName("photo_id");

            builder.HasOne(x => x.CrmTask).WithMany(x => x.Photos).HasForeignKey(x => x.CrmTaskId);
            builder.HasOne(x => x.Photo).WithOne(x => x.CrmPhoto).HasForeignKey<CrmTaskPhoto>(x => x.PhotoId);

            builder.ToTable("crm_task_photo");
        }
    }
}
