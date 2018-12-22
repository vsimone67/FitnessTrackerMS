using FitnessTracker.Domain.Workout;
using System.Collections.Generic;

namespace FitnessTracker.Application.Workout.Interfaces
{
    public interface IWorkoutService
    {
        List<FitnessTracker.Domain.Workout.Workout> GetAllWorkouts();

        List<BodyInfo> GetBodyInfo();

        List<ExerciseName> GetExercises();

        List<RepsName> GetReps();

        List<SetName> GetSets();

        FitnessTracker.Domain.Workout.Workout GetWorkout(int id);

        FitnessTracker.Domain.Workout.Workout GetWorkoutForDisplay(int id);

        BodyInfo SaveBodyInfo(BodyInfo bodyInfo);

        DailyWorkout SaveDailyWorkout(DailyWorkout workout);

        FitnessTracker.Domain.Workout.Workout SaveWorkout(FitnessTracker.Domain.Workout.Workout workout);

        FitnessTracker.Domain.Workout.Workout UpdateWorkout(FitnessTracker.Domain.Workout.Workout workout);

        List<DailyWorkout> GetSavedWorkout(int id);
    }
}