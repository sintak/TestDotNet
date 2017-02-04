using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ConfigurationTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            #region 多层结构的配置
            #endregion
            //IDictionary<string, string> source = new Dictionary<string, string>()
            //{
            //    ["Ele1"] = "value1",
            //    ["Ele2:Sub1"] = "value2.1",
            //    ["Ele2:Sub2"] = "value2.2"
            //};

            //IConfiguration config = new ConfigurationBuilder().Add(new MemoryConfigurationSource() { InitialData = source }).Build();
            //Console.WriteLine($"Ele1: {config["Ele1"]}");
            //Console.WriteLine($"Ele2.Sub1: {config.GetSection("Ele2")["Sub1"]}");
            //Console.WriteLine($"Ele2.Sub2: {config.GetSection("Ele2")["Sub2"]}");

            //Console.ReadLine();

            #region 其它配置来源。xml文件、json文件或数据库等
            //IConfigurationBuilder builder = new ConfigurationBuilder();
            //builder.SetBasePath(Directory.GetCurrentDirectory());
            //builder.AddJsonFile("appsettings.json");

            //IConfiguration config = builder.Build();
            //Console.WriteLine($"Ele1: {config["Ele1"]}");
            //Console.WriteLine($"Ele2.Sub1: {config.GetSection("Ele2")["Sub1"]}");
            //Console.WriteLine($"Ele2.Sub2: {config.GetSection("Ele2")["Sub2"]}");

            //Console.ReadLine();
            #endregion

            #region Options对象映射
            // 创建DI容器，注册Options Pattern服务
            IServiceCollection services = new ServiceCollection();
            services.AddOptions();  // 注册Options Pattern服务。将配置内容注册到容器里，来获取对应的服务Provider对象。

            // 读取配置文件
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            IConfiguration config = builder.Build();

            // 通过注册的服务获取最终映射的配置对象
            IServiceProvider serviceProvider = services.Configure<ConfigOptions>(config).BuildServiceProvider();
            ConfigOptions options = serviceProvider.GetService<IOptions<ConfigOptions>>().Value;

            Console.WriteLine($"Ele1: {options.Ele1}");
            Console.WriteLine($"Ele2.Sub1: {options.Ele2.Sub1}");
            Console.WriteLine($"Ele2.Sub2: {options.Ele2.Sub2}");

            Console.ReadLine();
            #endregion
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
