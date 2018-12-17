using FitnessTracker.Application.Common;
using FitnessTracker.Application.Model.Workout;

namespace FitnessTracker.Application.Workout.Command
{
    public class SaveDailyWorkoutCommand : ICommand
    {
        public WorkoutDisplayDTO Workout { get; set; }
    }
}