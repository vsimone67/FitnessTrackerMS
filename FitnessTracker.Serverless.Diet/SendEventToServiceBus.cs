using EventBus.Events;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Text;

namespace FitnessTracker.Serverless.Diet
{
    public class SendEventToServiceBus
    {
        private readonly ServiceBusConnectionStringBuilder _connectionString;

        public SendEventToServiceBus(string connectionString, string entityPath)
        {
            _connectionString = new ServiceBusConnectionStringBuilder(connectionString)
            {
                EntityPath = entityPath
            };
        }

        public void Send(IntegrationEvent evt)
        {
            var eventName = evt.GetType().Name;
            var jsonMessage = JsonConvert.SerializeObject(evt);
            var body = Encoding.UTF8.GetBytes(jsonMessage);

            var message = new Message
            {
                MessageId = Guid.NewGuid().ToString(),
                Body = body,
                Label = eventName,
            };
            TopicClient topicClient = new TopicClient(_connectionString, RetryPolicy.Default);

            topicClient.SendAsync(message).GetAwaiter().GetResult();
        }
    }
}