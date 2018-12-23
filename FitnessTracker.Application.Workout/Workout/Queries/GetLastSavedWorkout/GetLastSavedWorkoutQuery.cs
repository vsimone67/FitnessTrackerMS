using FitnessTracker.Application.Model.Workout;
using MediatR;
using System.Collections.Generic;

namespace FitnessTracker.Application.Workout.Queries
{
    public class GetLastSavedWorkoutQuery : IRequest<List<DailyWorkoutDTO>>
    {
        public int Id { get; set; }
    }
}