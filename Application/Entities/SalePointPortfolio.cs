using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Entities
{
    public class SalePointPortfolio
    {
        public int SalePointId { get; set; }
        public int PortfolioId { get; set; }
        public SalePoint SalePoint { get; set; }
        public ProductPortfolio Portfolio { get; set; }
    }
}
