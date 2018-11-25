using FitnessTracker.Application.Common;
using FitnessTracker.Application.Model.Diet;

namespace FitnessTracker.Application.Command
{
    public class DeleteFoodItemCommand : ICommand
    {
        public FoodInfoDTO FoodInfo { get; set; }
    }
}