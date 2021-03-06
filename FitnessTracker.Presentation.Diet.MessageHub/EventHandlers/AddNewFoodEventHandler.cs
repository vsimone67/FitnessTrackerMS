﻿using EventBus.Abstractions;
using FitnessTracker.Application.Model.Diet.Events;
using FitnessTracker.Presentation.Diet.MessageHub.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace FitnessTracker.Presentation.Diet.MessageHub.EventHandlers
{
    public class AddNewFoodEventHandler : IIntegrationEventHandler<AddNewFoodEvent>
    {
        private readonly ILogger _logger;
        private readonly IHubContext<DietHub> _hubContext;

        public AddNewFoodEventHandler(IHubContext<DietHub> hubContext, ILogger<AddNewFoodEventHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
        }

        public async Task Handle(AddNewFoodEvent addedFoodItem)
        {
            _logger.LogInformation("Add New Food Event Handled, SignalR Hub");

            await _hubContext
                .Clients
                .All
               .SendAsync("AddNewFood", addedFoodItem);
        }
    }
}