using AutoMapper;
using EventBus;
using EventBus.Abstractions;
using FitnessTracker.Common.AppSettings;
using FitnessTracker.Common.Attributes;
using FitnessTracker.Common.EventBus;
using FitnessTracker.Common.Web.Extentions;
using FitnessTracker.Common.Web.Filters;
using FitnetssTracker.Application.Common;
using FitnetssTracker.Application.Common.Processor;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
using NLog.Extensions.Logging;
using NLog.Web;
using Microsoft.Extensions.Logging;

namespace FitnessTracker.Common.Web.StartupConfig
{
    public static class CommonStartupConifigExtentions
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

        public static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration, bool AddDBCheck = true)
        {
            IOptions<FitnessTrackerSettings> appSettings = services.BuildServiceProvider().GetRequiredService<IOptions<FitnessTrackerSettings>>();

            services.AddHealthChecks(checks =>
            {
                checks.AddValueTaskCheck("HTTP Endpoint", () => new ValueTask<IHealthCheckResult>(HealthCheckResult.Healthy("Ok")),
                                         TimeSpan.Zero  //No cache for this HealthCheck, better just for demos
                                        );

                if (AddDBCheck)
                    checks.AddSqlCheck("vsazure", appSettings.Value.ConnectionString, TimeSpan.FromMinutes(1));
            });

            return services;
        }

        public static IServiceCollection AddCustomSwagger(this IServiceCollection services, SwaggerInfo swaggerInfo)
        {
            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc(swaggerInfo.Version, new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = swaggerInfo.Title,
                    Version = swaggerInfo.Version,
                    Description = swaggerInfo.Description,
                    TermsOfService = swaggerInfo.TermsOfService
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

        public static IServiceCollection RegisterMappingEngine(this IServiceCollection services, Container container, IMapper mapperConfig)
        {
            container.Register<IMapper>(() => mapperConfig, Lifestyle.Singleton);  // Register automapper config and mappings

            return services;
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

        public static IApplicationBuilder AddSwaggerConfiguration(this IApplicationBuilder app, SwaggerInfo swaggerInfo)
        {
            app.UseSwagger()
              .UseSwaggerUI(c =>
              {
                  c.SwaggerEndpoint("/swagger/v1/swagger.json", swaggerInfo.EndPointDescription);
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

        #region WebHostBuilder

        public static IWebHostBuilder ConfigureNLog(this IWebHostBuilder builder)
        {
            builder.ConfigureLogging((hostingContext, logging) =>
            {
                //hostingContext.HostingEnvironment.ConfigureNLog("/appsettings/NLog.Config"); // common settings are in the /appsettings folder
                hostingContext.HostingEnvironment.ConfigureNLog("NLog.config"); // common settings are in the /appsettings folder
                logging.AddProvider(new NLogLoggerProvider());
                logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                logging.AddConsole();
                logging.AddDebug();
            });

            return builder;
        }

        public static IWebHostBuilder ConfigAppConfiguration(this IWebHostBuilder builder, string basePath = "")
        {
            builder.ConfigureAppConfiguration((builderContext, config) =>
            {
                var env = builderContext.HostingEnvironment;

                if (basePath != string.Empty)
                    config.SetBasePath(basePath);  // set path to docker volume for common files

                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                config.AddJsonFile("appsettings.secrets.json", optional: true, reloadOnChange: true);
                config.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

                config.AddEnvironmentVariables();
            });

            return builder;
        }

        #endregion WebHostBuilder
    }
}