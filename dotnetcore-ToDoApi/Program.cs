using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace dotnetcore_ToDoApi
{
    public class Program
    {
        public static Dictionary<string, string> arrayDict = new Dictionary<string, string>
        {
            {"array:entries:0", "value0"},
            {"array:entries:1", "value1"},
            {"array:entries:2", "value2"},
            {"array:entries:4", "value4"},
            {"array:entries:5", "value5"}
        };

        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File(
                    @"C:\logs\dotnetcore\app.log",
                    fileSizeLimitBytes: 1_000_000,
                    rollOnFileSizeLimit: true,
                    shared: true,
                    flushToDiskInterval: TimeSpan.FromSeconds(1))
                .CreateLogger();

            try
            {
                Log.Information("Starting web host");
                CreateWebHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");  
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>

            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.SetBasePath(Directory.GetCurrentDirectory());
                    config.AddInMemoryCollection(arrayDict);
                    config.AddJsonFile("json_array.json", optional: false, reloadOnChange: false);
                    config.AddJsonFile("starship.json", optional: false, reloadOnChange: false);
                    config.AddXmlFile("tvshow.xml", optional: false, reloadOnChange: false);
                     //onfig.AddEFConfiguration(options => options.UseInMemoryDatabase("InMemoryDb"));
                    config.AddCommandLine(args);
                })
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders(); 
                    logging.AddDebug();
                    logging.AddConsole(); 
                })
                .UseSerilog()
                .UseStartup<Startup>();
    }
}
