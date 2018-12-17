using AutoMapper;
using FitnessTracker.Application.Common;
using FitnessTracker.Application.Diet.Interfaces;
using FitnessTracker.Application.Model.Diet;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitnessTracker.Application.Diet.Queries
{
    public class GetMetabolicInfoQueryHandler : HandlerBase<IDietService>, IQueryHandler<GetMetabolicInfoQuery, List<MetabolicInfoDTO>>
    {
        public GetMetabolicInfoQueryHandler(IDietService service, IMapper mapper) : base(service, mapper)
        {
        }

        public List<MetabolicInfoDTO> Handle(GetMetabolicInfoQuery query)
        {
            var metabolicInfo = _service.GetMetabolicInfo();

            return _mapper.Map<List<MetabolicInfoDTO>>(metabolicInfo);
        }

        public async Task<List<MetabolicInfoDTO>> HandleAsync(GetMetabolicInfoQuery query)
        {
            return await Task.Run<List<MetabolicInfoDTO>>(() => Handle(query)).ConfigureAwait(false);
        }
    }
}