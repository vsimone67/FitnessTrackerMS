using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessTracker.Domain.Workout
{
    public partial class Reps
    {
        public Reps()
        {
            DailyWorkoutInfo = new HashSet<DailyWorkoutInfo>();            
        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RepsId { get; set; }
        public int ExerciseId { get; set; }
        public int SetId { get; set; }
        public int RepsNameId { get; set; }
        public string TimeToNextExercise { get; set; }
        public virtual ICollection<DailyWorkoutInfo> DailyWorkoutInfo { get; set; }
        [ForeignKey("ExerciseId")]
        public virtual Exercise Exercise { get; set; }
        [ForeignKey("RepsNameId")]
        public virtual RepsName RepsName { get; set; }
        //[ForeignKey("SetId")]
        //public virtual Set Set { get; set; }
    }
}
