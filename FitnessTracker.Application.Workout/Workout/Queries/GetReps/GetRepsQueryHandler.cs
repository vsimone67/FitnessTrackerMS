using AutoMapper;
using FitnessTracker.Application.Common;
using FitnessTracker.Application.Workout.Interfaces;
using FitnessTracker.Application.Model.Workout;
using FitnetssTracker.Application.Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTracker.Application.Queries
{

    public class GetRepsQueryHanlder : HandlerBase<IWorkoutService>, IQueryHandler<GetRepsQuery, List<RepsNameDTO>>
    {
        public GetRepsQueryHanlder(IWorkoutService service, IMapper mapper) : base(service, mapper) { }
        
        public List<RepsNameDTO> Handle(GetRepsQuery query)
        {
            var reps =  _service.GetReps().OrderBy(exp => exp.RepOrder).ToList();

            return _mapper.Map< List<RepsNameDTO>> (reps);
        }
        public async Task<List<RepsNameDTO>> HandleAsync(GetRepsQuery query)
        {
            return await Task.FromResult<List<RepsNameDTO>>(Handle(query));
        }
    }
}