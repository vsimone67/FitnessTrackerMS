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
    public class GetExercisesQueryHandler : HandlerBase<IWorkoutRepository>, IRequestHandler<GetExercisesQuery, List<ExerciseNameDTO>>
    {
        public GetExercisesQueryHandler(IWorkoutRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public async Task<List<ExerciseNameDTO>> Handle(GetExercisesQuery request, CancellationToken cancellationToken)
        {
            var exercises = await _repository.GetExercisesAsync().ConfigureAwait(false);

            return _mapper.Map<List<ExerciseNameDTO>>(exercises.OrderBy(exp => exp.Name));
        }
    }
}