using Application.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Entities
{
    public class ProductPortfolio : Base<int>
    {
        public string Name { get; set; }
        public List<SalePointPortfolio> SalePointPortfolioes { get; set; }
    }
}
