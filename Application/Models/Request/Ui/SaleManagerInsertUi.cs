using Application.Entities;
using Application.Entities.Security;
using Application.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Models.Request.Ui
{
    public class SaleManagerInsertUi  //: SaleManager
    {

        public Guid? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Msisdn { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public SaleSegment SaleSegment { get; set; }
        public ActivationStatus Status { get; set; }
        public int[] Regions { get; set; }

        public SaleManager GetManager()
        {
            var saleManager = new SaleManager
            {
                Id = Id == null ? Guid.Empty : Id.Value,
                FirstName = FirstName,
                LastName = LastName,
                Msisdn = Msisdn,
                SaleSegment = SaleSegment,
                Status = Status,
                User = new User
                {
                    Id = Id == null ? Guid.Empty : Id.Value,
                    Username = Username,
                    Password = Password
                }
            };
            saleManager.SaleMangerRegions = Regions.Select(x => new SaleManagerRegion
            {
                RegionId = x,
                SaleManagerId = Id == null ? Guid.Empty : Id.Value,
            }).ToList();

            return saleManager;
        }
    }
}
