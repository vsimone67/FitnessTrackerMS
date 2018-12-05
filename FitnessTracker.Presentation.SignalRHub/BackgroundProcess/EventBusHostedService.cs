﻿using EventBus;
using FitnessTracker.Application.Model.Diet.Events;
using FitnessTracker.Application.Workout.Events;
using FitnessTracker.Common.AppSettings;
using FitnessTracker.Presentation.SignalRHub.EventHandlers.Diet;
using FitnessTracker.Presentation.SignalRHub.EventHandlers.Workout;
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
        private EventBusRabbitMQIOC _eventBus;
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
                _eventBus = new EventBusRabbitMQIOC(_appSettings.Value.ConnectionAttributes, _subscriptionManager, Startup.DIContainer);
                _eventBus.TurnOnReceiveQueue();
                // Workout
                _eventBus.Subscribe<AddNewWorkoutEvent, AddNewWorkoutEventHandler>(_appSettings.Value.ConnectionAttributes.RabbitExchangeInfo[Workout].Queue[Queue], _appSettings.Value.ConnectionAttributes.RabbitExchangeInfo[Workout].ExchangeName);
                _eventBus.Subscribe<BodyInfoSavedEvent, BodyInfoSavedEventHandler>(_appSettings.Value.ConnectionAttributes.RabbitExchangeInfo[Workout].Queue[Queue], _appSettings.Value.ConnectionAttributes.RabbitExchangeInfo[Workout].ExchangeName);
                _eventBus.Subscribe<WorkoutCompletedEvent, WorkoutCompletedEventHandler>(_appSettings.Value.ConnectionAttributes.RabbitExchangeInfo[Workout].Queue[Queue], _appSettings.Value.ConnectionAttributes.RabbitExchangeInfo[Workout].ExchangeName);

                // Diet
                _eventBus.Subscribe<AddNewFoodEvent, AddNewFoodEventHandler>(_appSettings.Value.ConnectionAttributes.RabbitExchangeInfo[Diet].Queue[Queue], _appSettings.Value.ConnectionAttributes.RabbitExchangeInfo[Diet].ExchangeName);
                _eventBus.Subscribe<DeleteFoodItemEvent, DeleteFoodItemEventHandler>(_appSettings.Value.ConnectionAttributes.RabbitExchangeInfo[Diet].Queue[Queue], _appSettings.Value.ConnectionAttributes.RabbitExchangeInfo[Diet].ExchangeName);
                _eventBus.Subscribe<EditMetabolicInfo, EditMetabolicInfoEventHandler>(_appSettings.Value.ConnectionAttributes.RabbitExchangeInfo[Diet].Queue[Queue], _appSettings.Value.ConnectionAttributes.RabbitExchangeInfo[Diet].ExchangeName);
                _eventBus.Subscribe<SaveMenuEvent, SavedMenuEventHandler>(_appSettings.Value.ConnectionAttributes.RabbitExchangeInfo[Diet].Queue[Queue], _appSettings.Value.ConnectionAttributes.RabbitExchangeInfo[Diet].ExchangeName);

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