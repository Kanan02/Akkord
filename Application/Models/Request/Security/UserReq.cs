using Application.Entities.Security;
using Application.Models.Request.Base;
using Application.Models.Request.Ui;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models.Request.Security
{
    public class UserReq : BaseReq<User>
    {
        public bool IncludeRole { get; set; }
        //public bool IncludeManager { get; set; }
        public UserReq()  {}

        public UserReq(SignInUi req)
        {
            Value = new User
            {
                Username = req.Username,
                Password = req.Password
            };
        }
    }
}
