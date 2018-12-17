using EventBus;
using EventBus.Abstractions;
using FitnessTracker.Common.AppSettings;
using Microsoft.Extensions.Options;
using RabbitMQEventBus;
using SimpleInjector;

namespace FitnessTracker.Common.Web.EventBusCreator
{
    public class RabbitMQEventBusCreator : IEventBusInstance
    {
        private readonly EventBusRabbitMQIOC _eventBusRabbitMQIOC;

        public RabbitMQEventBusCreator(IOptions<FitnessTrackerSettings> appSettings, Container container, IEventBusSubscriptionsManager eventBusSubscriptionManager, bool ShouldTurnOnReceiveQueue)
        {
            _eventBusRabbitMQIOC = new EventBusRabbitMQIOC(appSettings.Value.ConnectionAttributes, eventBusSubscriptionManager, container);

            // We do not want multiple listeners on the event queue because the messages will not get through.  If we want to broadcast to multiple queues, set it up via config.  Each process should read from a queue not multiple
            if (ShouldTurnOnReceiveQueue)
                _eventBusRabbitMQIOC.TurnOnReceiveQueue();
        }

        public IEventBus GetInstance()
        {
            return _eventBusRabbitMQIOC;
        }
    }
}