using FitnessTracker.Common.Web.StartupConfig;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
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
             .ConfigureNLog()
             .ConfigAppConfiguration()
             .UseStartup<Startup>();
    }
}