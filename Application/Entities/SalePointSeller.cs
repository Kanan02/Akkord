using Application.Entities.Base;
using Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Entities
{
    public class SalePointSeller : Base<int>
    {
        public string Fullname { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Msisdn { get; set; }
        public int SalePointId { get; set; }
        public SalePoint SalePoint { get; set; }

        public void SalePointCreateValidate()
        {
            if (string.IsNullOrEmpty(Fullname))
                throw new AkkordException("seller_name_requried");

            if (string.IsNullOrEmpty(Msisdn))
                throw new AkkordException("seller_phone_requried");
        }
    }
}
