using AutoMapper;
using FitnessTracker.Application.Common;
using FitnessTracker.Application.Model.Workout;
using FitnessTracker.Application.Workout.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FitnessTracker.Application.Workout.Command
{
    public class SaveWorkoutCommandHandler : HandlerBase<IWorkoutRepository>, IRequestHandler<SaveWorkoutCommand, WorkoutDTO>
    {
        public SaveWorkoutCommandHandler(IWorkoutRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public async Task<WorkoutDTO> Handle(SaveWorkoutCommand request, CancellationToken cancellationToken)
        {
            var workoutDTO = _mapper.Map<FitnessTracker.Domain.Workout.Workout>(request.Workout);
            var workout = await _repository.SaveWorkoutAsync(workoutDTO);
            return _mapper.Map<WorkoutDTO>(workout);
        }
    }
}