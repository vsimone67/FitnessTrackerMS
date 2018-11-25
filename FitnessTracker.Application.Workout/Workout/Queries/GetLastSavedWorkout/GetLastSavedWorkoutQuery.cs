using FitnessTracker.Application.Common;
using FitnessTracker.Application.Model.Workout;
using System.Collections.Generic;

namespace FitnessTracker.Application.Queries
{
    public class GetLastSavedWorkoutQuery : IQuery<List<DailyWorkoutDTO>>
    {
        public int Id { get; set; }
    }
}