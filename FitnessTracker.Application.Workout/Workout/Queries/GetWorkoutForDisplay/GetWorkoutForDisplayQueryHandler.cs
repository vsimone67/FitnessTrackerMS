using FitnessTracker.Application.Common;
using FitnessTracker.Application.Model.Workout;
using FitnessTracker.Application.Workout.Interfaces;
using FitnessTracker.Domain.Workout;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FitnessTracker.Application.Workout.Queries
{
    public class GetWorkoutForDisplayQueryHandler : HandlerBase<IWorkoutRepository>, IRequestHandler<GetWorkoutForDisplayQuery, WorkoutDisplayDTO>
    {
        // no automapper is needed so pass null
        public GetWorkoutForDisplayQueryHandler(IWorkoutRepository repository) : base(repository, null) { }

        public async Task<WorkoutDisplayDTO> Handle(GetWorkoutForDisplayQuery request, CancellationToken cancellationToken)
        {
            FitnessTracker.Domain.Workout.Workout workout = await _repository.GetWorkoutForDisplayAsync(request.Id);

            WorkoutDisplayDTO retval = new WorkoutDisplayDTO
            {
                WorkoutId = workout.WorkoutId,
                Name = workout.Name,
                Phase = string.Empty,
                isActive = workout.isActive,

                // No AutoMapper conversion here because there is special logic here (e.g. display repos and weight
                Set = workout.Set.OrderBy(set => set.SetOrder).Select(x => new SetDisplayDTO
                {
                    Name = x.SetName.Name,
                    SetId = x.SetId,
                    SetOrder = x.SetOrder,
                    WorkoutId = x.WorkoutId,
                    SetNameId = x.SetNameId,

                    DisplayReps = x.Exercise.First().Reps.OrderBy(ord => ord.RepsName.RepOrder).Select(rep => new RepsDisplayDTO
                    {
                        RepsId = rep.RepsId,
                        Name = rep.RepsName.Name,
                        RepOrder = rep.RepsName.RepOrder,
                        ExerciseId = rep.ExerciseId,
                        SetId = rep.SetId,
                        RepsNameId = rep.RepsNameId,
                    }).Distinct().ToList(),
                    Exercise = x.Exercise.OrderBy(exp => exp.ExerciseOrder).Select(ex => new ExerciseDisplayDTO
                    {
                        ExerciseId = ex.ExerciseId,
                        Name = ex.ExerciseName.Name,
                        ExerciseNameId = ex.ExerciseNameId,
                        ExerciseOrder = ex.ExerciseOrder,
                        Measure = ex.Measure,

                        SetId = x.SetId,
                        Reps = ex.Reps.OrderBy(ord => ord.RepsName.RepOrder).Select(rep => new RepsDisplayDTO
                        {
                            RepsId = rep.RepsId,
                            Weight = FindWeight(workout.DailyWorkout.OrderByDescending(date => date.WorkoutDate).ToList(), x.SetId, ex.ExerciseId, rep.RepsId),
                            Name = rep.RepsName.Name,
                            TimeToNextExercise = rep.TimeToNextExercise,
                            RepOrder = rep.RepsName.RepOrder,
                            ExerciseId = rep.ExerciseId,
                            RepsNameId = rep.RepsNameId,
                            SetId = rep.SetId
                        }).ToList()
                    }).ToList()
                }).ToList()
            };

            CheckSetCount(retval);
            return retval;
        }

        private void CheckSetCount(WorkoutDisplayDTO workout)
        {
            int maxReps = GetMaxReps(workout);

            workout.Set.ForEach(set => { if (maxReps > set.DisplayReps.Count) set.AdditionalSets = maxReps - set.DisplayReps.Count; });
        }

        private int GetMaxReps(WorkoutDisplayDTO workout)
        {
            int maxReps = 0;

            workout.Set.ForEach(set => maxReps = Math.Max(set.DisplayReps.Count, maxReps));

            return maxReps;
        }

        protected int FindWeight(List<DailyWorkout> dailyWorkout, int setId, int exerciseId, int repsId)
        {
            int retVal = 0;

            if (dailyWorkout.Count > 0)
            {
                List<DailyWorkoutInfo> info = dailyWorkout[0].DailyWorkoutInfo.OrderByDescending(x => x.DailyWorkoutId).ToList();
                var workout = info.Find(exp => exp.ExerciseId == exerciseId && exp.SetId == setId && exp.RepsId == repsId);

                retVal = workout.WeightUsed;
            }
            return retVal;
        }
    }
}