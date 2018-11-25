using FitnessTracker.Common.Web.StartupConfig;
using FitnessTracker.Presentation.SignalRHub.StartupConfig;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;

namespace FitnessTracker.Presentation.SignalRHub
{
    public class Startup
    {
        private readonly Container _container = new Container();
        public static Container DIContainer;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            DIContainer = _container;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCustomMvc()
              .AddHealthChecks(Configuration, false)
              .ConfigureDIContainer(_container)
              .RegisterAppSettings(Configuration)
              .AddDependencies()
              .AddEventBus(Configuration, _container, true)
              .AddSignalRServices()
              .RegisterEventHandlers(_container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.AddCorsConfiguration()
                .UseRequestTimings()
                .InitializeDIContainer(_container)
                .ConfigureSignalRHubs()
                .ConfigureEventBus();
        }
    }
}