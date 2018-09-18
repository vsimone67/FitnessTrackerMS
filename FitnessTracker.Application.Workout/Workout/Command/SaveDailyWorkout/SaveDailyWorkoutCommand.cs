
using FitnessTracker.Application.Model.Workout;
using FitnetssTracker.Application.Common;

namespace FitnessTracker.Application.Command
{

    public class SaveDailyWorkoutCommand : ICommand
    {
        public WorkoutDisplayDTO Workout { get; set;}
    }
}