using Application;
using Application.Config;
using Infrastructure;
using Infrastructure.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using Web.Middlewares;

namespace Web
{
    public class Startup
    {
        private readonly IConfiguration Configuration;
        //private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration config) //, IWebHostEnvironment env
        {
            Configuration = config;
            //_env = env;
            InitSettings();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(x =>
            {
                x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder

                        .AllowAnyOrigin()
                        //.AllowCredentials()
                        .AllowAnyHeader()
                        .SetIsOriginAllowed(_ => true)
                        .AllowAnyMethod();
                });
            });

            services.AddAkkordApplication(Configuration);
            services.AddAkkordInfrastructure(Configuration);
            //services.AddDbContextPool<AkkordDbContext>(options =>
            //       options.UseMySql(Configuration["ConnectionStrings:Test"]));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //app.UseDefaultFiles();
            
            app.UseCors();

            app.UseMiddleware(typeof(ApiErrorMiddleware));

            app.UseAuthentication();
            app.UseRouting();
            
            app.UseAuthorization();
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action}/{id?}");
            });
        }

        private void InitSettings()
        {
            ProjectSetting.PhotoSavePath = Configuration["ProjectSetting:PhotoSavePath"];
            ProjectSetting.PhotoPath = Configuration["ProjectSetting:PhotoPath"];
            ProjectSetting.TaskCloseDistance = Convert.ToDouble(Configuration["ProjectSetting:TaskCloseDistance"]);
        }

    }
}
