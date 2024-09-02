using Application.Config;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Helpers
{
    public static class CoordinateHelper
    {
        public static bool IsValidLocation(double lat1, double lon1, double lat2, double lon2) =>
            GetDistance(lat1, lon1, lat2, lon2) <= ProjectSetting.TaskCloseDistance;

        static double GetDistance(double lat1, double lon1, double lat2, double lon2)
        {
            var R = 6371;
            var dLat = ToRadians(lat2 - lat1);
            var dLon = ToRadians(lon2 - lon1);
            var a =
                Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var d = R * c; // Distance in km
            return d;
        }

        static double ToRadians(double deg)
        {
            return deg * (Math.PI / 180);
        }
    }
}
