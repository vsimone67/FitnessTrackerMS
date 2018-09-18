using System;
using System.Collections.Generic;

namespace FitnessTracker.Domain.Workout
{
    public partial class RepsName
    {
        public RepsName()
        {
            Reps = new HashSet<Reps>();
        }

        public int RepsNameId { get; set; }
        public string Name { get; set; }
        public int RepOrder { get; set; }
        public virtual ICollection<Reps> Reps { get; set; }
    }
}
