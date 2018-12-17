using AutoMapper;
using FitnessTracker.Application.Common;
using FitnessTracker.Application.Model.Workout;
using FitnessTracker.Application.Workout.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTracker.Application.Workout.Queries
{
    public class GetSetsQueryHandler : HandlerBase<IWorkoutService>, IQueryHandler<GetSetQuery, List<SetNameDTO>>
    {
        public GetSetsQueryHandler(IWorkoutService service, IMapper mapper) : base(service, mapper)
        {
        }

        public List<SetNameDTO> Handle(GetSetQuery query)
        {
            var setNames = _service.GetSets().OrderBy(exp => exp.Name).ToList();
            return _mapper.Map<List<SetNameDTO>>(setNames);
        }

        public async Task<List<SetNameDTO>> HandleAsync(GetSetQuery query)
        {
            return await Task.Run<List<SetNameDTO>>(() => Handle(query)).ConfigureAwait(false);
        }
    }
}