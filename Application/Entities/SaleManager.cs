using Application.Entities.Base;
using Application.Entities.Security;
using Application.Enums;
using Application.Exceptions;
using Application.Helpers;
using Application.Models.Request.Ui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Entities
{
    public class SaleManager : Base<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Msisdn { get; set; }
        public SaleSegment SaleSegment { get; set; }
        public ActivationStatus Status { get; set; }
        public User User { get; set; }
        public List<SaleManagerRegion> SaleMangerRegions { get; set; }
        public List<SalePoint> SalePoints { get; set; }

        public void ValidateSave()
        {
            if (ReflectionHelper.GetEnumValues<SaleSegment>().All(x => x != SaleSegment))
                throw new AkkordException("sale_segment_required");

            if(Id == Guid.Empty)
            {
                if (User == null || string.IsNullOrEmpty(User.Username) || string.IsNullOrEmpty(User.Password))
                    throw new AkkordException("username_and_password_required");
            }

        }
    }
}
