using Application.Entities.Base;
using Application.Entities.Security;
using Application.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Entities
{
    public class Notification : Base<int>
    {
        public string Content { get; set; }
        //public bool IsRead { get; set; }
        public int? SalePointId { get; set; }
        public int? CrmTaskId { get; set; }
        public NotificationStatus Status { get; set; }
        //public Guid SaleManagerId { get; set; }
        public SalePoint SalePoint { get; set; }
        public CrmTask CrmTask { get; set; }
        //public SaleManager SaleManager { get; set; }

        public Guid FromId { get; set; }
        public Guid ToId { get; set; }
        public User From { get; set; }
        public User To { get; set; }

    }
}
