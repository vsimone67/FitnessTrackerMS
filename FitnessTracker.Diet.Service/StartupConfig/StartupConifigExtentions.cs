using AutoMapper;
using EventBus;
using EventBus.Abstractions;
using FitnessTracker.Application.MappingProfile;
using FitnessTracker.Common.AppSettings;
using FitnessTracker.Common.Attributes;
using FitnessTracker.Common.Web.Filters;
using FitnessTracker.Diet.Service.EventBus.Mock;
using FitnessTracker.Service.IOC;
using FitnetssTracker.Application.Common;
using FitnetssTracker.Application.Common.Processor;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.HealthChecks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using RabbitMQEventBus;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTracker.Diet.Service.StartupConfig
{
    public static class StartupConifigExtentions
    {
        #region Services

        public static IServiceCollection AddCustomMvc(this IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
            }).AddControllersAsServices().AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            }).AddControllersAsServices()  //Injecting Controllers themselves thru DI
              .SetCompatibilityVersion(CompatibilityVersion.Version_2_1); ;

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            return services;
        }

        public static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            IOptions<FitnessTrackerSettings> appSettings = services.BuildServiceProvider().GetRequiredService<IOptions<FitnessTrackerSettings>>();

            services.AddHealthChecks(checks =>
            {
                checks.AddValueTaskCheck("HTTP Endpoint", () => new ValueTask<IHealthCheckResult>(HealthCheckResult.Healthy("Ok")),
                                         TimeSpan.Zero  //No cache for this HealthCheck, better just for demos
                                        );

                checks.AddSqlCheck("vsazure", appSettings.Value.ConnectionString, TimeSpan.FromMinutes(1));
            });

            return services;
        }

        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "Diet Micro Service",
                    Version = "v1.0",
                    Description = "Application to track my diet and learn new technologies",
                    TermsOfService = "Terms Of Service"
                });
            });

            return services;
        }

        public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration, Container container)
        {
            IOptions<FitnessTrackerSettings> appSettings = services.BuildServiceProvider().GetRequiredService<IOptions<FitnessTrackerSettings>>();

            if (appSettings.Value.UseRabbitMQEventBus)
            {
                services.AddSingleton<IEventBus, EventBusRabbitMQIOC>(sp =>
                {
                    var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                    var eventBus = new EventBusRabbitMQIOC(appSettings.Value.ConnectionAtributes, eventBusSubcriptionsManager, container);
                    eventBus.TurnOnRecieveQueue();
                    return eventBus;
                });

                services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
            }
            else  // mock event bus
            {
                services.AddSingleton<IEventBus, EventBusBlank>();
            }
            return services;
        }

        public static IServiceCollection ConfigureDIContainer(this IServiceCollection services, Container container)
        {
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            services.EnableSimpleInjectorCrossWiring(container);

            return services;
        }

        public static IServiceCollection RegisterCommandAndQueryHandlers(this IServiceCollection services, Container container)
        {
            var fitnessTrackerAssemblies = LibraryManager.GetReferencingAssemblies("FitnessTracker");

            // Look in all assemblies and register all implementations of ICommandHandler<in TCommand>
            container.Register(typeof(ICommandHandler<,>), fitnessTrackerAssemblies);
            // Look in all assemblies and register all implementations of IQueryHandler<in TQuery, TResult>
            container.Register(typeof(IQueryHandler<,>), fitnessTrackerAssemblies);

            //TODO: NOTE:  No idea why we have to use services.addsingleton instead of container.Register.  container.register does not work
            services.AddSingleton<ICommandProcessor>((p) => new CommandProcessor(container.GetInstance));
            services.AddSingleton<IQueryProcessor>((p) => new QueryProcessor(container.GetInstance));

            return services;
        }

        public static IServiceCollection RegisterFitnessTrackerDependencies(this IServiceCollection services, Container container)
        {
            var fitnessTrackerAssemblyDefinedTypes = LibraryManager.GetReferencingAssemblies("FitnessTracker")
                            .SelectMany(an => an.DefinedTypes);

            // Get types with AutoRegisterAttribute in assemblies
            var typesWithAutoRegisterAttribute =
                from t in fitnessTrackerAssemblyDefinedTypes
                let attributes = t.GetCustomAttributes(typeof(AutoRegisterAttribute), true)
                where attributes != null && attributes.Any()
                select new { Type = t, Attributes = attributes.Cast<AutoRegisterAttribute>() };

            // Loop through types to register with IoC
            foreach (var typeToRegister in typesWithAutoRegisterAttribute)
            {
                foreach (var attribute in typeToRegister.Attributes)
                {
                    container.Register(attribute.RegisterAsType, typeToRegister.Type.AsType());
                }
            }

            return services;
        }

        public static IServiceCollection RegiserAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<FitnessTrackerSettings>(configuration);

            return services;
        }

        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }

        public static IServiceCollection RegisterMappingEngine(this IServiceCollection services, Container container)
        {
            IMapper mapperConfig = GetMapperConfiguration();
            container.Register<IMapper>(() => mapperConfig, Lifestyle.Singleton);  // Register automapper config and mappings

            return services;
        }

        public static IMapper GetMapperConfiguration()
        {
            MapperConfiguration mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DietMappingProfile());
            });

            return mapperConfig.CreateMapper();
        }

        #endregion Services

        #region App

        public static IApplicationBuilder AddCorsConfiguration(this IApplicationBuilder app)
        {
            app.UseCors("CorsPolicy");

            return app;
        }

        public static IApplicationBuilder AddMFCConfiguration(this IApplicationBuilder app)
        {
            app.UseMvcWithDefaultRoute();
            return app;
        }

        public static IApplicationBuilder AddSwaggerConfiguration(this IApplicationBuilder app)
        {
            app.UseSwagger()
              .UseSwaggerUI(c =>
              {
                  c.SwaggerEndpoint("/swagger/v1/swagger.json", "Diet Micro Service V1");
              });

            return app;
        }

        public static IApplicationBuilder InitialzieDIContainer(this IApplicationBuilder app, Container container)
        {
            container.AutoCrossWireAspNetComponents(app);
            container.Verify();
            return app;
        }

        #endregion App
    }
}