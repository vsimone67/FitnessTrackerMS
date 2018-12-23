using AutoMapper;
using FitnessTracker.Application.Common;
using FitnessTracker.Application.Diet.Interfaces;
using FitnessTracker.Application.Model.Diet;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FitnessTracker.Application.Diet.Queries
{
    public class GetMetabolicInfoQueryHandler : HandlerBase<IDietRepository>, IRequestHandler<GetMetabolicInfoQuery, List<MetabolicInfoDTO>>
    {
        public GetMetabolicInfoQueryHandler(IDietRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public async Task<List<MetabolicInfoDTO>> Handle(GetMetabolicInfoQuery request, CancellationToken cancellationToken)
        {
            var metabolicInfo = await _repository.GetMetabolicInfoAsync();

            return _mapper.Map<List<MetabolicInfoDTO>>(metabolicInfo);
        }
    }
}