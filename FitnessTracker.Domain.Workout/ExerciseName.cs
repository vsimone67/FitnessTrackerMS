using System;
using System.Collections.Generic;

namespace FitnessTracker.Domain.Workout
{
    public partial class ExerciseName
    {
        public ExerciseName()
        {
            Exercise = new HashSet<Exercise>();
        }

        public int ExerciseNameId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Exercise> Exercise { get; set; }
    }
}
