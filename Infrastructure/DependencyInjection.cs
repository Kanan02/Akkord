using Application.Helpers;
using Application.Interfaces.ICommon;
using Application.Interfaces.IRepository;
using Application.Interfaces.IRepository.Base;
using Application.Interfaces.IServices.Security;
using Application.Interfaces.IUoW;
using Application.Models.AppSetting;
using Infrastructure.Common;
using Infrastructure.Data.Context;
using Infrastructure.Data.UntOfWork;
using Infrastructure.Repository;
using Infrastructure.Repository.Base;
using Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAkkordInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
            ConfigureJWT(services, configuration);

            services.AddDbContextPool<AkkordDbContext>(options =>
                    options.UseMySql(configuration["ConnectionStrings:Prod"]));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUoW, UoW>();

            // security services
            services.AddScoped<ISecurityService, SecurityService>();

            // common services
            services.AddScoped<IExcellService, ExcellService>();

            return services;
        }

        private static void ConfigureJWT(IServiceCollection services, IConfiguration configuration)
        {
            var jwtSetting = AppSettingHelper.BindSetting<JwtSetting>(configuration);
            var key = Encoding.ASCII.GetBytes(jwtSetting.SecretKey);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(x =>
           {
               x.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   ValidIssuer = jwtSetting.Issuer,
                   ValidAudience = jwtSetting.Issuer,
                   
                   IssuerSigningKey = new SymmetricSecurityKey(key)
               };
           });
        }
    }
}
