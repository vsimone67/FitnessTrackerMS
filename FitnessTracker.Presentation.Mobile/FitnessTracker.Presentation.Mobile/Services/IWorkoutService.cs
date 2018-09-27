using System.Collections.Generic;
using System.Threading.Tasks;
using FitnessTracker.Application.Model.Workout;

namespace FitnessTracker.Mobile.Services
{
    public interface IWorkoutService
    {
        Task<List<WorkoutDTO>> GetAllWorkoutsAsync();
        Task<WorkoutDisplayDTO> GetWorkoutAsync(int id);
        Task<WorkoutDisplayDTO> SaveWorkoutAsync(WorkoutDisplayDTO workout);
        Task<List<DailyWorkoutDTO>> GetSavedWorkouts(int id);                }
}