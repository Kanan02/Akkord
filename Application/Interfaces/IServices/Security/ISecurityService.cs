using Application.Entities.Security;using System;
using System.Collections.Generic;

namespace Application.Interfaces.IServices.Security
{
    public interface ISecurityService
    {
        string GenerateToken(User user);
        List<string> GetCurrRoles();
        Guid GetCurrUserId();
        bool IsHaveRole(string roleName);
    }
}
