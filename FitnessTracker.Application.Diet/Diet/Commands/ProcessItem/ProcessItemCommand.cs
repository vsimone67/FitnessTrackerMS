﻿using FitnessTracker.Application.Common;
using FitnessTracker.Application.Model.Diet;

namespace FitnessTracker.Application.Diet.Command
{
    public class ProcessItemCommand : ICommand
    {
        public FoodInfoDTO FoodInfo { get; set; }
    }
}