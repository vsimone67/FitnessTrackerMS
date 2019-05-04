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
    public class GetAllWorkoutsQueryHandler : HandlerBase<IWorkoutRepository>, IRequestHandler<GetAllWorkoutsQuery, List<WorkoutDTO>>
    {
        public GetAllWorkoutsQueryHandler(IWorkoutRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public async Task<List<WorkoutDTO>> Handle(GetAllWorkoutsQuery request, CancellationToken cancellationToken)
        {
            var workouts = await _repository.GetAllWorkoutsAsync().ConfigureAwait(false);

            List<WorkoutDTO> retval;

            if (request.IsActive)
                retval = _mapper.Map<List<WorkoutDTO>>(workouts.Where(exp => exp.isActive).OrderBy(exp => exp.Name)); // only return active
            else
                retval = _mapper.Map<List<WorkoutDTO>>(workouts);  // return all

            return retval;
        }
    }
}