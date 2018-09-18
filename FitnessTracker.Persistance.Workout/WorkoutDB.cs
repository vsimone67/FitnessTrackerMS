using FitnessTracker.Application.Common.Interfaces;
using FitnessTracker.Application.Workout.Interfaces;
using FitnessTracker.Common.Attributes;
using FitnessTracker.Domain.Workout;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FitnessTracker.Persistance.Workout
{
    [AutoRegister(typeof(IWorkoutService))]
    public class WorkoutDB : IWorkoutService
    {
        private WorkoutContext _dbContext;

        public WorkoutDB(IApplicationSettings settings)
        {
            _dbContext = new WorkoutContext(settings.GetConnectionString("FitnessTrackerConnection"));
        }

        public List<FitnessTracker.Domain.Workout.Workout> GetAllWorkouts()
        {
            return _dbContext.Workout.ToList();
        }

        public FitnessTracker.Domain.Workout.Workout GetWorkout(int id)
        {
            // Entity Framework Core does not support lazy loading yet so you have to load what you need prior
            return _dbContext.Workout
                .Include(set => set.Set)
                .ThenInclude(setName => setName.SetName)
                .Include(set => set.Set).ThenInclude(ex => ex.Exercise).ThenInclude(exName => exName.ExerciseName)
                .ThenInclude(ex => ex.Exercise)
                .ThenInclude(rep => rep.Reps)
                .ThenInclude(repName => repName.RepsName)
                .Include(daily => daily.DailyWorkout)
                .ThenInclude(dailyInfo => dailyInfo.DailyWorkoutInfo)
                .ThenInclude(set => set.Set)
                .ThenInclude(ex => ex.Exercise)
                .ThenInclude(rep => rep.Reps)
                .Where(exp => exp.WorkoutId == id)
                .FirstOrDefault<FitnessTracker.Domain.Workout.Workout>();
        }

        public FitnessTracker.Domain.Workout.Workout GetWorkoutForDisplay(int id)
        {
            FitnessTracker.Domain.Workout.Workout workout = GetWorkout(id);
            return workout;
        }

        public List<DailyWorkout> GetSavedWorkout(int id)
        {
            return _dbContext.DailyWorkout.Where(exp => exp.WorkoutId == id).ToList();
        }

        public List<SetName> GetSets()
        {
            return _dbContext.SetName.ToList();
        }

        public List<ExerciseName> GetExercises()
        {
            return _dbContext.ExerciseName.ToList();
        }

        public List<RepsName> GetReps()
        {
            return _dbContext.RepsName.ToList();
        }

        public DailyWorkout SaveDailyWorkout(DailyWorkout workout)
        {
            _dbContext.DailyWorkout.Add(workout);
            SaveChanges();

            return workout;
        }

        public FitnessTracker.Domain.Workout.Workout SaveWorkout(FitnessTracker.Domain.Workout.Workout workout)
        {
            // very weird, the code above saves the reps out of order.  The code below was used in the orginal version and it for some reason saves the reps in the correct order
            FitnessTracker.Domain.Workout.Workout newworkout = new FitnessTracker.Domain.Workout.Workout
            {
                Name = workout.Name,
                isActive = true
            };

            _dbContext.Workout.Add(newworkout);
            SaveChanges();

            foreach (var newSet in workout.Set)
            {
                newworkout.Set.Add(newSet);
                SaveChanges();
            }

            return newworkout;
        }

        public BodyInfo SaveBodyInfo(BodyInfo bodyInfo)
        {
            _dbContext.BodyInfo.Add(bodyInfo);
            SaveChanges();
            return bodyInfo;
        }

        public List<BodyInfo> GetBodyInfo()
        {
            return _dbContext.BodyInfo.ToList();
        }

        protected int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }
    }
}