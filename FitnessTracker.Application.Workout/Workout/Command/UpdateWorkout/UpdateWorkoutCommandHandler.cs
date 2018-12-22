using AutoMapper;
using FitnessTracker.Application.Common;
using FitnessTracker.Application.Model.Workout;
using FitnessTracker.Application.Workout.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FitnessTracker.Application.Workout.Workout.Command
{
    public class UpdateWorkoutCommandHandler : HandlerBase<IWorkoutService>, ICommandHandler<UpdateWorkoutCommand, WorkoutDTO>
    {
        public UpdateWorkoutCommandHandler(IWorkoutService service, IMapper mapper) : base(service, mapper)
        {
        }

        public WorkoutDTO Handle(UpdateWorkoutCommand command)
        {
            var workoutDTO = _mapper.Map<FitnessTracker.Domain.Workout.Workout>(command.Workout);
            var workout = _service.UpdateWorkout(workoutDTO);
            return _mapper.Map<WorkoutDTO>(workout);
        }

        public async Task<WorkoutDTO> HandleAsync(UpdateWorkoutCommand command)
        {
            return await Task.Run<WorkoutDTO>(() => Handle(command)).ConfigureAwait(false);
        }
    }
}