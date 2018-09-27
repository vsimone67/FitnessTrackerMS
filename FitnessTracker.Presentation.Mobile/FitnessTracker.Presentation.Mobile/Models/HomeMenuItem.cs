using System;

namespace FitnessTracker.Mobile
{
    public enum MenuType
    {
        CreateDiet,
        LogWorkout,
        AddBodyInfo,
        AddWorkout

    }
    public class HomeMenuItem : BaseModel
    {
        public HomeMenuItem()
        {
            //MenuType = MenuType.CreateDiet;
        }
        public string Icon { get; set; }
        public MenuType MenuType { get; set; }
    }
}

