using FitnessTracker.Application.Common;
using FitnessTracker.Application.Model.Diet;

namespace FitnessTracker.Application.Diet.Command
{
    public class DeleteFoodItemCommand : ICommand
    {
        public FoodInfoDTO FoodInfo { get; set; }
    }
}