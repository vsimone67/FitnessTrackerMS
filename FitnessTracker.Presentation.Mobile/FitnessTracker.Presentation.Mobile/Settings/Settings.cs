using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace FitnessTracker.Mobile.Settings
{    
    public static class Settings
    {
        public const string WorkoutServiceURL = "http://vsfitnesstrackerapi.azurewebsites.net/api/Workout/";

        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }
        
        public static int WorkoutsToIncreaseWeight
        {
            get => AppSettings.GetValueOrDefault(nameof(WorkoutsToIncreaseWeight), 2);
            set => AppSettings.AddOrUpdateValue(nameof(WorkoutsToIncreaseWeight), value);
        }

        public static bool IncreaseWorkoutNotification
        {
            get => AppSettings.GetValueOrDefault(nameof(IncreaseWorkoutNotification), true);
            set => AppSettings.AddOrUpdateValue(nameof(IncreaseWorkoutNotification), value);
        }

        public static string ServiceURL
        {
            get => AppSettings.GetValueOrDefault(nameof(ServiceURL), Settings.WorkoutServiceURL);
            set => AppSettings.AddOrUpdateValue(nameof(ServiceURL), value);
        }

    }
}
