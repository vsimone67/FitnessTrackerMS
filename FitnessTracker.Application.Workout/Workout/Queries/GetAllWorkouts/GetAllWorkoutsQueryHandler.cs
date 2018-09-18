using AutoMapper;
using FitnessTracker.Application.Common;
using FitnessTracker.Application.Workout.Interfaces;
using FitnessTracker.Application.Model.Workout;
using FitnessTracker.Domain.Workout;
using FitnetssTracker.Application.Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTracker.Application.Queries
{
    public class GetAllWorkoutsQueryHandler : HandlerBase<IWorkoutService>, IQueryHandler<GetAllWorkoutsQuery, List<WorkoutDTO>>
    {
        public GetAllWorkoutsQueryHandler(IWorkoutService workoutService, IMapper mapper) : base(workoutService, mapper) { }
        
        public List<WorkoutDTO> Handle(GetAllWorkoutsQuery query)
        {
            var workouts =  _service.GetAllWorkouts().Where(exp => exp.isActive).OrderBy(exp => exp.Name).ToList();

            return _mapper.Map<List<WorkoutDTO>>(workouts);
        }

        public async Task<List<WorkoutDTO>> HandleAsync(GetAllWorkoutsQuery query)
        {
            return await Task.FromResult<List<WorkoutDTO>>(Handle(query));            
        }
    }
}
