using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FitnessTracker.Application.Model.Workout
{
    public class RepsDTO
    {      
        public int RepsId { get; set; }
        public int ExerciseId { get; set; }
        public int SetId { get; set; }
        public int RepsNameId { get; set; }
        public string TimeToNextExercise { get; set; }
        public int RepsNameRepsNameId { get; set; }
        public string RepsNameName { get; set; }
        public int RepsNameRepOrder { get; set; }
        public IEnumerable<RepsDTO> RepsNameReps { get; set; }
    }
}
