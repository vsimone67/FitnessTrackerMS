using FitnessTracker.Common.Web.Extentions;
using FitnessTracker.Common.Web.StartupConfig;
using FitnessTracker.Diet.Service.AutoMapper;
using FitnessTracker.Diet.Service.StartupConfig;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;

namespace FitnessTracker.Diet.Service
{
    public class Startup
    {
        private readonly Container _container = new Container();
        private readonly SwaggerInfo _swaggerInfo;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            _swaggerInfo = new SwaggerInfo()
            {
                Title = "Diet Micro Service",
                Version = "v1",
                Description = "Application to track my diet and learn new technologies",
                TermsOfService = "Terms of Service",
                EndPointDescription = "Diet Micro Service V1"
            };
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCustomMvc()
              .AddCustomSwagger(_swaggerInfo)
              .ConfigureDIContainer(_container)
              .RegisterAppSettings(Configuration)
              .AddDependencies(Configuration)
              .RegisterFitnessTrackerDependencies(_container)
              .RegisterCommandAndQueryHandlers(_container)
              .RegisterMappingEngine(_container, DietMapperConfig.GetDietMapperConfig())
              .AddHealthChecks(Configuration)
              .AddEventBus(Configuration, _container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.AddCorsConfiguration()
                .UseRequestTimings()
                .AddMFCConfiguration()
                .AddSwaggerConfiguration(_swaggerInfo)
                .InitializeDIContainer(_container);
        }
    }
}