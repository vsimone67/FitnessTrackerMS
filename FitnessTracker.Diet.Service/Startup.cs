﻿using FitnessTracker.Diet.Service.StartupConfig;
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

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCustomMvc()
              .AddCustomSwagger()
              .ConfigureDIContainer(_container)
              .RegisterFitnessTrackerDependencies(_container)
              .RegisterCommandAndQueryHandlers(_container)
              .RegisterMappingEngine(_container)
              .AddDependencies(Configuration)
              .RegiserAppSettings(Configuration)
              .AddHealthChecks(Configuration)
              .AddEventBus(Configuration, _container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.AddCorsConfiguration()
                .AddMFCConfiguration()
                .AddSwaggerConfiguration()
                .InitialzieDIContainer(_container);
        }
    }
}