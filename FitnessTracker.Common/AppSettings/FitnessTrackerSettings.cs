using EventBusAzure;
using RabbitMQWrapper;

namespace FitnessTracker.Common.AppSettings
{
    public class FitnessTrackerSettings
    {
        public string ConnectionString { get; set; }

        public ConnectionAttributes ConnectionAttributes { get; set; }

        public bool UseRabbitMQEventBus { get; set; }

        public AzureConnectionSettings AzureConnectionSettings { get; set; }
    }
}