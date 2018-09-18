using FitnetssTracker.Application.Common;
using System;
using System.Collections.Generic;
using FitnessTracker.Domain.Workout;
using FitnessTracker.Application.Model.Workout;

namespace FitnessTracker.Application.Queries
{
    public class GetAllWorkoutsQuery : IQuery<List<WorkoutDTO>>
    {
    }
}
