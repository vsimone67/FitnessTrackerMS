using FitnessTracker.Application.Model.Diet;
using MediatR;
using System.Collections.Generic;

namespace FitnessTracker.Application.Diet.Diet.Commands
{
    public class SaveMenuToEventBusCommand : IRequest
    {
        public List<NutritionInfoDTO> Menu { get; set; }
    }
}