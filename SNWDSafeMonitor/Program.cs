using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
//using IWHR.Common;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.WindowsServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RedisHelper = ColinChang.RedisHelper.RedisHelper;

namespace SNWDSafeMonitor
{
    public class Program
    {
        public static void Main(string[] args)
        {

            //  CreateWebHostBuilder(args).Build().Run();

            IWebHost host = WebHost.CreateDefaultBuilder(args)
    .UseKestrel(options => { options.Listen(IPAddress.Any, 7001); }) //7001，如果是其他端口，修改
    .UseStartup<Startup>() //使用Startup配置类
    .Build();
            //Debug状态或者程序参数中带有--console都表示普通运行方式而不是Windows服务运行方式
            bool isService = !(Debugger.IsAttached || args.Contains("--console")); 
            if (isService)
            {
                host.RunAsService();
            }
            else
            {
                host.Run();
            }

        }

        /*
         * 创建服务：
                 sc create NSBDService binPath= "C:\program files\dotnet\dotnet.exe e:\publish\SNWDSafeMonitor.dll" DisplayName= "NSBDService" start= auto
                启动服务：
         sc start NSBDService

                停止服务：
        sc stop  NSBDService

                删除服务：
        sc delete NSBDService

        测试服务是否启动：
        http://IP:7001/swagger

            */




        //public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        //    WebHost.CreateDefaultBuilder(args)
        //    .UseUrls("http://*:7001")
        //        .UseStartup<Startup>();
    }
}
