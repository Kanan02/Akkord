using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Entities
{
    public class SaleManagerRegion
    {
        public Guid SaleManagerId { get; set; }
        public int RegionId { get; set; }
        public SaleManager SaleManager { get; set; }
        public Region Region { get; set; }
    }
}
