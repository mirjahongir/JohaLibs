using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics;

namespace WebAdmin
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //AppConfig.Start.CheckArg(args);
            Console.Title = "Web Admin";
            Console.WriteLine($@"Process Id: {Process.GetCurrentProcess().Id}");
            CreateHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateHostBuilder(string[] args) =>
              WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://*:2030")
                .UseStartup<Startup>();
    }
}
