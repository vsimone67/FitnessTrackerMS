using FitnessTracker.Domain.Workout;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitnessTracker.Application.Workout.Interfaces
{
    public interface IWorkoutRepository
    {
        Task<List<FitnessTracker.Domain.Workout.Workout>> GetAllWorkoutsAsync();

        Task<List<BodyInfo>> GetBodyInfoAsync();

        Task<List<ExerciseName>> GetExercisesAsync();

        Task<List<RepsName>> GetRepsAsync();

        Task<List<SetName>> GetSetsAsync();

        Task<FitnessTracker.Domain.Workout.Workout> GetWorkoutAsync(int id);

        Task<FitnessTracker.Domain.Workout.Workout> GetWorkoutForDisplayAsync(int id);

        Task<BodyInfo> SaveBodyInfoAsync(BodyInfo bodyInfo);

        Task<DailyWorkout> SaveDailyWorkoutAsync(DailyWorkout workout);

        Task<FitnessTracker.Domain.Workout.Workout> SaveWorkoutAsync(FitnessTracker.Domain.Workout.Workout workout);

        Task<FitnessTracker.Domain.Workout.Workout> UpdateWorkoutAsync(FitnessTracker.Domain.Workout.Workout workout);

        Task<List<DailyWorkout>> GetSavedWorkoutAsync(int id);
    }
}