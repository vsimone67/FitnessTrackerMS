using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessTracker.Domain.Workout
{
    public partial class Exercise
    {
        public Exercise()
        {
            DailyWorkoutInfo = new HashSet<DailyWorkoutInfo>();
            Reps = new HashSet<Reps>();
            
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExerciseId { get; set; }
        public string Measure { get; set; }
        public int SetId { get; set; }
        public int ExerciseNameId { get; set; }
        public int? ExerciseOrder { get; set; }
        public virtual ICollection<DailyWorkoutInfo> DailyWorkoutInfo { get; set; }
        public virtual ICollection<Reps> Reps { get; set; }
        [ForeignKey("ExerciseNameId")]
        public virtual ExerciseName ExerciseName { get; set; }
        [ForeignKey("SetId")]
        public virtual Set Set { get; set; }
    }
}
