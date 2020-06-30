using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    logging.AddConsole();
                    //Debug.WriteLine 方法调用）来写入日志输出。
                    //在 Linux 中，此提供程序将日志写入 / var / log / message。
                    //logging.AddDebug();
                    //在 Windows 中，它使用 ETW。 提供程序可跨平台使用，但尚无支持 Linux 或 macOS 的事件集合和显示工具。
                    //logging.AddEventSourceLogger();

                });

    }
}
