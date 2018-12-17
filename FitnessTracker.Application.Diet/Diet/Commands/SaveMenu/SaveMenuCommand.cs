using FitnessTracker.Application.Common;
using FitnessTracker.Application.Model.Diet;
using System.Collections.Generic;

namespace FitnessTracker.Application.Diet.Command
{
    public class SaveMenuCommand : ICommand
    {
        public List<NutritionInfoDTO> Menu { get; set; }
    }
}