using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessTracker.Domain.Workout
{
    public partial class Workout
    {
        public Workout()
        {
            Set = new HashSet<Set>();
            DailyWorkout = new HashSet<DailyWorkout>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WorkoutId { get; set; }
        public string Name { get; set; }
        public bool isActive { get; set; }
        public virtual ICollection<Set> Set { get; set; }
        public virtual ICollection<DailyWorkout> DailyWorkout { get; set; }
    }
}
