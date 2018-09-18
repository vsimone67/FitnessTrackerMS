using FitnessTracker.Application.Model.Diet;
using FitnetssTracker.Application.Common;

namespace FitnessTracker.Application.Command
{

    public class ProcessItemCommand : ICommand
    {
        public FoodInfoDTO FoodInfo { get; set; }
    }
}