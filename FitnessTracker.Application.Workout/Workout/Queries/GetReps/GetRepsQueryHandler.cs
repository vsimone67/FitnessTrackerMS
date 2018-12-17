using AutoMapper;
using FitnessTracker.Application.Common;
using FitnessTracker.Application.Model.Workout;
using FitnessTracker.Application.Workout.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTracker.Application.Workout.Queries
{
    public class GetRepsQueryHanlder : HandlerBase<IWorkoutService>, IQueryHandler<GetRepsQuery, List<RepsNameDTO>>
    {
        public GetRepsQueryHanlder(IWorkoutService service, IMapper mapper) : base(service, mapper)
        {
        }

        public List<RepsNameDTO> Handle(GetRepsQuery query)
        {
            var reps = _service.GetReps().OrderBy(exp => exp.RepOrder).ToList();

            return _mapper.Map<List<RepsNameDTO>>(reps);
        }

        public async Task<List<RepsNameDTO>> HandleAsync(GetRepsQuery query)
        {
            return await Task.Run<List<RepsNameDTO>>(() => Handle(query)).ConfigureAwait(false);
        }
    }
}