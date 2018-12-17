﻿using FitnessTracker.Application.Common;
using FitnessTracker.Application.Model.Workout;

namespace FitnessTracker.Application.Workout.Command
{
    public class SaveWorkoutCommand : ICommand
    {
        public WorkoutDTO Workout { get; set; }
    }
}