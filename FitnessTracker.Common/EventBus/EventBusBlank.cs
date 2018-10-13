using EventBus.Abstractions;
using EventBus.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessTracker.Common.EventBus
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

        public void Subscribe<T, TH>(string queue, string exchange)
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