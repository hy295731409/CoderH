using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using CoreWebApi4Docker.Handler;
using Domain.Implement;
using Domain.Interface;
using Domain.Object.Auth;
using Framework.DB.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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

            //①自带jwt鉴权
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddJwtBearer(option =>
            //    {
            //        //限定认证操作是否必须通过https来做
            //        option.RequireHttpsMetadata = false;
            //        //决定token在认证完成后，是否需要保持到上下文里并向后传
            //        option.SaveToken = true;
            //        var token = Configuration.GetSection("tokenParameter").Get<TokenParameter>();
            //        option.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            ValidateIssuerSigningKey = true,
            //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(token.Secret)),
            //            ValidIssuer = token.Issuer,
            //            ValidateIssuer = true,
            //            ValidateAudience = false,
            //            //如果token的过期时间设置得小于5分钟，想要让认证对这个时间生效，需加上下面这一行
            //            //ClockSkew = TimeSpan.Zero
            //        };
            //    });


            //②添加策略鉴权模式
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Permission", policy => policy.Requirements.Add(new PolicyRequirement()));
            })
            .AddAuthentication(s =>
            {
                //添加JWT Scheme
                s.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                s.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                s.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            //添加jwt验证：
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateLifetime = true,//是否验证失效时间
                    ClockSkew = TimeSpan.FromSeconds(30),

                    ValidateAudience = true,//是否验证Audience
                    //ValidAudience = Const.GetValidudience(),//Audience
                    //这里采用动态验证的方式，在重新登陆时，刷新token，旧token就强制失效了
                    AudienceValidator = (m, n, z) =>
                    {
                        return m != null && m.FirstOrDefault().Equals(Const.ValidAudience);
                    },
                    ValidateIssuer = true,//是否验证Issuer
                    ValidIssuer = Const.Domain,//Issuer，这两项和前面签发jwt的设置一致

                    ValidateIssuerSigningKey = true,//是否验证SecurityKey
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Const.SecurityKey))//拿到SecurityKey
                };
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        //Token expired
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
            });
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo
            //    {
            //        Version = "v1",
            //        Title = "HaoZi JWT",
            //        Description = "基于.NET Core 3.0 的JWT 身份验证",
            //        Contact = new OpenApiContact
            //        {
            //            Name = "zaranet",
            //            Email = "zaranet@163.com",
            //            Url = new Uri("http://cnblogs.com/zaranet"),
            //        },
            //    });
            //    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            //    {
            //        Description = "在下框中输入请求头中需要添加Jwt授权Token：Bearer Token",
            //        Name = "Authorization",
            //        In = ParameterLocation.Header,
            //        Type = SecuritySchemeType.ApiKey,
            //        BearerFormat = "JWT",
            //        Scheme = "Bearer"
            //    });
            //    c.AddSecurityRequirement(new OpenApiSecurityRequirement
            //    {
            //        {
            //            new OpenApiSecurityScheme
            //            {
            //                Reference = new OpenApiReference {
            //                    Type = ReferenceType.SecurityScheme,
            //                    Id = "Bearer"
            //                }
            //            },
            //            new string[] { }
            //        }
            //    });
            //});
            //认证服务
            services.AddSingleton<IAuthorizationHandler, PolicyHandler>();

            RegisterSwagger(services);

            #region 自带的DI
            //• 瞬时（Transient） 对象总是不同的；向每一个控制器和每一个服务提供了一个新的实例
            //services.AddTransient<IWeatherForecastService, WeatherForecastService>();
            //• 作用域（Scoped） 对象在一次请求中是相同的，但在不同请求中是不同的
            //services.AddScoped<IWeatherForecastService, WeatherForecastService>();
            //• 单例（Singleton） 对象对每个对象和每个请求是相同的（无论是否在 ConfigureServices 中提供实例）
            //services.AddSingleton<IWeatherForecastService, WeatherForecastService>(); 
            #endregion

            #region core 2.x 使用autofac
            ////创建 Autofac 容器
            //var containerBuilder = new ContainerBuilder();
            //containerBuilder.Populate(services);
            ////将 UserService 类作为 IUserService 的实现进行注册
            //containerBuilder.RegisterType<WeatherForecastService>().As<IWeatherForecastService>().InstancePerLifetimeScope();
            //var container = containerBuilder.Build();
            ////接管内置的容器
            //return new AutofacServiceProvider(container); 
            #endregion
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

        /// <summary>
        /// core3.x 使用autofac注入
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            //单个指定注册
            //builder.RegisterType<WeatherForecastService>().As<IWeatherForecastService>().InstancePerLifetimeScope();

            //批量注册
            var path = AppDomain.CurrentDomain.RelativeSearchPath;
            if (!Directory.Exists(path))
                path = AppDomain.CurrentDomain.BaseDirectory;
            //var files = Directory.GetFiles(path, "*.*").Where(s => s.EndsWith(".dll") || s.EndsWith(".exe"));exe文件转成assembly会抛异常
            var asses = Directory.GetFiles(path, "*.*").Where(s => s.EndsWith(".dll")).Select(Assembly.LoadFrom).ToList();
            var assemblies = Assembly.GetExecutingAssembly();
            asses.Add(assemblies);

            //方式①：找到 Domain 类所在的程序集中所有以 Service 命名的类型进行注册
            //var assembly = asses.Find(p => p.FullName.StartsWith("Domain"));
            //builder.RegisterAssemblyTypes(assembly)
            //方式②：注册所有程序集中所有以 Service 命名的类型
            //builder.RegisterAssemblyTypes(asses.ToArray())
            //.Where(t => t.Name.EndsWith("Service"))
            //.AsImplementedInterfaces()
            //.InstancePerLifetimeScope();

            //方式③：注册所有程序集中所有实现Idependency接口的类型
            builder.RegisterAssemblyTypes(asses.ToArray())
                .Where(type => typeof(IDependency).IsAssignableFrom(type) && !type.IsAbstract)
                .AsSelf() //自身服务，用于没有接口的类
                .AsImplementedInterfaces() //接口服务
                .PropertiesAutowired();//属性注入
        }
    }
}
