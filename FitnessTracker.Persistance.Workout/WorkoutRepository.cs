using FitnessTracker.Application.Workout.Interfaces;
using FitnessTracker.Common.AppSettings;
using FitnessTracker.Common.Attributes;
using FitnessTracker.Domain.Workout;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTracker.Persistance.Workout
{
    [AutoRegister(typeof(IWorkoutRepository))]
    public class WorkoutRepository : IWorkoutRepository
    {
        private readonly WorkoutContext _dbContext;

        public WorkoutRepository(IOptions<FitnessTrackerSettings> settings)
        {
            _dbContext = new WorkoutContext(settings.Value.ConnectionString);
        }

        public async Task<List<Domain.Workout.Workout>> GetAllWorkoutsAsync()
        {
            return await _dbContext.Workout.ToListAsync();
        }

        public async Task<List<BodyInfo>> GetBodyInfoAsync()
        {
            return await _dbContext.BodyInfo.ToListAsync();
        }

        public async Task<List<ExerciseName>> GetExercisesAsync()
        {
            return await _dbContext.ExerciseName.ToListAsync();
        }

        public async Task<List<RepsName>> GetRepsAsync()
        {
            return await _dbContext.RepsName.ToListAsync();
        }

        public async Task<List<DailyWorkout>> GetSavedWorkoutAsync(int id)
        {
            return await _dbContext.DailyWorkout.Where(exp => exp.WorkoutId == id).ToListAsync();
        }

        public async Task<List<SetName>> GetSetsAsync()
        {
            return await _dbContext.SetName.ToListAsync();
        }

        public async Task<Domain.Workout.Workout> GetWorkoutAsync(int id)
        {
            return await _dbContext.Workout
              .Include(set => set.Set)
              .ThenInclude(setName => setName.SetName)
              .Include(set => set.Set).ThenInclude(ex => ex.Exercise)
              .ThenInclude(exName => exName.ExerciseName)
              .ThenInclude(ex => ex.Exercise)
              .ThenInclude(rep => rep.Reps)
              .ThenInclude(repName => repName.RepsName)
              .Include(daily => daily.DailyWorkout)
              .ThenInclude(dailyInfo => dailyInfo.DailyWorkoutInfo)
              .ThenInclude(set => set.Set)
              .ThenInclude(ex => ex.Exercise)
              .ThenInclude(rep => rep.Reps)
              .Where(exp => exp.WorkoutId == id)
              
              .AsNoTracking()
              .FirstAsync<Domain.Workout.Workout>();
        }

        public async Task<Domain.Workout.Workout> GetWorkoutForDisplayAsync(int id)
        {
            return await GetWorkoutAsync(id);
        }

        public async Task<BodyInfo> SaveBodyInfoAsync(BodyInfo bodyInfo)
        {
            _dbContext.BodyInfo.Add(bodyInfo);
            await SaveChangesAsync();
            return bodyInfo;
        }

        public async Task<DailyWorkout> SaveDailyWorkoutAsync(DailyWorkout workout)
        {
            _dbContext.DailyWorkout.Add(workout);
            await SaveChangesAsync();

            return workout;
        }

        public async Task<Domain.Workout.Workout> SaveWorkoutAsync(Domain.Workout.Workout workout)
        {
            // very weird, the code above saves the reps out of order.  The code below was used in the orginal version and it for some reason saves the reps in the correct order
            FitnessTracker.Domain.Workout.Workout newworkout = new FitnessTracker.Domain.Workout.Workout
            {
                Name = workout.Name,
                isActive = true
            };

            _dbContext.Workout.Add(newworkout);
            await SaveChangesAsync();

            foreach (var newSet in workout.Set)
            {
                newworkout.Set.Add(newSet);
                await SaveChangesAsync();
            }

            return newworkout;
        }

        public async Task<Domain.Workout.Workout> UpdateWorkoutAsync(Domain.Workout.Workout workout)
        {
            _dbContext.Workout.Update(workout);
            await SaveChangesAsync();

            return workout;
        }

        protected async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}