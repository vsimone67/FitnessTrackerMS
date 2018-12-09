using EventBus;
using EventBus.Abstractions;
using EventBusAzure.EventBusServiceBus;
using FitnessTracker.Common.AppSettings;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Options;
using SimpleInjector;

namespace FitnessTracker.Common.Web.EventBusCreator
{
    /// <summary>
    /// This is a helper class that is responsible for creating a specific instance of IEventBus, this is used in the common AddEventBus so we can have a clean set of code as to what event bus we want to implement.
    /// This class will encapsulate all the create logic to simplify the event bus creation
    /// </summary>
    public class AzureServiceBusCreator : IEventBusInstance
    {
        public EventBusServiceBus _eventBusServiceBus;

        public AzureServiceBusCreator(IOptions<FitnessTrackerSettings> appSettings, Container container, IEventBusSubscriptionsManager eventBusSubscriptionManager, bool ShouldTurnOnReceiveQueue)
        {
            var connectionString = new ServiceBusConnectionStringBuilder(appSettings.Value.AzureConnectionSettings.ConnectionString)
            {
                EntityPath = appSettings.Value.AzureConnectionSettings.TopicName
            };
            var azureEventBusSubscriptionManger = new DefaultServiceBusPersisterConnection(connectionString);

            _eventBusServiceBus = new EventBusServiceBus(azureEventBusSubscriptionManger, eventBusSubscriptionManager, container, appSettings.Value.AzureConnectionSettings.SubscriptionClientName);

            if (ShouldTurnOnReceiveQueue)
                _eventBusServiceBus.StartSubscriptionMessageHandler();
        }

        public IEventBus GetInstance()
        {
            return _eventBusServiceBus;
        }
    }
}