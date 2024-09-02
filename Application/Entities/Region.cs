using Application.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Entities
{
    public class Region : Base<int>
    {
        public string Name { get; set; }
        public List<SalePoint> SalePoints { get; set; }
        public List<SaleManagerRegion> SaleMangerRegions { get; set; }
    }
}
