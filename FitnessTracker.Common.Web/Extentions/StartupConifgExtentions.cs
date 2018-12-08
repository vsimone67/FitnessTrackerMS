using AutoMapper;
using EventBus;
using EventBus.Abstractions;
using EventBusAzure.EventBusServiceBus;
using FitnessTracker.Application.Common;
using FitnessTracker.Application.Common.Processor;
using FitnessTracker.Common.AppSettings;
using FitnessTracker.Common.Attributes;
using FitnessTracker.Common.ExtentionMethods;
using FitnessTracker.Common.Web.Extentions;
using FitnessTracker.Common.Web.Filters;
using FitnessTracker.Common.Web.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using NLog.Extensions.Logging;
using NLog.Web;
using RabbitMQEventBus;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System.Linq;

namespace FitnessTracker.Common.Web.StartupConfig
{
    public static class CommonStartupConfigExtentions
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
              .SetCompatibilityVersion(CompatibilityVersion.Version_2_2); ;

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

            if (!AddDBCheck)
                services.AddHealthChecks();
            else
                services.AddHealthChecks()
                        .AddSqlServer(appSettings.Value.ConnectionString);

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

        public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration, Container container, bool ShouldTurnOnReceiveQueue = false)
        {
            IOptions<FitnessTrackerSettings> appSettings = services.BuildServiceProvider().GetRequiredService<IOptions<FitnessTrackerSettings>>();

            if (appSettings.Value.UseRabbitMQEventBus)
            {
                services.AddSingleton<IEventBus, EventBusRabbitMQIOC>(sp =>
                {
                    var eventBusSubscriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                    var eventBus = new EventBusRabbitMQIOC(appSettings.Value.ConnectionAttributes, eventBusSubscriptionsManager, container);

                    // We do not want multiple listeners on the event queue because the messages will not get through.  If we want to broadcast to multiple queues, set it up via config.  Each process should read from a queue not multiple
                    if (ShouldTurnOnReceiveQueue)
                        eventBus.TurnOnReceiveQueue();

                    return eventBus;
                });

                services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
            }
            else  // Azure Service Hub
            {
                //services.AddSingleton<IEventBus, EventBusBlank>();
                services.AddSingleton<IEventBus, EventBusServiceBus>(sp =>
                {
                    var connectionString = new ServiceBusConnectionStringBuilder(appSettings.Value.AzureConnectionSettings.ConnectionString)
                    {
                        EntityPath = appSettings.Value.AzureConnectionSettings.TopicName
                    };
                    var azureEventBusSubscriptionManger = new DefaultServiceBusPersisterConnection(connectionString);

                    var eventBusSubcriptionsManager = new InMemoryEventBusSubscriptionsManager();
                    var eventBus = new EventBusServiceBus(azureEventBusSubscriptionManger, eventBusSubcriptionsManager, container, appSettings.Value.AzureConnectionSettings.SubscriptionClientName);

                    if (ShouldTurnOnReceiveQueue)
                        eventBus.StartSubscriptionMessageHandler();

                    return eventBus;
                });

                services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
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

        public static IServiceCollection RegisterAppSettings(this IServiceCollection services, IConfiguration configuration)
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

        public static IApplicationBuilder UseRequestTimings(this IApplicationBuilder app)
        {
            app.UseMiddleware<RequestTimingsMiddleWare>();
            return app;
        }

        public static IApplicationBuilder InitializeDIContainer(this IApplicationBuilder app, Container container)
        {
            container.AutoCrossWireAspNetComponents(app);
            container.Verify();
            return app;
        }

        #endregion App

        #region WebHostBuilder

        public static IWebHostBuilder ConfigureNLog(this IWebHostBuilder builder, string basePath = "")
        {
            string fileName = basePath + "NLog.config";
            builder.ConfigureLogging((hostingContext, logging) =>
            {
                //hostingContext.HostingEnvironment.ConfigureNLog("/appsettings/NLog.Config"); // common settings are in the /appsettings folder
                hostingContext.HostingEnvironment.ConfigureNLog(fileName); // common settings are in the /appsettings folder
                logging.AddProvider(new NLogLoggerProvider());
                logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                logging.AddConsole();
                logging.AddDebug();
            });

            return builder;
        }

        public static IWebHostBuilder ConfigureNLogFromEnvironment(this IWebHostBuilder builder)
        {
            var config = new Microsoft.Extensions.Configuration.ConfigurationBuilder()
                                                               .AddEnvironmentVariables()
                                                               .Build(); // get variables from environment to pass to config (if exist)

            string basePath = config.GetValue<string>("nlogdirectory").NullToEmpty();

            return ConfigureNLog(builder, basePath);
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

        public static IWebHostBuilder ConfigAppConfigurationFromEnvironment(this IWebHostBuilder builder)
        {
            var config = new Microsoft.Extensions.Configuration.ConfigurationBuilder()
                                                             .AddEnvironmentVariables()
                                                             .Build(); // get variables from environment to pass to config (if exist)

            string basePath = config.GetValue<string>("appdirectory").NullToEmpty();

            return ConfigAppConfiguration(builder, basePath);
        }

        #endregion WebHostBuilder
    }
}