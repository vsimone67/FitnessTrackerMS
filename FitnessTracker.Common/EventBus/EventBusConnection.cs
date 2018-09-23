using Microsoft.Extensions.Configuration;
using RabbitMQWrapper;
using System.Collections.Generic;

namespace FitnessTracker.Common.EventBus
{
    public static class EventBusConnection
    {
        public static ConnectionAtributes GetEventConnection(IConfiguration configuration)
        {
            return new ConnectionAtributes()
            {
                HostName = configuration.GetValue<string>("rabbitMQServer:hostName"),
                UserName = configuration.GetValue<string>("rabbitMQServer:userName"),
                Password = configuration.GetValue<string>("rabbitMQServer:password"),
                RabbitExchangeInfo = new List<ExchangeInfo>() { new ExchangeInfo() {  ExchangeName = configuration.GetValue<string>("fitnessTrackerEventQueue:exchangeName"),
                     ExchangeType = configuration.GetValue<string>("fitnessTrackerEventQueue:exchangeType"), RoutingKey = configuration.GetValue<string>("fitnessTrackerEventQueue:routingKey"),
                     Queue = configuration.GetSection("fitnessTrackerEventQueue:queues").Get<List<string>>()} }
            };
        }
    }
}