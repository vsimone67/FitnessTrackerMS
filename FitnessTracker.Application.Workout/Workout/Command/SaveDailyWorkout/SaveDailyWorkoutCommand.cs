using FitnessTracker.Application.Common;
using FitnessTracker.Application.Model.Workout;

namespace FitnessTracker.Application.Command
{
    public class SaveDailyWorkoutCommand : ICommand
    {
        public WorkoutDisplayDTO Workout { get; set; }
    }
}