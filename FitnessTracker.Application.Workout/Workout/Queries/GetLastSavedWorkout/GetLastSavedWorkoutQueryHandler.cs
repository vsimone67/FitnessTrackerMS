using AutoMapper;
using FitnessTracker.Application.Common;
using FitnessTracker.Application.Model.Workout;
using FitnessTracker.Application.Workout.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FitnessTracker.Application.Workout.Queries
{
    public class GetLastSavedWorkoutQueryHandler : HandlerBase<IWorkoutRepository>, IRequestHandler<GetLastSavedWorkoutQuery, List<DailyWorkoutDTO>>
    {
        public GetLastSavedWorkoutQueryHandler(IWorkoutRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public async Task<List<DailyWorkoutDTO>> Handle(GetLastSavedWorkoutQuery request, CancellationToken cancellationToken)
        {
            var savedWorkout = await _repository.GetSavedWorkoutAsync(request.Id);

            return _mapper.Map<List<DailyWorkoutDTO>>(savedWorkout);
        }
    }
}