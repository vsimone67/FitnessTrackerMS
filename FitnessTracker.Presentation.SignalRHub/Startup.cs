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
        private Container _container = new Container();

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCustomMvc()
              .AddHealthChecks(Configuration)
              .AddEventBus(Configuration, _container)
              .ConfigureDIContainer(_container)
              .AddSignalRServices()
              .RegisterEventHandlers(_container)
              .AddDependencies();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.AddCorsConfiguration()
                .InitialzieDIContainer(_container)
                .ConfigureSignalRHubs()
                .ConfigureEventBus();
        }
    }
}