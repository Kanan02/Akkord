using Application.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Entities.Security
{
    public class User : Base<Guid>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public List<UserRole> UserRoles { get; set; }
    }
}
