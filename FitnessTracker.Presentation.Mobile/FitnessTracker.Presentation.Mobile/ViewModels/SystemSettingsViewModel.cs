using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace FitnessTracker.Mobile.ViewModels
{
    public class SystemSettingsViewModel : BaseViewModel
    {
        #region Command Creation

        public ICommand SetWorkoutInterval => new Command(() => ExecuteSetWorkoutInterval());
        public ICommand SetWorkoutWeightNotification => new Command(() => ExecuteSetWorkoutWeightNotification());
        public ICommand SetServiceURL => new Command(() => ExecuteSetServiceURL());

        #endregion Command Creation

        #region Properties

        protected List<int> workoutIntervals = new List<int>
        { 1,2,3,4,5};

        public List<int> WorkoutIntervals => workoutIntervals;

        private int intervalIndex;

        public int IntervalIndex
        {
            get => intervalIndex;
            set
            {
                SetProperty(ref intervalIndex, value);
                Settings.Settings.WorkoutsToIncreaseWeight = WorkoutIntervals[intervalIndex];
            }
        }

        protected bool isNotify;

        public bool IsNotify
        {
            get => isNotify;
            set
            {
                SetProperty(ref isNotify, value);
                Settings.Settings.IncreaseWorkoutNotification = isNotify;
            }
        }

        protected string serviceURL;

        public string ServiceURL
        {
            get => serviceURL;
            set
            {
                SetProperty(ref serviceURL, value);
                Settings.Settings.ServiceURL = serviceURL;
            }
        }

        #endregion Properties

        public SystemSettingsViewModel()
        {
            Title = "Settings";
        }

        #region Command Implementation

        protected void ExecuteSetWorkoutInterval()
        {
            IntervalIndex = Settings.Settings.WorkoutsToIncreaseWeight - 1; // there are 5 values but the index is 0 based so just subtract one
        }

        protected void ExecuteSetWorkoutWeightNotification()
        {
            IsNotify = Settings.Settings.IncreaseWorkoutNotification;
        }

        protected void ExecuteSetServiceURL()
        {
            ServiceURL = Settings.Settings.ServiceURL;
        }

        #endregion Command Implementation
    }
}