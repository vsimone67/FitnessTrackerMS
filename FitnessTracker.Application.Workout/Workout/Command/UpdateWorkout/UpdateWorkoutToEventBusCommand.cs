using FitnessTracker.Application.Model.Workout;
using MediatR;

namespace FitnessTracker.Application.Workout.Workout.Command
{
    public class UpdateWorkoutToEventBusCommand : IRequest
    {
        public WorkoutDTO Workout { get; set; }
    }
}