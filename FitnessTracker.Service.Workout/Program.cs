using FitnessTracker.Common.Web.StartupConfig;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace FitnessTracker.Service.Workout
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
         WebHost.CreateDefaultBuilder(args)
             .ConfigureNLogFromEnvironment()
             .ConfigAppConfigurationFromEnvironment()
             .UseStartup<Startup>();
    }
}