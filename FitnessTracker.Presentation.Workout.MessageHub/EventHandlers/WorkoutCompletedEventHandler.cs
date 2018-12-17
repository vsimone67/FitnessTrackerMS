using EventBus.Abstractions;
using FitnessTracker.Application.Workout.Events;
using FitnessTracker.Presentation.Workout.MessageHub.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace FitnessTracker.Presentation.Workout.MessageHub.EventHandlers
{
    public class WorkoutCompletedEventHandler : IIntegrationEventHandler<WorkoutCompletedEvent>
    {
        private readonly ILogger _logger;
        private readonly IHubContext<WorkoutHub> _hubContext;

        public WorkoutCompletedEventHandler(IHubContext<WorkoutHub> hubContext, ILogger<WorkoutCompletedEventHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
        }

        public async Task Handle(WorkoutCompletedEvent completedWorkout)
        {
            _logger.LogInformation("Workout Completed Event Handled, SignalR Hub");

            await _hubContext
                .Clients
                .All
               .SendAsync("WorkoutCompleted", completedWorkout);
        }
    }
}