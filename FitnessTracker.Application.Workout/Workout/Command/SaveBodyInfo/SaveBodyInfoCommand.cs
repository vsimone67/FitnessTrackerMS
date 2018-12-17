using FitnessTracker.Application.Common;
using FitnessTracker.Application.Model.Workout;

namespace FitnessTracker.Application.Workout.Command
{
    public class SaveBodyInfoCommand : ICommand
    {
        public BodyInfoDTO BodyInfo { get; set; }
    }
}