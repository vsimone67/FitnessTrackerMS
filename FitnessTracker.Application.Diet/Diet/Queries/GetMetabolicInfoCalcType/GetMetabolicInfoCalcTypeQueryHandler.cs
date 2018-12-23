using AutoMapper;
using FitnessTracker.Application.Common;
using FitnessTracker.Application.Diet.Interfaces;
using FitnessTracker.Application.Model.Diet;
using FitnessTracker.Domain.Diet;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FitnessTracker.Application.Diet.Queries
{
    public class GetMetabolicInfoCalcTypeQueryHandler : HandlerBase<IDietRepository>, IRequestHandler<GetMetabolicInfoCalcTypeQuery, CurrentMacrosDTO>
    {
        public GetMetabolicInfoCalcTypeQueryHandler(IDietRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public async Task<CurrentMacrosDTO> Handle(GetMetabolicInfoCalcTypeQuery request, CancellationToken cancellationToken)
        {
            List<MetabolicInfo> currentMacroList = await _repository.GetMetabolicInfoAsync();
            CurrentMacros currentMacro = new CurrentMacros();
            currentMacro.HydrateFromMetabolicInfo(currentMacroList, request.Id);

            return _mapper.Map<CurrentMacrosDTO>(currentMacro);
        }
    }
}