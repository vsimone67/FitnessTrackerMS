using AutoMapper;
using FitnessTracker.Application.Common;
using FitnessTracker.Application.Interfaces;
using FitnessTracker.Application.Model.Diet;
using FitnessTracker.Domain.Diet;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitnessTracker.Application.Queries
{
    public class GetMetabolicInfoCalcTypeQueryHandler : HandlerBase<IDietService>, IQueryHandler<GetMetabolicInfoCalcTypeQuery, CurrentMacrosDTO>
    {
        public GetMetabolicInfoCalcTypeQueryHandler(IDietService service, IMapper mapper) : base(service, mapper)
        {
        }

        public CurrentMacrosDTO Handle(GetMetabolicInfoCalcTypeQuery query)
        {
            List<MetabolicInfo> currentMacroList = _service.GetMetabolicInfo();
            CurrentMacros currentMacro = new CurrentMacros();
            currentMacro.HydrateFromMetabolicInfo(currentMacroList, query.Id);

            return _mapper.Map<CurrentMacrosDTO>(currentMacro);
        }

        public async Task<CurrentMacrosDTO> HandleAsync(GetMetabolicInfoCalcTypeQuery query)
        {
            return await Task.FromResult<CurrentMacrosDTO>(Handle(query));
        }
    }
}