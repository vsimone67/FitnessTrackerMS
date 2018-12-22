using FitnessTracker.Application.Common;
using FitnessTracker.Application.Model.Workout;

namespace FitnessTracker.Application.Workout.Workout.Command
{
    public class UpdateWorkoutCommand : ICommand
    {
        public WorkoutDTO Workout { get; set; }
    }
}