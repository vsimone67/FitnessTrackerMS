using AutoMapper;
using FitnessTracker.Application.Common;
using FitnessTracker.Application.Model.Workout;
using FitnessTracker.Application.Workout.Interfaces;
using FitnessTracker.Domain.Workout;

using FitnetssTracker.Application.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitnessTracker.Application.Command
{
    public class SaveDailyWorkoutCommandHandler : HandlerBase<IWorkoutService>, ICommandHandler<SaveDailyWorkoutCommand, DailyWorkoutDTO>
    {
        public SaveDailyWorkoutCommandHandler(IWorkoutService service, IMapper mapper) : base(service, mapper)
        {
        }

        public DailyWorkoutDTO Handle(SaveDailyWorkoutCommand command)
        {
            DailyWorkout dailyWorkout = new DailyWorkout();
            WorkoutDisplayDTO workout = command.Workout;

            dailyWorkout.Phase = workout.Phase;
            dailyWorkout.WorkoutDate = DateTime.Now;
            dailyWorkout.WorkoutId = workout.WorkoutId;
            dailyWorkout.Duration = workout.Duration;

            foreach (SetDisplayDTO set in workout.Set)
            {
                set.Exercise.ForEach(ex => ex.Reps.ForEach(rep => dailyWorkout.DailyWorkoutInfo.Add(new DailyWorkoutInfo
                {
                    ExerciseId = ex.ExerciseId,
                    SetId = ex.SetId,
                    RepsId = rep.RepsId,
                    WeightUsed = rep.Weight,
                    WorkoutId = workout.WorkoutId
                })));
            }

            DailyWorkout savedWorkout = _service.SaveDailyWorkout(dailyWorkout);

            return _mapper.Map<DailyWorkoutDTO>(savedWorkout);
        }

        public async Task<DailyWorkoutDTO> HandleAsync(SaveDailyWorkoutCommand command)
        {
            return await Task.FromResult<DailyWorkoutDTO>(Handle(command));
        }
    }
}