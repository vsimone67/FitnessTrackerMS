using EventBus.Abstractions;
using FitnessTracker.Application.Model.Diet.Events;
using FitnessTracker.Application.Workout.Events;
using FitnessTracker.Common.Web;
using FitnessTracker.Presentation.SignalRHub.EventHandlers.Diet;
using FitnessTracker.Presentation.SignalRHub.EventHandlers.Workout;
using FitnessTracker.Presentation.SignalRHub.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;

namespace FitnessTracker.Presentation.SignalRHub.StartupConfig
{
    public static class StartupConfigExtentions
    {
        public static IServiceCollection AddSignalRServices(this IServiceCollection services)
        {
            services.AddSignalR();

            return services;
        }

        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            // Workout
            //services.AddSingleton<IIntegrationEventHandler<AddNewWorkoutEvent>, AddNewWorkoutEventHandler>();
            //services.AddSingleton<IIntegrationEventHandler<BodyInfoSavedEvent>, BodyInfoSavedEventHandler>();
            //services.AddSingleton<IIntegrationEventHandler<WorkoutCompletedEvent>, WorkoutCompletedEventHanlder>();

            // Diet
            //services.AddSingleton<IIntegrationEventHandler<AddNewFoodEvent>, AddNewFoodEventHandler>();
            //services.AddSingleton<IIntegrationEventHandler<DeleteFoodItemEvent>, DeleteFoodItemEventHandler>();
            //services.AddSingleton<IIntegrationEventHandler<EditMetabolicInfo>, EditMetabolicInfoEventHandler>();
            //services.AddSingleton<IIntegrationEventHandler<SaveMenuEvent>, SavedMenuEventHandler>();

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

        #endregion App
    }
}