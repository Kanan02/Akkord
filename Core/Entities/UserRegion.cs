using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class UserRegion
    {
        public Guid UserId { get; set; }
        public int RegionId { get; set; }
        public User User { get; set; }
        public Region Region { get; set; }
    }
}
