using FitnessTracker.Application.Model.Diet;
using FitnessTracker.Application.Common;

namespace FitnessTracker.Application.Command
{

    public class EditMetabolicInfoCommand : ICommand
    {
        public MetabolicInfoDTO MetabolicInfo { get; set; }
    }
}