using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Seller : Base<int>
    {
        public string Fullname { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Number { get; set; }
        public int SalePointId { get; set; }
        public SalePoint SalePoint { get; set; }
    }
}
