using FitnessTracker.Application.Model.Workout;
using MediatR;

namespace FitnessTracker.Application.Workout.Workout.Command
{
    public class SaveDailyWorkoutToEventBusCommand : IRequest
    {
        public DailyWorkoutDTO Workout { get; set; }
    }
}