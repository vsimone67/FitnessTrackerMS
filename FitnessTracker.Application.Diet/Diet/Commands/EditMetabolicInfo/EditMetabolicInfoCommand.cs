using FitnessTracker.Application.Common;
using FitnessTracker.Application.Model.Diet;

namespace FitnessTracker.Application.Command
{
    public class EditMetabolicInfoCommand : ICommand
    {
        public MetabolicInfoDTO MetabolicInfo { get; set; }
    }
}