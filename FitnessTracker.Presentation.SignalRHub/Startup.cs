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

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCustomMvc()
              .AddHealthChecks(Configuration, false)
              .ConfigureDIContainer(_container)
              .RegiserAppSettings(Configuration)
              .AddDependencies()
              .AddEventBus(Configuration, _container)
              .AddSignalRServices()
              .RegisterEventHandlers(_container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.AddCorsConfiguration()
                .UseRequestTimings()
                .InitialzieDIContainer(_container)
                .ConfigureSignalRHubs()
                .ConfigureEventBus();
        }
    }
}