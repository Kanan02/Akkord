using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class ProductPortfolio : Base<int>
    {
        public string Name { get; set; }
    }
}
