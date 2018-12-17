using AutoMapper;
using FitnessTracker.Application.Common;
using FitnessTracker.Application.Model.Workout;
using FitnessTracker.Application.Workout.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitnessTracker.Application.Workout.Queries
{
    public class GetLastSavedWorkoutQueryHandler : HandlerBase<IWorkoutService>, IQueryHandler<GetLastSavedWorkoutQuery, List<DailyWorkoutDTO>>
    {
        public GetLastSavedWorkoutQueryHandler(IWorkoutService service, IMapper mapper) : base(service, mapper)
        {
        }

        public List<DailyWorkoutDTO> Handle(GetLastSavedWorkoutQuery query)
        {
            var savedWorkout = _service.GetSavedWorkout(query.Id);

            return _mapper.Map<List<DailyWorkoutDTO>>(savedWorkout);
        }

        public async Task<List<DailyWorkoutDTO>> HandleAsync(GetLastSavedWorkoutQuery query)
        {
            return await Task.Run<List<DailyWorkoutDTO>>(() => Handle(query));
        }
    }
}