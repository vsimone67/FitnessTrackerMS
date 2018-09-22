//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.DependencyInjection;
//using Newtonsoft.Json.Serialization;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace DockerPOC.Common.Web.Extentions
//{
//    public static class StartupConifigExtentions
//    {
//        #region Services

//        public static IServiceCollection AddCustomMvc(this IServiceCollection services)
//        {
//            services.AddMvc(options =>
//            {
//                options.Filters.Add(typeof(Filters.HttpGlobalExceptionFilter));
//            }).AddControllersAsServices().AddJsonOptions(options =>
//            {
//                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
//            }).AddControllersAsServices()  //Injecting Controllers themselves thru DI
//              .SetCompatibilityVersion(CompatibilityVersion.Version_2_1); ;

//            services.AddCors(options =>
//            {
//                options.AddPolicy("CorsPolicy",
//                    builder => builder.AllowAnyOrigin()
//                    .AllowAnyMethod()
//                    .AllowAnyHeader()
//                    .AllowCredentials());
//            });

//            return services;
//        }

//        //public static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
//        //{
//        //    services.AddHealthChecks(checks =>
//        //    {
//        //        checks.AddValueTaskCheck("HTTP Endpoint", () => new ValueTask<IHealthCheckResult>(HealthCheckResult.Healthy("Ok")),
//        //                                 TimeSpan.Zero  //No cache for this HealthCheck, better just for demos
//        //                                );

//        //        checks.AddSqlCheck("vsazure", configuration.GetConnectionString("WorkoutConnection"), TimeSpan.FromMinutes(1));
//        //    });

//        //    return services;
//        //}

//        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
//        {
//            services.AddSwaggerGen(options =>
//            {
//                options.DescribeAllEnumsAsStrings();
//                options.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
//                {
//                    Title = "Workout Micro Service",
//                    Version = "v1.0",
//                    Description = "Application to track workouts and learn new technologies",
//                    TermsOfService = "Terms Of Service"
//                });
//            });

//            return services;
//        }

//        public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration, Container container)
//        {
//            services.AddSingleton<IEventBus, EventBusRabbitMQIOC>(sp =>
//            {
//                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

//                return new EventBusRabbitMQIOC(EventBusConnection.GetEventConnection(configuration), eventBusSubcriptionsManager, container);
//            });

//            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
//            return services;
//        }

//        public static IServiceCollection ConfigureDIContainer(this IServiceCollection services, Container container)
//        {
//            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
//            services.EnableSimpleInjectorCrossWiring(container);

//            return services;
//        }

//        #endregion Services

//        #region App

//        public static IApplicationBuilder AddCorsConfiguration(this IApplicationBuilder app)
//        {
//            app.UseCors("CorsPolicy");

//            return app;
//        }

//        public static IApplicationBuilder AddMFCConfiguration(this IApplicationBuilder app)
//        {
//            app.UseMvcWithDefaultRoute();
//            return app;
//        }

//        public static IApplicationBuilder AddSwaggerConfiguration(this IApplicationBuilder app)
//        {
//            app.UseSwagger()
//              .UseSwaggerUI(c =>
//              {
//                  c.SwaggerEndpoint("/swagger/v1/swagger.json", "Workout Micro Service V1");
//              });

//            return app;
//        }

//        public static IApplicationBuilder ConfigureEventBus(this IApplicationBuilder app)
//        {
//            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
//            eventBus.Subscribe<AddNewWorkoutEvent, AddNewWorkoutEventHandler>();

//            return app;
//        }

//        public static IApplicationBuilder InitialzieDIContainer(this IApplicationBuilder app, Container container)
//        {
//            container.AutoCrossWireAspNetComponents(app);
//            container.Verify();
//            return app;
//        }

//        #endregion App
//    }
//}