using Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.EntityMappings
{
    public class CrmTaskMapping : IEntityTypeConfiguration<CrmTask>
    {
        public void Configure(EntityTypeBuilder<CrmTask> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");

            builder.Property(x => x.InsertedDt).HasColumnName("inserted_dt");
            builder.Property(x => x.MeetingResult).HasColumnName("meeting_result").HasColumnType("nvarchar(3000)");
            builder.Property(x => x.PurposeOfVisit).HasColumnName("purpose_of_visit");
            builder.Property(x => x.SalePointId).HasColumnName("sale_point_id");
            builder.Property(x => x.Status).HasColumnName("status");
            builder.Property(x => x.VisitDt).HasColumnName("visit_dt");
            builder.Property(x => x.ClosedDt).HasColumnName("closed_dt");
            builder.Property(x => x.InsertedUserId).HasColumnName("inserted_user_id");
            builder.Property(x => x.SaleManagerId).HasColumnName("assigned_user_id");


            builder.HasOne(x => x.SalePoint).WithMany().HasForeignKey(x => x.SalePointId);
            builder.HasOne(x => x.InsertedUser).WithMany().HasForeignKey(x => x.InsertedUserId);
            builder.HasOne(x => x.SaleManager).WithMany().HasForeignKey(x => x.SaleManagerId);
            builder.ToTable("crm_task");
        }
    }
}
