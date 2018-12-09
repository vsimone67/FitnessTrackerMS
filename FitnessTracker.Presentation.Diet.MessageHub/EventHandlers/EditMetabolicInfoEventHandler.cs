using EventBus.Abstractions;
using FitnessTracker.Application.Model.Diet.Events;
using FitnessTracker.Presentation.Diet.MessageHub.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace FitnessTracker.Presentation.Diet.MessageHub.EventHandlers
{
    public class EditMetabolicInfoEventHandler : IIntegrationEventHandler<EditMetabolicInfo>
    {
        private ILogger _logger;
        private readonly IHubContext<DietHub> _hubContext;

        public EditMetabolicInfoEventHandler(IHubContext<DietHub> hubContext, ILogger<EditMetabolicInfoEventHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
        }

        public async Task Handle(EditMetabolicInfo metabolicInfo)
        {
            _logger.LogInformation("Edit Metabolic Info Completed Event Handled, SignalR Hub");

            await _hubContext
                .Clients
                .All
               .SendAsync("EditMetabolicInfo", metabolicInfo);
        }
    }
}