using Application.Entities.Security;
using Application.Interfaces.IServices.Base;
using Application.Models.Request;
using Application.Models.Request.Security;
using Application.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IServices.Security
{
    public interface IUserService : IBaseService<User>
    {
        Task<LoginRes> SignIn(UserReq req);
    }
}
