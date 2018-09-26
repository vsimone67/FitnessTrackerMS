using EventBus;
using EventBus.Abstractions;
using FitnessTracker.Application.Model.Diet.Events;
using FitnessTracker.Application.Workout.Events;
using FitnessTracker.Common.AppSettings;
using FitnessTracker.Presentation.SignalRHub.EventHandlers.Diet;
using FitnessTracker.Presentation.SignalRHub.EventHandlers.Workout;
using FitnessTracker.Presentation.SignalRHub.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.HealthChecks;
using Microsoft.Extensions.Options;
using RabbitMQEventBus;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;
using System.Threading.Tasks;

namespace FitnessTracker.Presentation.SignalRHub.StartupConfig
{
    public static class StartupConfigExtentions
    {
        #region Services

        public static IServiceCollection AddCustomMvc(this IServiceCollection services)
        {
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
            services.AddHealthChecks(checks =>
            {
                checks.AddValueTaskCheck("HTTP Endpoint", () => new ValueTask<IHealthCheckResult>(HealthCheckResult.Healthy("Ok")),
                                         TimeSpan.Zero  //No cache for this HealthCheck, better just for demos
                                        );
            });

            return services;
        }

        public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration, Container container)
        {
            services.AddSingleton<IEventBus, EventBusRabbitMQIOC>(sp =>
            {
                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();
                var appSettings = sp.GetRequiredService<IOptions<FitnessTrackerSettings>>();
                var eventBus = new EventBusRabbitMQIOC(appSettings.Value.ConnectionAtributes, eventBusSubcriptionsManager, container);
                eventBus.TurnOnRecieveQueue();
                return eventBus;
            });

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

            return services;
        }

        public static IServiceCollection AddSignalRServices(this IServiceCollection services)
        {
            services.AddSignalR();

            return services;
        }

        public static IServiceCollection ConfigureDIContainer(this IServiceCollection services, Container container)
        {
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            services.EnableSimpleInjectorCrossWiring(container);

            return services;
        }

        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            // Workout
            services.AddSingleton<IIntegrationEventHandler<AddNewWorkoutEvent>, AddNewWorkoutEventHandler>();
            services.AddSingleton<IIntegrationEventHandler<BodyInfoSavedEvent>, BodyInfoSavedEventHandler>();
            services.AddSingleton<IIntegrationEventHandler<WorkoutCompletedEvent>, WorkoutCompletedEventHanlder>();

            // Diet
            services.AddSingleton<IIntegrationEventHandler<AddNewFoodEvent>, AddNewFoodEventHandler>();
            services.AddSingleton<IIntegrationEventHandler<DeleteFoodItemEvent>, DeleteFoodItemEventHandler>();
            services.AddSingleton<IIntegrationEventHandler<EditMetabolicInfo>, EditMetabolicInfoEventHandler>();
            services.AddSingleton<IIntegrationEventHandler<SaveMenuEvent>, SavedMenuEventHandler>();

            return services;
        }

        public static IServiceCollection RegiserAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<FitnessTrackerSettings>(configuration);

            return services;
        }

        public static IServiceCollection RegisterEventHandlers(this IServiceCollection services, Container container)
        {
            //var fitnessTrackerAssemblies = LibraryManager.GetReferencingAssemblies("FitnessTracker");

            //// Look in all assemblies and register all implementations of ICommandHandler<in TCommand>
            //container.Register(typeof(ICommandHandler<,>), fitnessTrackerAssemblies);
            //// Look in all assemblies and register all implementations of IQueryHandler<in TQuery, TResult>
            //container.Register(typeof(IQueryHandler<,>), fitnessTrackerAssemblies);

            ////TODO: NOTE:  No idea why we have to use services.addsingleton instead of container.Register.  container.register does not work
            //services.AddSingleton<ICommandProcessor>((p) => new CommandProcessor(container.GetInstance));
            //services.AddSingleton<IQueryProcessor>((p) => new QueryProcessor(container.GetInstance));

            // Add Event Handlers here

            //services.AddSingleton<IIntegrationEventHandler<AddNewWorkoutEvent>, AddNewWorkoutEventHandler>();

            return services;
        }

        #endregion Services

        #region App

        public static IApplicationBuilder AddCorsConfiguration(this IApplicationBuilder app)
        {
            app.UseCors("CorsPolicy");

            return app;
        }

        public static IApplicationBuilder ConfigureEventBus(this IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

            // Workout
            eventBus.Subscribe<AddNewWorkoutEvent, AddNewWorkoutEventHandler>();
            eventBus.Subscribe<BodyInfoSavedEvent, BodyInfoSavedEventHandler>();
            eventBus.Subscribe<WorkoutCompletedEvent, WorkoutCompletedEventHanlder>();

            // Diet
            eventBus.Subscribe<AddNewFoodEvent, AddNewFoodEventHandler>();
            eventBus.Subscribe<DeleteFoodItemEvent, DeleteFoodItemEventHandler>();
            eventBus.Subscribe<EditMetabolicInfo, EditMetabolicInfoEventHandler>();
            eventBus.Subscribe<SaveMenuEvent, SavedMenuEventHandler>();

            return app;
        }

        public static IApplicationBuilder ConfigureSignalRHubs(this IApplicationBuilder app)
        {
            app.UseSignalR(routes => routes.MapHub<WorkoutHub>("/workouthub"));
            app.UseSignalR(routes => routes.MapHub<DietHub>("/diethub"));

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