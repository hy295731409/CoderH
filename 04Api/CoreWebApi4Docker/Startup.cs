using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Implement;
using Domain.Interface;
using Domain.Object.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

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

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(option =>
                {
                    //限定认证操作是否必须通过https来做
                    option.RequireHttpsMetadata = false;
                    //决定token在认证完成后，是否需要保持到上下文里并向后传
                    option.SaveToken = true;
                    var token = Configuration.GetSection("tokenParameter").Get<TokenParameter>();
                    option.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(token.Secret)),
                        ValidIssuer = token.Issuer,
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        //如果token的过期时间设置得小于5分钟，想要让认证对这个时间生效，需加上下面这一行
                        //ClockSkew = TimeSpan.Zero
                    };
                });

            RegisterSwagger(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //中间件（短路）
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync(Process.GetCurrentProcess().ProcessName);
            //});

            app.UseRouting();

            //先身份验证（控制器打[Authorize]）
            app.UseAuthentication();
            //后授权
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

        /// <summary>
        /// 注册swagger
        /// </summary>
        /// <param name="services"></param>
        public void RegisterSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Version = "v1",
                    Title = "API文档"
                });
                option.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Version = "v2",
                    Title = "API文档"
                });
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);
                var xmlPath = Path.Combine(basePath, "CoreWebApi4Docker.xml");
                option.IncludeXmlComments(xmlPath);

                var xmlPath2 = Path.Combine(basePath, "Domain.xml");
                option.IncludeXmlComments(xmlPath2);

                //配置swagger支持JWT
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = ""
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });
        }
    }
}
