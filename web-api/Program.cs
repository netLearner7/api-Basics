using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace web.api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>//这个lambda不要动因为efcore要用，动了可能会影响数据库迁移
            WebHost.CreateDefaultBuilder(args)
                //程序启动时就调用startup这个类
                .UseStartup<Startup>()
                .Build();
    }
}
