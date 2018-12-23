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
    public class GetColumnsQueryHandler : HandlerBase<IDietRepository>, IRequestHandler<GetColumnsQuery, List<MealInfoDTO>>
    {
        public GetColumnsQueryHandler(IDietRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public async Task<List<MealInfoDTO>> Handle(GetColumnsQuery request, CancellationToken cancellationToken)
        {
            var columns = await _repository.GetColumnsAsync();

            return _mapper.Map<List<MealInfoDTO>>(columns);
        }
    }
}