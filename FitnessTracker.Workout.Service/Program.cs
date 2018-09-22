using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
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
                 hostingContext.HostingEnvironment.ConfigureNLog("NLog.Config");
                 logging.AddProvider(new NLogLoggerProvider());
                 logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                 logging.AddConsole();
                 logging.AddDebug();
             })
             .UseNLog()
             .UseStartup<Startup>();
    }
}