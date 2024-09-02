using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Helpers
{
    public class AppSettingHelper
    {
        public static T BindSetting<T>(IConfiguration configuration)
        {
            var obj = (T)Activator.CreateInstance(typeof(T));
            configuration.Bind(typeof(T).Name, obj);
            return obj;
        }
    }
}
