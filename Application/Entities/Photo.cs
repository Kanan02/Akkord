using Application.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Application.Entities
{
    public class Photo : Base<int>
    {
        public string Src { get; set; }
        public List<SalePoint> SalePoints { get; set; }
        public CrmTaskPhoto CrmPhoto { get; set; }
        public SalePointPhoto SalePointPhoto { get; set; }
        
        [NotMapped]
        public string Base64 { get; set; }

    }
}
