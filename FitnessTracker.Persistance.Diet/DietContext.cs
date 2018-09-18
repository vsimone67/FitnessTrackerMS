using FitnessTracker.Domain.Diet;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Persistance.Diet
{
    public class DietContext : DbContext
    {
        protected string _connectionString;

        public DietContext(string connectionString)
        {
            _connectionString = connectionString;
            Database.SetCommandTimeout(360);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        public DbSet<FoodInfo> FoodInfo { get; set; }
        public DbSet<FoodDefault> FoodDefault { get; set; }
        public DbSet<MealInfo> MealInfo { get; set; }
        public DbSet<SavedMenu> SavedMenu { get; set; }

        public virtual DbSet<MetabolicInfo> MetabolicInfo { get; set; }
    }
}