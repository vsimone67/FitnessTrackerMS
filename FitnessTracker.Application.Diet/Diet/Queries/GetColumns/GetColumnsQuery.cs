using FitnessTracker.Application.Common;
using FitnessTracker.Application.Model.Diet;
using MediatR;
using System.Collections.Generic;

namespace FitnessTracker.Application.Diet.Queries
{
    public class GetColumnsQuery : IRequest<List<MealInfoDTO>>
    {
    }
}