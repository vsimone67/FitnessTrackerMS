using FitnessTracker.Application.Model.Workout;
using MediatR;

namespace FitnessTracker.Application.Workout.Workout.Command
{
    public class SaveWorkoutToEventBusCommand : IRequest
    {
        public WorkoutDTO Workout { get; set; }
    }
}