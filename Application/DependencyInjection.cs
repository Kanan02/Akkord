using Application.Interfaces.IServices;
using Application.Interfaces.IServices.Base;
using Application.Interfaces.IServices.Security;
using Application.Services;
using Application.Services.Base;
using Application.Services.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAkkordApplication(this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
            services.AddScoped<ISaleManagerService, SaleManagerService>();
            services.AddScoped<ISalePointService, SalePointService>();
            services.AddScoped<IRegionService, RegionService>();

            services.AddScoped<ICrmTaskService, CrmTaskService>();
            services.AddScoped<INotificationService, NotificationService>();


            // security
            services.AddScoped<IUserService,UserService>();
            services.AddScoped<IRoleService,RoleService>();
            services.AddScoped<IAccessValidator,AccessValidator>();
            
            return services;
        }

        

    }
}
