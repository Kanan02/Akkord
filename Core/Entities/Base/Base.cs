using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Base
{
    public class Base<TId>
    {
        public virtual TId Id { get; set; }
    }
}
