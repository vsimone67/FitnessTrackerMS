using FitnessTracker.Application.Common;
using FitnessTracker.Application.Model.Diet;

namespace FitnessTracker.Application.Queries
{
    public class GetMetabolicInfoCalcTypeQuery : IQuery<CurrentMacrosDTO>
    {
        public string Id { get; set; }
    }
}