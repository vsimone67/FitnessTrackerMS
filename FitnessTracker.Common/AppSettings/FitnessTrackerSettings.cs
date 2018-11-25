using RabbitMQWrapper;

namespace FitnessTracker.Common.AppSettings
{
    public class FitnessTrackerSettings
    {
        public string ConnectionString { get; set; }

        public ConnectionAtributes ConnectionAtributes { get; set; }

        public bool UseRabbitMQEventBus { get; set; }
    }
}