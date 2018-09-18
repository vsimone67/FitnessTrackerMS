using FitnessTracker.Application.Model.Diet;
using FitnetssTracker.Application.Common;

namespace FitnessTracker.Application.Queries
{

    public class GetMetabolicInfoCalcTypeQuery : IQuery<CurrentMacrosDTO>
    {
        public string Id { get; set; }
    }
}