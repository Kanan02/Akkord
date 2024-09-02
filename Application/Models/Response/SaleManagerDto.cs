using Application.Entities;
using Application.Enums;
using Application.Models.Response.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models.Response
{
    public class SaleManagerDto : IListFilterDto<SaleManager, SaleManagerDto>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Msisdn { get; set; }
        public SaleSegment SaleSegment { get; set; }
        public ActivationStatus Status { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<int> Regions { get; set; } 

        public SaleManagerDto SetDto(SaleManager entity)
        {
            Id = entity.Id;
            FirstName = entity.FirstName;
            LastName = entity.LastName;
            Msisdn = entity.Msisdn;
            SaleSegment = entity.SaleSegment;
            Status = entity.Status;
            entity.SaleMangerRegions?.ForEach(sm => {
                if(sm.Region!=null)
                    Regions.Add(sm.RegionId);
            });
            Username = entity.User?.Username;
            Password = entity.User?.Password;
            return this;
        }

        public SaleManagerDto() =>  Regions = new List<int>();
        
    }
}
