﻿using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Photo : Base<int>
    {
        public string Src { get; set; }
    }
}
