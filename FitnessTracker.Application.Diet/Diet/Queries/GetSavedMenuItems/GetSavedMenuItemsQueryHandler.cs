using AutoMapper;
using FitnessTracker.Application.Common;
using FitnessTracker.Application.Diet.Interfaces;
using FitnessTracker.Application.Model.Diet;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FitnessTracker.Application.Diet.Queries
{
    public class GetSavedMenuItemsQueryHandler : HandlerBase<IDietRepository>, IRequestHandler<GetSavedMenuItemsQuery, List<FoodInfoDTO>>
    {
        public GetSavedMenuItemsQueryHandler(IDietRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public async Task<List<FoodInfoDTO>> Handle(GetSavedMenuItemsQuery request, CancellationToken cancellationToken)
        {
            var foodList = await _repository.GetAllFoodDataAsync();

            return _mapper.Map<List<FoodInfoDTO>>(foodList.OrderBy(exp => exp.Item).ToList());
        }
    }
}