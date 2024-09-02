using Application.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Entities
{
    public class CitiesOfBaku : Base<int>
    {
        public string Name { get; set; }
    }
}
