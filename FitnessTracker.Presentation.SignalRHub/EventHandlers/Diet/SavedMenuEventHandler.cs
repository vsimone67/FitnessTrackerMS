using EventBus.Abstractions;
using FitnessTracker.Application.Model.Diet.Events;
using FitnessTracker.Presentation.SignalRHub.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace FitnessTracker.Presentation.SignalRHub.EventHandlers.Diet
{
    public class SavedMenuEventHandler : IIntegrationEventHandler<SaveMenuEvent>
    {
        private ILogger _logger;
        private readonly IHubContext<DietHub> _hubContext;

        public SavedMenuEventHandler(IHubContext<DietHub> hubContext, ILogger<SavedMenuEventHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
        }

        public async Task Handle(SaveMenuEvent savedMenu)
        {
            _logger.LogWarning("Saved Menu Completed Event Handled, SignalR Hub");

            await _hubContext
                .Clients
                .All
               .SendAsync("MenuSaved", savedMenu);
        }
    }
}