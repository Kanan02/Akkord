using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Entities
{
    public class CrmTaskPhoto
    {
        public int CrmTaskId { get; set; }
        public int PhotoId { get; set; }
        public CrmTask CrmTask { get; set; }
        public Photo Photo { get; set; }
    }
}
