using EventBus.Abstractions;
using FitnessTracker.Application.Workout.Events;
using FitnessTracker.Presentation.SignalRHub.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace FitnessTracker.Presentation.SignalRHub.EventHandlers.Workout
{
    public class BodyInfoSavedEventHandler : IIntegrationEventHandler<BodyInfoSavedEvent>
    {
        private ILogger _logger;
        private readonly IHubContext<WorkoutHub> _hubContext;

        public BodyInfoSavedEventHandler(IHubContext<WorkoutHub> hubContext, ILogger<BodyInfoSavedEventHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
        }

        public async Task Handle(BodyInfoSavedEvent newBodyInfo)
        {
            _logger.LogWarning("BodyInfo Saved Event Handled, SignalR Hub");

            await _hubContext
                .Clients
                .All
               .SendAsync("BodyInfoSaved", newBodyInfo);
        }
    }
}