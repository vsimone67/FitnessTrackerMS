using EventBus.Abstractions;
using FitnessTracker.Application.Model.Diet.Events;
using FitnessTracker.Presentation.Diet.MessageHub.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace FitnessTracker.Presentation.Diet.MessageHub.EventHandlers
{
    public class DeleteFoodItemEventHandler : IIntegrationEventHandler<DeleteFoodItemEvent>
    {
        private ILogger _logger;
        private readonly IHubContext<DietHub> _hubContext;

        public DeleteFoodItemEventHandler(IHubContext<DietHub> hubContext, ILogger<DeleteFoodItemEventHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
        }

        public async Task Handle(DeleteFoodItemEvent deletedFoodItem)
        {
            _logger.LogInformation("Delete Food Item Event Handled, SignalR Hub");

            await _hubContext
                .Clients
                .All
               .SendAsync("DeleteFoodItem", deletedFoodItem);
        }
    }
}