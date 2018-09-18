using AutoMapper;
using FitnessTracker.Application.Common;
using FitnessTracker.Application.Interfaces;
using FitnessTracker.Application.Model.Diet;
using FitnetssTracker.Application.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitnessTracker.Application.Queries
{

    public class GetMetabolicInfoQueryHandler : HandlerBase<IDietService>, IQueryHandler<GetMetabolicInfoQuery, List<MetabolicInfoDTO>>
    {
        public GetMetabolicInfoQueryHandler(IDietService service, IMapper mapper) : base(service, mapper) { }
       
        public List<MetabolicInfoDTO> Handle(GetMetabolicInfoQuery query)
        {
            var metabolicInfo =  _service.GetMetabolicInfo();

            return _mapper.Map<List<MetabolicInfoDTO>>(metabolicInfo);
        }
        public async Task<List<MetabolicInfoDTO>> HandleAsync(GetMetabolicInfoQuery query)
        {
            return await Task.FromResult<List<MetabolicInfoDTO>>(Handle(query));
        }
    }
}