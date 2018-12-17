using AutoMapper;
using FitnessTracker.Application.Common;
using FitnessTracker.Application.Diet.Interfaces;
using FitnessTracker.Application.Model.Diet;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitnessTracker.Application.Diet.Queries
{
    public class GetColumnsQueryHandler : HandlerBase<IDietService>, IQueryHandler<GetColumnsQuery, List<MealInfoDTO>>
    {
        public GetColumnsQueryHandler(IDietService service, IMapper mapper) : base(service, mapper)
        {
        }

        public List<MealInfoDTO> Handle(GetColumnsQuery query)
        {
            var columns = _service.GetColumns();

            return _mapper.Map<List<MealInfoDTO>>(columns);
        }

        public async Task<List<MealInfoDTO>> HandleAsync(GetColumnsQuery query)
        {
            return await Task.Run<List<MealInfoDTO>>(() => Handle(query)).ConfigureAwait(false);
        }
    }
}