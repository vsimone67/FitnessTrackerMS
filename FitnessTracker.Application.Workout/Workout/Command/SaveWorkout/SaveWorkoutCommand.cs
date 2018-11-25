using FitnessTracker.Application.Model.Workout;
using FitnessTracker.Application.Common;

namespace FitnessTracker.Application.Command
{

    public class SaveWorkoutCommand : ICommand
    {
        public WorkoutDTO Workout { get; set; }
    }
}