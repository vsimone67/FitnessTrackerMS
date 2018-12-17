using AutoMapper;
using FitnessTracker.Application.Common;
using FitnessTracker.Application.Model.Workout;
using FitnessTracker.Application.Workout.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTracker.Application.Workout.Queries
{
    public class GetExercisesQueryHandler : HandlerBase<IWorkoutService>, IQueryHandler<GetExercisesQuery, List<ExerciseNameDTO>>
    {
        public GetExercisesQueryHandler(IWorkoutService service, IMapper mapper) : base(service, mapper)
        {
        }

        public List<ExerciseNameDTO> Handle(GetExercisesQuery query)
        {
            var exercises = _service.GetExercises().OrderBy(exp => exp.Name).ToList();

            return _mapper.Map<List<ExerciseNameDTO>>(exercises);
        }

        public async Task<List<ExerciseNameDTO>> HandleAsync(GetExercisesQuery query)
        {
            return await Task.Run<List<ExerciseNameDTO>>(() => Handle(query));
        }
    }
}