using Application.Constants;
using Application.Entities.Security;
using Application.Helpers;
using Application.Interfaces.IServices.Security;
using Application.Models.AppSetting;
using Application.Services.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Security
{
    public class SecurityService : ISecurityService
    {
        public IConfiguration _configuration { get; set; }
        private IHttpContextAccessor _httpContextAccessor;

        public SecurityService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public string GenerateToken(User user)
        {
            var jwtSetting = AppSettingHelper.BindSetting<JwtSetting>(_configuration);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


            var claims = new List<Claim>()
            {
                new Claim(ClaimConstant.UserId, user.Id.ToString())
                //new Claim(ClaimTypes.Role, "")
            };

            SetRoleClaims(claims, user.UserRoles.Select(u => u.Role).ToList());

            var token = new JwtSecurityToken(jwtSetting.Issuer, jwtSetting.Issuer, claims, expires: DateTime.Now.AddHours(jwtSetting.ExpireDurationHour), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private void SetRoleClaims(List<Claim> claims, List<Role> roles)
            => claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r.Name)));

        public List<string> GetCurrRoles() => GetCurrentClaims().Where(c => c.Type == ClaimTypes.Role).Select(r => r.Value).ToList();
        private List<Claim> GetCurrentClaims() => _httpContextAccessor.HttpContext.User.Claims.ToList();

        public Guid GetCurrUserId() => new Guid(GetClaimValuByType(ClaimConstant.UserId));

        //private string GetClaimValuByType(string key) => GetCurrentClaims().FirstOrDefault(c => c.Type == key).Value;
        private string GetClaimValuByType(string key) => GetCurrentClaims().FirstOrDefault(c => c.Type == key)?.Value;

        public bool IsHaveRole(string roleName) => GetCurrRoles().Any(r => r == roleName);
    }
}
