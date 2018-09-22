using Microsoft.Extensions.Configuration;
using RabbitMQWrapper;
using System.Collections.Generic;

namespace FitnessTracker.Workout.Service.EventBus.Connection
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
                RabbitExchangeInfo = new List<ExchangeInfo>() { new ExchangeInfo() {  ExchangeName = configuration.GetValue<string>("workoutEventQueue:exchangeName"),
                     ExchangeType = configuration.GetValue<string>("workoutEventQueue:exchangeType"), RoutingKey = configuration.GetValue<string>("workoutEventQueue:routingKey"),
                     Queue = configuration.GetSection("workoutEventQueue:queues").Get<List<string>>()} }
            };
        }
    }
}