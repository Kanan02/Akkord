using Application.Entities;
using Application.Entities.Security;
using Application.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models.Response
{
    public class LoginRes
    {
        public List<string> Roles { get; set; }
        public string Token { get; set; }
        public string Username { get; set; }
        public SaleSegment SaleSegment { get; set; }
        //public SaleManagerDto SaleManager { get; set; }
        public LoginRes(){}

        public LoginRes(string token,List<string> roles,User user,SaleManager manager)
        {
            Token = token;
            Roles = roles;
            Username = user?.Username;
            if (manager != null)
                SaleSegment = manager.SaleSegment;
        }
    }
}
