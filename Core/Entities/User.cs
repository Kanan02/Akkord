using Core.Entities.Base;
using Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class User : Base<Guid>
    {
        public string Fullname { get; set; }
        public string Mobile { get; set; }
        public string Password { get; set; }
        public SaleSegment SaleSegment { get; set; }
    }
}
