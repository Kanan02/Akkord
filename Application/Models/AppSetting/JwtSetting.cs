using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models.AppSetting
{
    public class JwtSetting
    {
        public string Issuer { get; set; }
        public string SecretKey { get; set; }
        public int ExpireDurationHour { get; set; }
    }
}
