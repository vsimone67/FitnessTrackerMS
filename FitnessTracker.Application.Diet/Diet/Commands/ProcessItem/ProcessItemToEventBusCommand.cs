using FitnessTracker.Application.Model.Diet;
using MediatR;

namespace FitnessTracker.Application.Diet.Diet.Commands
{
    public class ProcessItemToEventBusCommand : IRequest
    {
        public FoodInfoDTO FoodInfo { get; set; }
    }
}