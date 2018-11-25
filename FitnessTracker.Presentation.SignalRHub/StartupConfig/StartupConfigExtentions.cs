using EventBus.Abstractions;
using FitnessTracker.Application.Model.Diet.Events;
using FitnessTracker.Application.Workout.Events;
using FitnessTracker.Common.AppSettings;
using FitnessTracker.Common.Web;
using FitnessTracker.Presentation.SignalRHub.EventHandlers.Diet;
using FitnessTracker.Presentation.SignalRHub.EventHandlers.Workout;
using FitnessTracker.Presentation.SignalRHub.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SimpleInjector;

namespace FitnessTracker.Presentation.SignalRHub.StartupConfig
{
    public static class StartupConfigExtentions
    {
        private const int Workout = 0;
        private const int Diet = 1;
        private const int Queue = 0;

        public static IServiceCollection AddSignalRServices(this IServiceCollection services)
        {
            services.AddSignalR();

            return services;
        }

        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            // Experimented with background process to host the event queue, I like the original way better
            //services.AddSingleton<IHostedService, EventBusHostedService>();

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

            // Workout
            eventBus.Subscribe<AddNewWorkoutEvent, AddNewWorkoutEventHandler>(appSettings.Value.ConnectionAtributes.RabbitExchangeInfo[Workout].Queue[Queue], appSettings.Value.ConnectionAtributes.RabbitExchangeInfo[Workout].ExchangeName);
            eventBus.Subscribe<BodyInfoSavedEvent, BodyInfoSavedEventHandler>(appSettings.Value.ConnectionAtributes.RabbitExchangeInfo[Workout].Queue[Queue], appSettings.Value.ConnectionAtributes.RabbitExchangeInfo[Workout].ExchangeName);
            eventBus.Subscribe<WorkoutCompletedEvent, WorkoutCompletedEventHandler>(appSettings.Value.ConnectionAtributes.RabbitExchangeInfo[Workout].Queue[Queue], appSettings.Value.ConnectionAtributes.RabbitExchangeInfo[Workout].ExchangeName);

            // Diet
            eventBus.Subscribe<AddNewFoodEvent, AddNewFoodEventHandler>(appSettings.Value.ConnectionAtributes.RabbitExchangeInfo[Diet].Queue[Queue], appSettings.Value.ConnectionAtributes.RabbitExchangeInfo[Diet].ExchangeName);
            eventBus.Subscribe<DeleteFoodItemEvent, DeleteFoodItemEventHandler>(appSettings.Value.ConnectionAtributes.RabbitExchangeInfo[Diet].Queue[Queue], appSettings.Value.ConnectionAtributes.RabbitExchangeInfo[Diet].ExchangeName);
            eventBus.Subscribe<EditMetabolicInfo, EditMetabolicInfoEventHandler>(appSettings.Value.ConnectionAtributes.RabbitExchangeInfo[Diet].Queue[Queue], appSettings.Value.ConnectionAtributes.RabbitExchangeInfo[Diet].ExchangeName);
            eventBus.Subscribe<SaveMenuEvent, SavedMenuEventHandler>(appSettings.Value.ConnectionAtributes.RabbitExchangeInfo[Diet].Queue[Queue], appSettings.Value.ConnectionAtributes.RabbitExchangeInfo[Diet].ExchangeName);

            return app;
        }

        public static IApplicationBuilder ConfigureSignalRHubs(this IApplicationBuilder app)
        {
            app.UseSignalR(routes => routes.MapHub<WorkoutHub>("/workouthub"));
            app.UseSignalR(routes => routes.MapHub<DietHub>("/diethub"));

            return app;
        }

        #endregion App
    }
}