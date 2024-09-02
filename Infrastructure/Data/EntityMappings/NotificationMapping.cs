using Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.EntityMappings
{
    public class NotificationMapping : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");

            builder.Property(x => x.Content).HasColumnName("content").HasColumnType("nvarchar(1000)");
            //builder.Property(x => x.IsRead).HasColumnName("is_read");
            builder.Property(x => x.SalePointId).HasColumnName("sale_point_id");
            builder.Property(x => x.CrmTaskId).HasColumnName("task_id");
            builder.Property(x => x.Status).HasColumnName("status");
            //builder.Property(x => x.SaleManagerId).HasColumnName("sale_manager_id").HasCollation("latin1_swedish_ci");
            builder.Property(x => x.FromId).HasColumnName("from_id").HasCollation("latin1_swedish_ci");
            builder.Property(x => x.ToId).HasColumnName("to_id").HasCollation("latin1_swedish_ci");

            builder.HasOne(x => x.SalePoint).WithMany().HasForeignKey(x => x.SalePointId);
            builder.HasOne(x => x.CrmTask).WithOne().HasForeignKey<Notification>(x => x.CrmTaskId);

            builder.HasOne(x => x.CrmTask).WithOne(x => x.Notification).HasForeignKey<Notification>(x => x.CrmTaskId);
            //builder.HasOne(x => x.SaleManager).WithMany().HasForeignKey(x => x.SaleManagerId);

            builder.HasOne(x => x.From).WithMany().HasForeignKey(x => x.FromId);
            builder.HasOne(x => x.To).WithMany().HasForeignKey(x => x.ToId);



            builder.ToTable("notification");
        }
    }
}
