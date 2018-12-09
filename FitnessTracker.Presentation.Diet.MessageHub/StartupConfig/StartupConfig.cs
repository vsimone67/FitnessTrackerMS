using EventBus.Abstractions;
using FitnessTracker.Application.Model.Diet.Events;
using FitnessTracker.Common.AppSettings;
using FitnessTracker.Common.Web;
using FitnessTracker.Presentation.Diet.MessageHub.EventHandlers;
using FitnessTracker.Presentation.Diet.MessageHub.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SimpleInjector;

namespace FitnessTracker.Presentation.Diet.MessageHub.StartupConfig
{
    public static class StartupConfigExtentions
    {
        private const int Diet = 0;
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

            eventBus.Subscribe<AddNewFoodEvent, AddNewFoodEventHandler>(appSettings.Value.ConnectionAttributes.RabbitExchangeInfo[Diet].Queue[Queue], appSettings.Value.ConnectionAttributes.RabbitExchangeInfo[Diet].ExchangeName);
            eventBus.Subscribe<DeleteFoodItemEvent, DeleteFoodItemEventHandler>(appSettings.Value.ConnectionAttributes.RabbitExchangeInfo[Diet].Queue[Queue], appSettings.Value.ConnectionAttributes.RabbitExchangeInfo[Diet].ExchangeName);
            eventBus.Subscribe<EditMetabolicInfo, EditMetabolicInfoEventHandler>(appSettings.Value.ConnectionAttributes.RabbitExchangeInfo[Diet].Queue[Queue], appSettings.Value.ConnectionAttributes.RabbitExchangeInfo[Diet].ExchangeName);
            eventBus.Subscribe<SaveMenuEvent, SavedMenuEventHandler>(appSettings.Value.ConnectionAttributes.RabbitExchangeInfo[Diet].Queue[Queue], appSettings.Value.ConnectionAttributes.RabbitExchangeInfo[Diet].ExchangeName);

            return app;
        }

        public static IApplicationBuilder ConfigureSignalRHubs(this IApplicationBuilder app)
        {
            app.UseSignalR(routes => routes.MapHub<DietHub>("/diethub"));

            return app;
        }

        #endregion App
    }
}