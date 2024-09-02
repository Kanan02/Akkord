using Application.Entities;
using Application.Models.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Application.Models.Request
{
    public class NotificationReq : PagingReq<Notification>
    {
        public bool  IncludeTask { get; set; }
        public bool  IncludeSalePoint { get; set; }
        public bool  IncludeSaleManager { get; set; }
        public bool? IsRead { get; set; }

        public Expression<Func<Notification, object>> orderByDescExpression;
    }
}
