using FitnessTracker.Common.Web.StartupConfig;
using FitnessTracker.Presentation.Diet.MessageHub.StartupConfig;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;

namespace FitnessTracker.Presentation.Diet.MessageHub
{
    public class Startup
    {
        private readonly Container _container = new Container();

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCustomMvc()
              .AddHealthChecks(Configuration, false)
              .ConfigureDIContainer(_container)
              .RegisterAppSettings(Configuration)
              .AddEventBus(Configuration, _container, true)
              .AddSignalRServices()
              .RegisterEventHandlers(_container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.AddCorsConfiguration()
                .UseRequestTimings()
                .UseHealthChecks("/ready")
                .InitializeDIContainer(_container)
                .ConfigureSignalRHubs()
                .ConfigureEventBus();
        }
    }
}