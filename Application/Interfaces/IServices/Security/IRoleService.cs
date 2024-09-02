using Application.Entities.Security;
using Application.Interfaces.IServices.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IServices.Security
{
    public interface IRoleService : IBaseService<Role>
    {
        Task<Role> GetByName(string name);
    }
}
