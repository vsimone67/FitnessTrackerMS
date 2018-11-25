using AutoMapper;
using FitnessTracker.Application.Common;
using FitnessTracker.Application.Workout.Interfaces;
using FitnessTracker.Application.Model.Workout;
using FitnessTracker.Domain.Workout;
using FitnessTracker.Application.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitnessTracker.Application.Command
{
    public class SaveWorkoutCommandHandler : HandlerBase<IWorkoutService>, ICommandHandler<SaveWorkoutCommand, WorkoutDTO>
    {
        public SaveWorkoutCommandHandler(IWorkoutService service, IMapper mapper) : base(service, mapper) { }

        public WorkoutDTO Handle(SaveWorkoutCommand command)
        {
            var workoutDTO = _mapper.Map<FitnessTracker.Domain.Workout.Workout>(command.Workout);
            var workout = _service.SaveWorkout(workoutDTO);
            return _mapper.Map<WorkoutDTO>(workout);

        }

        public async Task<WorkoutDTO> HandleAsync(SaveWorkoutCommand command)
        {
            return await Task.FromResult<WorkoutDTO>(Handle(command));
        }
    }
}