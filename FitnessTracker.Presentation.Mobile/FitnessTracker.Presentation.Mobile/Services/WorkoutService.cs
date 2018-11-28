using FitnessTracker.Application.Model.Workout;
using FitnessTracker.Common.HTTP;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitnessTracker.Presentation.Mobile.Services
{
    public class WorkoutService : IWorkoutService
    {
        protected string _WorkoutURL;

        public WorkoutService()
        {
            _WorkoutURL = Settings.Settings.ServiceURL;
        }

        public Task<List<WorkoutDTO>> GetAllWorkoutsAsync()
        {
            return HttpHelper.GetAsync<List<WorkoutDTO>>(_WorkoutURL + "GetWorkouts");
        }

        public Task<WorkoutDisplayDTO> GetWorkoutAsync(int id)
        {
            return HttpHelper.GetAsync<WorkoutDisplayDTO>(_WorkoutURL + "GetWorkoutForDisplay/" + id.ToString());
        }

        public Task<WorkoutDisplayDTO> SaveWorkoutAsync(WorkoutDisplayDTO workout)
        {
            return HttpHelper.PutAsync<WorkoutDisplayDTO>(_WorkoutURL + "SaveDailyWorkout/", workout);
        }

        public Task<List<DailyWorkoutDTO>> GetSavedWorkouts(int id)
        {
            return HttpHelper.GetAsync<List<DailyWorkoutDTO>>(_WorkoutURL + "GetLastSavedWorkout/" + id.ToString());
        }
    }
}