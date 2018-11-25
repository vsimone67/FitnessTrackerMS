using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FitnessTracker.Domain.Workout
{
    public partial class SetName
    {
        public SetName()
        {
            Set = new HashSet<Set>();
        }

        [Key]
        public int SetNameId { get; set; }

        public string Name { get; set; }
        public virtual ICollection<Set> Set { get; set; }
    }
}