using FitnessTracker.Application.Model.Diet;
using MediatR;

namespace FitnessTracker.Application.Diet.Queries
{
    public class GetMetabolicInfoCalcTypeQuery : IRequest<CurrentMacrosDTO>
    {
        public string Id { get; set; }
    }
}