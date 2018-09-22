using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog.Web;

namespace FitnessTracker.Workout.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
         WebHost.CreateDefaultBuilder(args)

             .UseHealthChecks("/hc")    // ADD LINK TO HEALTHCHECKS
             .ConfigureLogging((hostingContext, logging) =>
             {
                 //hostingContext.HostingEnvironment.ConfigureNLog("/appsettings/NLog.Config"); // common settings are in the /appsettings folder
                 hostingContext.HostingEnvironment.ConfigureNLog("NLog.Config"); // common settings are in the /appsettings folder
                 logging.AddProvider(new NLogLoggerProvider());
                 logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                 logging.AddConsole();
                 logging.AddDebug();
             })
             .ConfigureAppConfiguration((builderContext, config) =>
             {
                 var env = builderContext.HostingEnvironment;

                 //config.SetBasePath("/appsettings");  // set path to docker volume for common files
                 config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                 config.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

                 config.AddEnvironmentVariables();
             })
             .UseStartup<Startup>();
    }
}