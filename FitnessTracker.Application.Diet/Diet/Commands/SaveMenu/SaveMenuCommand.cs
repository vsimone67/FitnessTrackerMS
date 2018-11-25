using FitnessTracker.Application.Model.Diet;
using FitnessTracker.Application.Common;
using System.Collections.Generic;

namespace FitnessTracker.Application.Command
{

    public class SaveMenuCommand : ICommand
    {
        public List<NutritionInfoDTO> Menu { get; set; }
    }
}