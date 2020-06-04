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
                    //�޶���֤�����Ƿ����ͨ��https����
                    option.RequireHttpsMetadata = false;
                    //����token����֤��ɺ��Ƿ���Ҫ���ֵ��������ﲢ���
                    option.SaveToken = true;
                    var token = Configuration.GetSection("tokenParameter").Get<TokenParameter>();
                    option.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(token.Secret)),
                        ValidIssuer = token.Issuer,
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        //���token�Ĺ���ʱ�����õ�С��5���ӣ���Ҫ����֤�����ʱ����Ч�������������һ��
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
            //�м������·��
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync(Process.GetCurrentProcess().ProcessName);
            //});

            app.UseRouting();

            //�������֤����������[Authorize]��
            app.UseAuthentication();
            //����Ȩ
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
        /// ע��swagger
        /// </summary>
        /// <param name="services"></param>
        public void RegisterSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Version = "v1",
                    Title = "API�ĵ�"
                });
                option.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Version = "v2",
                    Title = "API�ĵ�"
                });
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);
                var xmlPath = Path.Combine(basePath, "CoreWebApi4Docker.xml");
                option.IncludeXmlComments(xmlPath);

                var xmlPath2 = Path.Combine(basePath, "Domain.xml");
                option.IncludeXmlComments(xmlPath2);

                //����swagger֧��JWT
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
