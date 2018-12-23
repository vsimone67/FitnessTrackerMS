using FitnessTracker.Application.Model.Diet;
using MediatR;

namespace FitnessTracker.Application.Diet.Diet.Commands
{
    public class DeleteFoodItemToEventBusCommand : IRequest
    {
        public FoodInfoDTO FoodInfo { get; set; }
    }
}