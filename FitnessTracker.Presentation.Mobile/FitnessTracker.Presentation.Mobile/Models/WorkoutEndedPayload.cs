using FitnessTracker.Application.Model.Workout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessTracker.Mobile.Models
{
    public class WorkoutEndedPayload
    {
        public DateTime StartWorkoutTime { get; set; }
        public WorkoutDisplayDTO SelectedWorkout { get; set; }
    }
}
