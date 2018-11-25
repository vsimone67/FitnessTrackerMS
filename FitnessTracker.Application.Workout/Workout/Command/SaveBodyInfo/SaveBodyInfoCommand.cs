using FitnessTracker.Application.Model.Workout;
using FitnessTracker.Application.Common;

namespace FitnessTracker.Application.Command
{
    public class SaveBodyInfoCommand : ICommand
    {
        public BodyInfoDTO BodyInfo { get; set; }
    }
}
