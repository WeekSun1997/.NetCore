using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Reflection;
using System.Threading.Tasks;
using EntityFramework.Core.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MysqlEntity.Core.Model;
using WebSocketMagger;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Interface;
using DBServer;
using ExceptionMiddleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using JWTMiddleware;
using Cache;
using Autofac.Core;
using Autofac;
using MongoDB;
using Autofac.Extensions.DependencyInjection;
using AutoFacAop;
using Autofac.Extras.DynamicProxy;
using Microsoft.AspNetCore.Hosting.Server.Features;
using IdentityConfig;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {

            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json")
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            builder.AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        //public void ConfigureServices(IServiceCollection services)
        //{

        //    services.AddSingleton<WebSocketBase>();
        //    services.AddSingleton<WebSocketHandler>();
        //    services.AddSingleton<JwtJsonModel>();
        //    services.AddSingleton<IDBServices, MysqlDBServices>(x => new MysqlDBServices(Configuration.GetConnectionString("MySqlStr")));
        //    services.AddSingleton<ICache, RedisServer>(option => new RedisServer()
        //    {
        //        _connectionString = "127.0.0.1:6379",//Configuration["Redis:_connectionString"],
        //        _defaultDB = 0
        //    }
        //);
        //    services.Configure<CookiePolicyOptions>(options =>
        //    {
        //        options.CheckConsentNeeded = context => false;
        //        options.MinimumSameSitePolicy = SameSiteMode.None;
        //    });
        //    var JwtKey = Encoding.UTF8.GetBytes(Configuration["JwtStr:JwtKey"]);
        //    var Issuer = Configuration["JwtStr:ValidIssuer"];
        //    var Audience = Configuration["JwtStr:ValidAudience"];
        //    JwtJsonModel.JwtKey = Configuration["JwtStr:JwtKey"];
        //    JwtJsonModel.Issuer = Issuer;
        //    JwtJsonModel.Claims = Audience;

        //    services.AddSession(options =>
        //    {
        //        options.Cookie.Name = ".AdventureWorks.Session";
        //        options.IdleTimeout = System.TimeSpan.FromSeconds(20000);//设置session的过期时间
        //        options.Cookie.HttpOnly = true;//设置在浏览器不能通过js获得该cookie的值
        //    });
        //    services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        //    services.AddHttpContextAccessor();
        //    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        //    .AddJwtBearer(jwtBearerOptions =>
        //    {
        //        jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
        //        {
        //            ValidateActor = true,
        //            ValidateAudience = true,
        //            ValidateLifetime = true,
        //            ValidateIssuerSigningKey = true,
        //            ValidIssuer = Issuer,
        //            ValidAudience = Audience,
        //            IssuerSigningKey = new SymmetricSecurityKey(JwtKey),
        //            ValidateIssuer = true,
        //        };
        //    });
        //    services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        //    services.AddSwaggerGen(option =>
        //    {

        //        option.SwaggerDoc("refuge", new Microsoft.OpenApi.Models.OpenApiInfo
        //        {
        //            Title = "Web接口",
        //            Version = "版本1"
        //        });
        //        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        //        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        //        // 启用xml注释. 该方法第二个参数启用控制器的注释，默认为false.
        //        option.IncludeXmlComments(xmlPath, true);

        //    });
        //    WebContext.WebContextStr = Configuration.GetConnectionString("SqlStr");
        //    //webdevContext.MySqlContextStr = Configuration.GetConnectionString("MysqlStr");
        //    MongoDBServer.MongoDbConnectionString = Configuration["MongoDB:MongDbString"];
        //    MongoDBServer.MongoDbDataBase = Configuration["MongoDB:DataBase"];
        //    services.AddOptions();//提供依赖注入
        //}


        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var builder = new ContainerBuilder();
            services.AddSingleton<WebSocketBase>();
            services.AddSingleton<WebSocketHandler>();
            services.AddSingleton<JwtJsonModel>();
            services.AddSingleton<IDBServices, MysqlDBServices>(x => new MysqlDBServices(Configuration.GetConnectionString("MySqlStr")));
            services.AddSingleton<ICache, RedisServer>(option => new RedisServer()
            {
                _connectionString = "127.0.0.1:6379",//Configuration["Redis:_connectionString"],
                _defaultDB = 0
            }
        );
           // services.AddIdentityServer()
           //.AddDeveloperSigningCredential()
           //.AddInMemoryClients(Config.GetClients())
           //.AddInMemoryApiResources(Config.GetSoluction());
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            var JwtKey = Encoding.UTF8.GetBytes(Configuration["JwtStr:JwtKey"]);
            var Issuer = Configuration["JwtStr:ValidIssuer"];
            var Audience = Configuration["JwtStr:ValidAudience"];
            JwtJsonModel.JwtKey = Configuration["JwtStr:JwtKey"];
            JwtJsonModel.Issuer = Issuer;
            JwtJsonModel.Claims = Audience;

            services.AddSession(options =>
            {
                options.Cookie.Name = ".AdventureWorks.Session";
                options.IdleTimeout = System.TimeSpan.FromSeconds(20000);//设置session的过期时间
                options.Cookie.HttpOnly = true;//设置在浏览器不能通过js获得该cookie的值
            });
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHttpContextAccessor();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(jwtBearerOptions =>
            {
                jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateActor = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Issuer,
                    ValidAudience = Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(JwtKey),
                    ValidateIssuer = true,
                };
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSwaggerGen(option =>
            {

                option.SwaggerDoc("refuge", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Web接口",
                    Version = "版本1"
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                // 启用xml注释. 该方法第二个参数启用控制器的注释，默认为false.
                option.IncludeXmlComments(xmlPath, true);

            });
            WebContext.WebContextStr = Configuration.GetConnectionString("SqlStr");
            //webdevContext.MySqlContextStr = Configuration.GetConnectionString("MysqlStr");
            MongoDBServer.MongoDbConnectionString = Configuration["MongoDB:MongDbString"];
            MongoDBServer.MongoDbDataBase = Configuration["MongoDB:DataBase"];
            services.AddScoped<LogAop>();
            builder.RegisterType<RedisCacheAop>();
            builder.RegisterType<CacheBase>();
            builder.Populate(services);
            Assembly assembly = Assembly.LoadFrom(@"G:\Program Files (x86)\Web\Web\BaseBill\bin\Debug\netcoreapp2.2\BaseServer.dll");
            builder.RegisterAssemblyTypes(assembly)
                .AsImplementedInterfaces()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(LogAop))
                .InterceptedBy(typeof(RedisCacheAop));

            var application = builder.Build();

            return new AutofacServiceProvider(application);
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSwagger();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseCors(options => options
           .WithOrigins("http://127.0.0.1:44303")
           .AllowAnyHeader()
           .AllowAnyMethod()
           .AllowCredentials()
           );
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/refuge/swagger.json", "My API 1.0.1");//注意,中间那段的名字 (refuge) 要和 上面 SwaggerDoc 方法定义的 名字 (refuge)一样
                s.RoutePrefix = string.Empty; //默认值是 "swagger" ,需要这样请求:https://localhost:44384/swagger
            });
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseWebSockets();
            //app.UseIdentityServer();
            app.UseMiddleware<WebSocketMiddlerware>();
            app.UseMiddleware<ExceptionMiddlewareModel>();
            app.UseMiddleware<JwtMiddleware>();
            app.UseMvc();
        }
    }
}
