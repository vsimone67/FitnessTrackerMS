using FitnessTracker.Application.Model.Workout;
using MediatR;

namespace FitnessTracker.Application.Workout.Command
{
    public class SaveDailyWorkoutCommand : IRequest<DailyWorkoutDTO>
    {
        public WorkoutDisplayDTO Workout { get; set; }
    }
}