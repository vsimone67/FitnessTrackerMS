using FitnessTracker.Domain.Workout;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Persistance.Workout
{
    public class WorkoutContext : DbContext
    {
        protected string _connectionString;

        public virtual DbSet<BodyInfo> BodyInfo { get; set; }
        public virtual DbSet<DailyWorkout> DailyWorkout { get; set; }
        public virtual DbSet<DailyWorkoutInfo> DailyWorkoutInfo { get; set; }
        public virtual DbSet<Exercise> Exercise { get; set; }
        public virtual DbSet<ExerciseName> ExerciseName { get; set; }
        public virtual DbSet<Reps> Reps { get; set; }
        public virtual DbSet<RepsName> RepsName { get; set; }
        public virtual DbSet<Set> Set { get; set; }
        public virtual DbSet<SetName> SetName { get; set; }
        public virtual DbSet<FitnessTracker.Domain.Workout.Workout> Workout { get; set; }

        public WorkoutContext(string connectionString)
        {
            _connectionString = connectionString;
            Database.SetCommandTimeout(360);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}