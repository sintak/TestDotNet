using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using WebApiFrame.Core.Middlewares;
using WebApiFrame.Core.Filters;
using WebApiFrame.Repository;
using WebApiFrame.Controllers;
using Autofac;
using Autofac.Extensions.DependencyInjection;

namespace WebApiFrame
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();  // 注册Options Pattern服务。将配置内容注册到容器里，来获取对应的服务Provider对象。
            services.Configure<ConfigOptions>(Configuration.GetSection("CfgContent"));

            // Add framework services.
            services.AddMvc(
                options => {
                    // 过滤器加到全局

                    options.Filters.Add(typeof(SimpleActionFilterAttribute));
                    options.Filters.Add(typeof(SimpleExceptionFilterAttribute));
                    options.Filters.Add(typeof(SimpleResourceFilterAttribute));
                    options.Filters.Add(typeof(SimpleResultFilterAttribute));

                    // ### 实例注册
                    //options.Filters.Add(new MyActionFilterAttribute());
                    options.Filters.Add(new MyActionFilterAttribute("Global"));

                    // ### 类型注册
                    //options.Filters.Add(typeof(MyActionFilterAttribute));
                }
                );

            // ### 通过ServiceFilter引用
            // 将过滤器类型添加到DI容器里
            //services.AddScoped<MyActionFilterAttribute>();

            // @@@DI方式1 注册接口和实现类的映射关系
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<ITestOne, TestOne>();
            services.AddScoped<ITestTwo, TestTwo>();
            services.AddScoped<ITestThree, TestThree>();

            //services.AddTransient<ITestTransient, TestInstance>();  // transient 瞬时。 每次注入时都生成一个新的实例
            //services.AddScoped<ITestScoped, TestInstance>();  // scoped 域内。 在每一次请求处理流程中创建一个新的实例
            //services.AddSingleton<ITestSingleton, TestInstance>();  // singleton 单例。 第一次注入时创建实例，全应用程序内任何时候都共享这个唯一实例。
            //services.AddTransient<TestService, TestService>();

            //// 第三方DI容器 autofac容器
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<TestInstance>().As<ITestTransient>().InstancePerDependency();
            containerBuilder.RegisterType<TestInstance>().As<ITestScoped>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<TestInstance>().As<ITestSingleton>().SingleInstance();
            containerBuilder.RegisterType<TestService>().AsSelf().InstancePerDependency();
            containerBuilder.Populate(services);

            var container = containerBuilder.Build();
            return container.Resolve<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // 设置日志最小输出级别为Error
            loggerFactory.WithFilter(
                new FilterLoggerSettings()
                {
                    // 设置以命名空间开头的日志的最小输出级别
                    // （这些命名空间打印的日志才会输出）
                    { "Microsoft", LogLevel.Warning },
                    //{ "WebApiFrame", LogLevel.Error }
                }
            ).AddConsole();
            //loggerFactory.AddConsole(Configuration.GetSection("Logging"));

            loggerFactory.AddDebug();

            //loggerFactory.AddNLog();


            // 添加自定义中间件
            //app.Use(async (context,next) =>
            //{
            //    await context.Response.WriteAsync("Hello World too!");
            //    await next();  // ### 通过Use方法注册的中间件，如果不调用next方法，效果等同于Run方法。当调用next方法后，此中间件处理完后将请求传递下去，由后续的中间件继续处理
            //});

            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
            // or
            //app.UseMiddleware<HelloworldMiddleware>();
            //app.UseMiddleware<HelloworldTooMiddleware>();

            //app.Map("/test", MapTest);  // Map方法主要通过请求路径和其他自定义条件过滤来指定注册的中间件，看起来更像一个路由。
            app.MapWhen(
                context => {
                    return context.Request.Query.ContainsKey("a");  // 只有当请求参数中含有a时，页面才正常显示内容。
                },
                MapTest
                );

            app.UseVisitLogMiddleware();

            app.UseMvc();
        }

        private static void MapTest(IApplicationBuilder app) {
            app.Run(
                async context => {
                    await context.Response.WriteAsync("Url is " + context.Request.PathBase.ToString());
                }
                );
        }
    }

    public class ConfigOptions
    {
        public string Ele1 { get; set; }
        public SubConfigOptions Ele2 { get; set; }
    }

    public class SubConfigOptions
    {
        public string Sub1 { get; set; }
        public string Sub2 { get; set; }
    }
}
