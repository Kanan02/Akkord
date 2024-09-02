using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models.Request.Base
{
    public class BaseReq<T>
    {
        public T Value { get; set; }
    }
}
