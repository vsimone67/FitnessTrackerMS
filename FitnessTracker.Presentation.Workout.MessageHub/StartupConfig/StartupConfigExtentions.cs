using EventBus.Abstractions;
using FitnessTracker.Application.Workout.Events;
using FitnessTracker.Common.AppSettings;
using FitnessTracker.Common.Web;
using FitnessTracker.Presentation.Workout.MessageHub.EventHandlers;
using FitnessTracker.Presentation.Workout.MessageHub.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTracker.Presentation.Workout.MessageHub.StartupConfig
{
    public static class StartupConfigExtentions
    {
        private const int Workout = 0;
        private const int Queue = 0;

        public static IServiceCollection AddSignalRServices(this IServiceCollection services)
        {
            services.AddSignalR();

            return services;
        }

        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            return services;
        }

        public static IServiceCollection RegisterEventHandlers(this IServiceCollection services, Container container)
        {
            var fitnessTrackerAssemblies = LibraryManager.GetReferencingAssemblies("FitnessTracker");

            // Auto Register All Event Handlers
            container.Register(typeof(IIntegrationEventHandler<>), fitnessTrackerAssemblies);

            return services;
        }

        #region App

        public static IApplicationBuilder ConfigureEventBus(this IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            var appSettings = app.ApplicationServices.GetRequiredService<IOptions<FitnessTrackerSettings>>();

            eventBus.Subscribe<AddNewWorkoutEvent, AddNewWorkoutEventHandler>(appSettings.Value.ConnectionAttributes.RabbitExchangeInfo[Workout].Queue[Queue], appSettings.Value.ConnectionAttributes.RabbitExchangeInfo[Workout].ExchangeName);
            eventBus.Subscribe<BodyInfoSavedEvent, BodyInfoSavedEventHandler>(appSettings.Value.ConnectionAttributes.RabbitExchangeInfo[Workout].Queue[Queue], appSettings.Value.ConnectionAttributes.RabbitExchangeInfo[Workout].ExchangeName);
            eventBus.Subscribe<WorkoutCompletedEvent, WorkoutCompletedEventHandler>(appSettings.Value.ConnectionAttributes.RabbitExchangeInfo[Workout].Queue[Queue], appSettings.Value.ConnectionAttributes.RabbitExchangeInfo[Workout].ExchangeName);

            return app;
        }

        public static IApplicationBuilder ConfigureSignalRHubs(this IApplicationBuilder app)
        {
            app.UseSignalR(routes => routes.MapHub<WorkoutHub>("/workouthub"));

            return app;
        }

        #endregion App
    }
}