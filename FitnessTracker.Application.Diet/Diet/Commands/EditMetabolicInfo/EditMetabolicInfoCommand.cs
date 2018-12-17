using FitnessTracker.Application.Common;
using FitnessTracker.Application.Model.Diet;

namespace FitnessTracker.Application.Diet.Command
{
    public class EditMetabolicInfoCommand : ICommand
    {
        public MetabolicInfoDTO MetabolicInfo { get; set; }
    }
}