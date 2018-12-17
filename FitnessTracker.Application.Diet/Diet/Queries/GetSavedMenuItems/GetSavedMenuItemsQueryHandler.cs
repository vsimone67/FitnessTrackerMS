using AutoMapper;
using FitnessTracker.Application.Common;
using FitnessTracker.Application.Diet.Interfaces;
using FitnessTracker.Application.Model.Diet;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTracker.Application.Diet.Queries
{
    public class GetSavedMenuItemsQueryHandler : HandlerBase<IDietService>, IQueryHandler<GetSavedMenuItemsQuery, List<FoodInfoDTO>>
    {
        public GetSavedMenuItemsQueryHandler(IDietService service, IMapper mapper) : base(service, mapper)
        {
        }

        public List<FoodInfoDTO> Handle(GetSavedMenuItemsQuery query)
        {
            var foodList = _service.GetAllFoodData().OrderBy(exp => exp.Item).ToList();

            return _mapper.Map<List<FoodInfoDTO>>(foodList);
        }

        public async Task<List<FoodInfoDTO>> HandleAsync(GetSavedMenuItemsQuery query)
        {
            return await Task.Run<List<FoodInfoDTO>>(() => Handle(query)).ConfigureAwait(false);
        }
    }
}