using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

namespace Inventory.Api
{
    public class Program
    {
        protected Program()
        {
        }

        private static void Main(string[] args)
        {
            try
            {
                Log.Information("Application starting up...");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception exception)
            {
                Log.Fatal(exception, "The application failes to start correctly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IWebHostBuilder CreateHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
            .CaptureStartupErrors(true)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true);
            })
            .UseSerilog((hostingContext, loggerConfiguration) =>
            {
                loggerConfiguration.ReadFrom
                .Configuration(hostingContext.Configuration)
                .Enrich.FromLogContext();
            });
    }
}