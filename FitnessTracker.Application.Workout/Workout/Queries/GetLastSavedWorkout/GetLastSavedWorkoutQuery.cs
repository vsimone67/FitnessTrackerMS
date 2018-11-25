using FitnessTracker.Application.Model.Workout;
using FitnessTracker.Application.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessTracker.Application.Queries
{
    public class GetLastSavedWorkoutQuery : IQuery<List<DailyWorkoutDTO>>
    {
        public int Id { get; set; }
    }

}
