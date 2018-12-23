using FitnessTracker.Application.Common;
using FitnessTracker.Application.Model.Workout;
using MediatR;

namespace FitnessTracker.Application.Workout.Queries
{
    public class GetWorkoutForDisplayQuery : IRequest<WorkoutDisplayDTO>
    {
        public int Id { get; set; }
    }
}