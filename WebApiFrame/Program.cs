using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using WebApiFrame.Repository;

namespace WebApiFrame
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                //.ConfigureServices(  // @@@DI方式2 注册接口和实现类的映射关系
                //    services => {
                //        // 注册接口和实现类的映射关系
                //        services.AddScoped<IUserRepository, UserRepository>();
                //    }
                //)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
