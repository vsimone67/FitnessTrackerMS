using FitnessTracker.Application.Model.Workout;
using MediatR;
using System.Collections.Generic;

namespace FitnessTracker.Application.Workout.Queries
{
    public class GetSetQuery : IRequest<List<SetNameDTO>>
    {
    }
}