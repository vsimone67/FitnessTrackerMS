using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessTracker.Domain.Workout
{
    public partial class Set
    {
        public Set()
        {
            DailyWorkoutInfo = new HashSet<DailyWorkoutInfo>();
            Exercise = new HashSet<Exercise>();
            Reps = new HashSet<Reps>();                
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SetId { get; set; }
        public int WorkoutId { get; set; }
        public int SetNameId { get; set; }
        public int? SetOrder { get; set; }
        public virtual ICollection<DailyWorkoutInfo> DailyWorkoutInfo { get; set; }
        public virtual ICollection<Exercise> Exercise { get; set; }
        public virtual ICollection<Reps> Reps { get; set; }
        //[ForeignKey("SetNameId")]
        public virtual SetName SetName { get; set; }
        [ForeignKey("WorkoutId")]
        public virtual Workout Workout { get; set; }
    }
}
