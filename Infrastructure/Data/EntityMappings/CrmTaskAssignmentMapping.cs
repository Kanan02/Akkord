//using Application.Entities;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Infrastructure.Data.EntityMappings
//{
//    public class CrmTaskAssignmentMapping : IEntityTypeConfiguration<CrmTaskAssignment>
//    {
//        public void Configure(EntityTypeBuilder<CrmTaskAssignment> builder)
//        {
//            builder.HasKey(x => x.Id);
//            builder.Property(x => x.Id).HasColumnName("id");

//            builder.Property(x => x.SenderId).HasColumnName("sender_id");
//            builder.Property(x => x.ReceiverId).HasColumnName("receiver_id");
//            builder.Property(x => x.Status).HasColumnName("status");

//            builder.HasOne(x => x.Sender).WithMany().HasForeignKey(x => x.SenderId);
//            builder.HasOne(x => x.Receiver).WithMany().HasForeignKey(x => x.ReceiverId);

//            builder.HasOne(x => x.CrmTask).WithOne().HasForeignKey<CrmTaskAssignment>(x => x.Id);

//            builder.ToTable("crm_task_assignment");
//        }
//    }
//}
