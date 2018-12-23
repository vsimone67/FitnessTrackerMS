using FitnessTracker.Application.Model.Workout;
using MediatR;
using System.Collections.Generic;

namespace FitnessTracker.Application.Workout.Queries
{
    public class GetAllWorkoutsQuery : IRequest<List<WorkoutDTO>>
    {
        public bool IsAll { get; set; } = true;  // default to get all
    }
}