using AutoMapper;
using FitnessTracker.Application.Common;
using FitnessTracker.Application.Workout.Interfaces;
using FitnessTracker.Application.Model.Workout;
using FitnetssTracker.Application.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitnessTracker.Application.Queries
{
    public  class GetLastSavedWorkoutQueryHandler : HandlerBase<IWorkoutService>, IQueryHandler<GetLastSavedWorkoutQuery, List<DailyWorkoutDTO>>
    {
        public GetLastSavedWorkoutQueryHandler(IWorkoutService service, IMapper mapper) : base(service, mapper) { }
        
        public List<DailyWorkoutDTO> Handle(GetLastSavedWorkoutQuery query)
        {
            var savedWorkout = _service.GetSavedWorkout(query.Id);

            return _mapper.Map<List<DailyWorkoutDTO>>(savedWorkout);
        }

        public async Task<List<DailyWorkoutDTO>> HandleAsync(GetLastSavedWorkoutQuery query)
        {
            return await Task.FromResult<List<DailyWorkoutDTO>>(Handle(query));
        }
    }
    
}



