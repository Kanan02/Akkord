using Application.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Entities.Security
{
    public class Role : Base<int>
    {
        public string Name { get; set; }
        public List<UserRole> UserRoles { get; set; }
    }
}
