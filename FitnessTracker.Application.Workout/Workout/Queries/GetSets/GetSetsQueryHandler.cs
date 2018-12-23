using AutoMapper;
using FitnessTracker.Application.Common;
using FitnessTracker.Application.Model.Workout;
using FitnessTracker.Application.Workout.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FitnessTracker.Application.Workout.Queries
{
    public class GetSetsQueryHandler : HandlerBase<IWorkoutRepository>, IRequestHandler<GetSetQuery, List<SetNameDTO>>
    {
        public GetSetsQueryHandler(IWorkoutRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public async Task<List<SetNameDTO>> Handle(GetSetQuery request, CancellationToken cancellationToken)
        {
            var setNames = await _service.GetSetsAsync();
            return _mapper.Map<List<SetNameDTO>>(setNames.OrderBy(exp => exp.Name).ToList());
        }
    }
}