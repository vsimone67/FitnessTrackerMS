using EventBus.Abstractions;
using EventBus.Events;

namespace FitnessTracker.Diet.Service.EventBus.Mock
{
    public class EventBusBlank : IEventBus
    {
        public void Publish(IntegrationEvent @event)
        {
        }

        public void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
        }

        public void SubscribeDynamic<TH>(string eventName) where TH : IDynamicIntegrationEventHandler
        {
        }

        public void Unsubscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
        }

        public void UnsubscribeDynamic<TH>(string eventName) where TH : IDynamicIntegrationEventHandler
        {
        }
    }
}