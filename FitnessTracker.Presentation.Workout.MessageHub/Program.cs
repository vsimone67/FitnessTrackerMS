using FitnessTracker.Common.Web.StartupConfig;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace FitnessTracker.Presentation.Workout.MessageHub
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