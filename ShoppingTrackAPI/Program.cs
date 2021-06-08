using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ShoppingTrackAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            //string environmentName = "Development";
            //#if DEBUG
            //environmentName = "Development";
            //#elif RELEASE
            //environmentName = "Production";
            //#endif
            return Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                })
                //.UseEnvironment(environmentName)
                .ConfigureAppConfiguration((context, config) =>
                {
                    Console.WriteLine("Environment: {0}", context.HostingEnvironment.EnvironmentName);
                    config.AddSystemsManager($"/shopping-track-api/{context.HostingEnvironment.EnvironmentName}/",
                        reloadAfter: TimeSpan.FromMinutes(20));
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder/*.UseEnvironment(environmentName)*/.UseStartup<Startup>();
                });
        }
    }
}
