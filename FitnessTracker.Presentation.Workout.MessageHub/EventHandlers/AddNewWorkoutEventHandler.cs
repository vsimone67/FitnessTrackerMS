using EventBus.Abstractions;
using FitnessTracker.Application.Workout.Events;
using FitnessTracker.Presentation.Workout.MessageHub.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace FitnessTracker.Presentation.Workout.MessageHub.EventHandlers
{
    public class AddNewWorkoutEventHandler : IIntegrationEventHandler<AddNewWorkoutEvent>
    {
        private ILogger _logger;
        private readonly IHubContext<WorkoutHub> _hubContext;

        public AddNewWorkoutEventHandler(IHubContext<WorkoutHub> hubContext, ILogger<AddNewWorkoutEventHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
        }

        public async Task Handle(AddNewWorkoutEvent newWorkout)
        {
            _logger.LogInformation("Workout Event Handled, SignalR Hub");

            await _hubContext
                .Clients
                .All
               .SendAsync("NewWorkoutAdded", newWorkout);
        }
    }
}