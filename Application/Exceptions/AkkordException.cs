using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Exceptions
{
    public class AkkordException : Exception
    {
        public AkkordException(string ex) : base(ex) { }
    }
}
