using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Application.Entities
{
    public class SalePointPhoto
    {
        public int SalePointId { get; set; }
        public int PhotoId { get; set; }
        public SalePoint SalePoint { get; set; }
        public Photo Photo { get; set; }
        [NotMapped]
        public string Base64 { get; set; }
        [NotMapped]
        public string Image { get; set; }
    }
}
