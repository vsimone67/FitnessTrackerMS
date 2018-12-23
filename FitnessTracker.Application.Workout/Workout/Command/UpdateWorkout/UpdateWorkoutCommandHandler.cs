using AutoMapper;
using FitnessTracker.Application.Common;
using FitnessTracker.Application.Model.Workout;
using FitnessTracker.Application.Workout.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FitnessTracker.Application.Workout.Workout.Command
{
    public class UpdateWorkoutCommandHandler : HandlerBase<IWorkoutRepository>, IRequestHandler<UpdateWorkoutCommand, WorkoutDTO>
    {
        public UpdateWorkoutCommandHandler(IWorkoutRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public async Task<WorkoutDTO> Handle(UpdateWorkoutCommand request, CancellationToken cancellationToken)
        {
            var workoutDTO = _mapper.Map<FitnessTracker.Domain.Workout.Workout>(request.Workout);
            var workout = await _repository.UpdateWorkoutAsync(workoutDTO);
            return _mapper.Map<WorkoutDTO>(workout);
        }
    }
}