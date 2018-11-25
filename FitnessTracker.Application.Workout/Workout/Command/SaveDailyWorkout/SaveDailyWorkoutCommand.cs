
using FitnessTracker.Application.Model.Workout;
using FitnessTracker.Application.Common;

namespace FitnessTracker.Application.Command
{

    public class SaveDailyWorkoutCommand : ICommand
    {
        public WorkoutDisplayDTO Workout { get; set; }
    }
}