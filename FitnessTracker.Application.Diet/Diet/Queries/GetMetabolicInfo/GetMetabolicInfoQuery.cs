using FitnessTracker.Application.Model.Diet;
using MediatR;
using System.Collections.Generic;

namespace FitnessTracker.Application.Diet.Queries
{
    public class GetMetabolicInfoQuery : IRequest<List<MetabolicInfoDTO>>
    {
    }
}