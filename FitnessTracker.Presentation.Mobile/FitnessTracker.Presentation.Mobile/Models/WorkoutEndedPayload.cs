using FitnessTracker.Application.Model.Workout;
using System;

namespace FitnessTracker.Mobile.Models
{
    public class WorkoutEndedPayload
    {
        public DateTime StartWorkoutTime { get; set; }
        public WorkoutDisplayDTO SelectedWorkout { get; set; }
    }
}