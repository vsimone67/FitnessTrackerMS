using EventBus;
using EventBusAzure.EventBusServiceBus;
using FitnessTracker.Application.Model.Diet.Events;
using FitnessTracker.Application.Workout.Events;
using FitnessTracker.Common.AppSettings;
using FitnessTracker.Presentation.SignalRHub.EventHandlers.Diet;
using FitnessTracker.Presentation.SignalRHub.EventHandlers.Workout;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQEventBus;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FitnessTracker.Presentation.SignalRHub.BackgroundProcess
{
    public class EventBusHostedService : IHostedService
    {
        private readonly ILogger<EventBusHostedService> _logger;
        private readonly IOptions<FitnessTrackerSettings> _appSettings;
        private EventBusRabbitMQIOC _eventBusRabbitMQ;
        private EventBusServiceBus _eventBusAzure;
        private InMemoryEventBusSubscriptionsManager _subscriptionManager;

        private const int Workout = 0;
        private const int Diet = 1;
        private const int Queue = 0;

        public EventBusHostedService(ILogger<EventBusHostedService> logger, IOptions<FitnessTrackerSettings> appSettings)
        {
            _logger = logger;
            _appSettings = appSettings;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(async () =>
            {
                _logger.LogInformation("Starting EventBus Process......");
                _subscriptionManager = new InMemoryEventBusSubscriptionsManager();

                if (_appSettings.Value.UseRabbitMQEventBus)
                {
                    _eventBusRabbitMQ = new EventBusRabbitMQIOC(_appSettings.Value.ConnectionAttributes, _subscriptionManager, Startup.DIContainer);
                    _eventBusRabbitMQ.TurnOnReceiveQueue();

                    // Workout
                    _eventBusRabbitMQ.Subscribe<AddNewWorkoutEvent, AddNewWorkoutEventHandler>(_appSettings.Value.ConnectionAttributes.RabbitExchangeInfo[Workout].Queue[Queue], _appSettings.Value.ConnectionAttributes.RabbitExchangeInfo[Workout].ExchangeName);
                    _eventBusRabbitMQ.Subscribe<BodyInfoSavedEvent, BodyInfoSavedEventHandler>(_appSettings.Value.ConnectionAttributes.RabbitExchangeInfo[Workout].Queue[Queue], _appSettings.Value.ConnectionAttributes.RabbitExchangeInfo[Workout].ExchangeName);
                    _eventBusRabbitMQ.Subscribe<WorkoutCompletedEvent, WorkoutCompletedEventHandler>(_appSettings.Value.ConnectionAttributes.RabbitExchangeInfo[Workout].Queue[Queue], _appSettings.Value.ConnectionAttributes.RabbitExchangeInfo[Workout].ExchangeName);

                    // Diet
                    _eventBusRabbitMQ.Subscribe<AddNewFoodEvent, AddNewFoodEventHandler>(_appSettings.Value.ConnectionAttributes.RabbitExchangeInfo[Diet].Queue[Queue], _appSettings.Value.ConnectionAttributes.RabbitExchangeInfo[Diet].ExchangeName);
                    _eventBusRabbitMQ.Subscribe<DeleteFoodItemEvent, DeleteFoodItemEventHandler>(_appSettings.Value.ConnectionAttributes.RabbitExchangeInfo[Diet].Queue[Queue], _appSettings.Value.ConnectionAttributes.RabbitExchangeInfo[Diet].ExchangeName);
                    _eventBusRabbitMQ.Subscribe<EditMetabolicInfo, EditMetabolicInfoEventHandler>(_appSettings.Value.ConnectionAttributes.RabbitExchangeInfo[Diet].Queue[Queue], _appSettings.Value.ConnectionAttributes.RabbitExchangeInfo[Diet].ExchangeName);
                    _eventBusRabbitMQ.Subscribe<SaveMenuEvent, SavedMenuEventHandler>(_appSettings.Value.ConnectionAttributes.RabbitExchangeInfo[Diet].Queue[Queue], _appSettings.Value.ConnectionAttributes.RabbitExchangeInfo[Diet].ExchangeName);
                }
                else
                {
                    var connectionString = new ServiceBusConnectionStringBuilder(_appSettings.Value.AzureConnectionSettings.ConnectionString);
                    var azureEventBusSubscriptionManger = new DefaultServiceBusPersisterConnection(connectionString);
                    _eventBusAzure = new EventBusServiceBus(azureEventBusSubscriptionManger, _subscriptionManager, Startup.DIContainer, _appSettings.Value.AzureConnectionSettings.SubscriptionClientName);
                    _eventBusAzure.StartSubscriptionMessageHandler();

                    // Workout
                    _eventBusAzure.Subscribe<AddNewWorkoutEvent, AddNewWorkoutEventHandler>(_appSettings.Value.ConnectionAttributes.RabbitExchangeInfo[Workout].Queue[Queue], _appSettings.Value.ConnectionAttributes.RabbitExchangeInfo[Workout].ExchangeName);
                    _eventBusAzure.Subscribe<BodyInfoSavedEvent, BodyInfoSavedEventHandler>(_appSettings.Value.ConnectionAttributes.RabbitExchangeInfo[Workout].Queue[Queue], _appSettings.Value.ConnectionAttributes.RabbitExchangeInfo[Workout].ExchangeName);
                    _eventBusAzure.Subscribe<WorkoutCompletedEvent, WorkoutCompletedEventHandler>(_appSettings.Value.ConnectionAttributes.RabbitExchangeInfo[Workout].Queue[Queue], _appSettings.Value.ConnectionAttributes.RabbitExchangeInfo[Workout].ExchangeName);

                    // Diet
                    _eventBusAzure.Subscribe<AddNewFoodEvent, AddNewFoodEventHandler>(_appSettings.Value.ConnectionAttributes.RabbitExchangeInfo[Diet].Queue[Queue], _appSettings.Value.ConnectionAttributes.RabbitExchangeInfo[Diet].ExchangeName);
                    _eventBusAzure.Subscribe<DeleteFoodItemEvent, DeleteFoodItemEventHandler>(_appSettings.Value.ConnectionAttributes.RabbitExchangeInfo[Diet].Queue[Queue], _appSettings.Value.ConnectionAttributes.RabbitExchangeInfo[Diet].ExchangeName);
                    _eventBusAzure.Subscribe<EditMetabolicInfo, EditMetabolicInfoEventHandler>(_appSettings.Value.ConnectionAttributes.RabbitExchangeInfo[Diet].Queue[Queue], _appSettings.Value.ConnectionAttributes.RabbitExchangeInfo[Diet].ExchangeName);
                    _eventBusAzure.Subscribe<SaveMenuEvent, SavedMenuEventHandler>(_appSettings.Value.ConnectionAttributes.RabbitExchangeInfo[Diet].Queue[Queue], _appSettings.Value.ConnectionAttributes.RabbitExchangeInfo[Diet].ExchangeName);
                }

                while (!cancellationToken.IsCancellationRequested)
                {
                    await Task.Delay(TimeSpan.FromSeconds(60), cancellationToken);
                }

                _logger.LogInformation($"Keep ALive Loop Has Ended......");
            }, cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Keep Alive Process Is Stopping....");
            return Task.CompletedTask;
        }
    }
}