using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Domain.Implement;
using Domain.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CoreWebApi4Docker
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<IWeatherForecastService, WeatherForecastService>();

            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Version = "v1",
                    Title = "APIÎÄµµ"
                });
                option.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Version = "v2",
                    Title = "APIÎÄµµ"
                });
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);
                var xmlPath = Path.Combine(basePath, "CoreWebApi4Docker.xml");
                option.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync(Process.GetCurrentProcess().ProcessName);
            //});
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                //endpoints.MapAreaControllerRoute(
                //    name: "default",
                //    areaName: "",
                //    pattern: "{controller=Home}/{action=Index}/{id?}"
                //    );
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1 docs");
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "v2 docs");
            });
        }
    }
}
