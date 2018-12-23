using FitnessTracker.Application.Common;
using FitnessTracker.Application.Model.Workout;
using MediatR;

namespace FitnessTracker.Application.Workout.Workout.Command
{
    public class UpdateWorkoutCommand : IRequest<WorkoutDTO>
    {
        public WorkoutDTO Workout { get; set; }
    }
}