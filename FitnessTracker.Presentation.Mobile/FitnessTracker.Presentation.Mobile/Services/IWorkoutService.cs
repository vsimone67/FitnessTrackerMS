using FitnessTracker.Application.Model.Workout;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitnessTracker.Mobile.Services
{
    public interface IWorkoutService
    {
        Task<List<WorkoutDTO>> GetAllWorkoutsAsync();

        Task<WorkoutDisplayDTO> GetWorkoutAsync(int id);

        Task<WorkoutDisplayDTO> SaveWorkoutAsync(WorkoutDisplayDTO workout);

        Task<List<DailyWorkoutDTO>> GetSavedWorkouts(int id);
    }
}