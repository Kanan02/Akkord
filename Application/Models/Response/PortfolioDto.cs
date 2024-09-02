using Application.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models.Response
{
    public class PortfolioDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public PortfolioDto(ProductPortfolio portfolio)
        {
            Id = portfolio.Id;
            Name = portfolio.Name;
        }
    }
}
