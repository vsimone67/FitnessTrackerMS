using FitnessTracker.Application.Model.Workout;
using FitnessTracker.Presentation.Mobile.Models;
using FitnessTracker.Presentation.Mobile.Services;
using FitnessTracker.Presentation.Mobile.Views;
using Rg.Plugins.Popup.Contracts;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FitnessTracker.Presentation.Mobile.ViewModels
{
    public class LogWorkoutViewModel : BaseViewModel
    {
        #region Injected Services

        private IWorkoutService _workoutService;
        private IPopupNavigation _navigation;

        #endregion Injected Services

        #region Command Creation

        public ICommand LoadWorkoutCommand => new Command(async () => await ExecuteLoadWorkoutsCommand());
        public ICommand GetWorkoutCommand => new Command(async (workoutId) => await ExecuteGetWorkoutCommand(workoutId));
        public ICommand TurnSleepOffCommand => new Command(() => ExecuteTurnOffSleepCommand());
        public ICommand StartRestTimerCommand => new Command(async (TimeToNextExercise) => await ExecuteStartRestTimerCommand(TimeToNextExercise));
        public ICommand RestTimeClickCommand => new Command(async () => await ExecuteRestTimeClickCommand());
        public ICommand WorkoutEndedCommand => new Command(async () => await ExecuteWorkoutEndedCommand());

        #endregion Command Creation

        #region Member Variables

        public DateTime StartWorkoutTime { get; set; }  // Time the workout started
        public int NumberOfClicks { get; set; }  // the number of times the user taps the cell of a rep
        public int MaxExercisesPerWorkout { get; set; }  // now man exercises/rep in the workout
        public bool IsFirstClick { get; set; }  // Is this the first click on the workout screen
        public bool IsIncreaseWeight { get; set; }  // Notification it is time to increase weight
        public int WorkoutsToIncreaseWeight { get; set; } // number of workouts to check to notify to increase weight
        public ObservableCollection<WorkoutDTO> Workouts { get; set; } // The workouts available to select from

        protected WorkoutDisplayDTO _selectedWorkout;

        #endregion Member Variables

        #region Properties

        public WorkoutDisplayDTO SelectedWorkout
        {
            get => _selectedWorkout;
            set => SetProperty(ref _selectedWorkout, value);
        }

        #endregion Properties

        public LogWorkoutViewModel(IWorkoutService workoutService, IPopupNavigationService navigation)
        {
            Workouts = new ObservableCollection<WorkoutDTO>();

            _workoutService = workoutService;
            Title = "Log Workout";
            NumberOfClicks = 0;
            IsFirstClick = false;
            IsIncreaseWeight = false;
            WorkoutsToIncreaseWeight = 2;
            _navigation = navigation.Navigation;
        }

        #region Command Implementations

        public async Task ExecuteLoadWorkoutsCommand()
        {
            Workouts.Clear();
            IsBusy = true;

            try
            {
                var workouts = await _workoutService.GetAllWorkoutsAsync();
                workouts.ForEach(workout => Workouts.Add(workout));
            }
            catch (Exception ex)
            {
                SendError("Error getting workouts: " + ex.Message);  // Send error to global error page
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task ExecuteGetWorkoutCommand(object workoutId)
        {
            IsBusy = true;
            MaxExercisesPerWorkout = 0;
            IsIncreaseWeight = false;
            try
            {
                SelectedWorkout = await _workoutService.GetWorkoutAsync((int)workoutId);

                // Get the number of exercises/rep for the workout
                foreach (var set in SelectedWorkout.Set)
                {
                    foreach (var exercise in set.Exercise)
                    {
                        foreach (var rep in exercise.Reps)
                        {
                            MaxExercisesPerWorkout++;
                        }
                    }
                }

                await CheckToIncreaseWeight(SelectedWorkout.WorkoutId);
            }
            catch (Exception ex)
            {
                SendError("Error getting workout ID: " + ex.Message);  // Send error to global error page
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void ExecuteTurnOffSleepCommand()
        {
            //ScreenLock.RequestActive();
        }

        public async Task ExecuteStartRestTimerCommand(object timeToNextExercise)
        {
            var page = new TimerPopup();
            // Send the data over to the view model of the pop-up timer
            MessagingCenter.Send<object, int>(this, MessageConstants.PopupTimer, (int)timeToNextExercise);
            await _navigation.PushAsync(page, false);
        }

        public async Task ExecuteRestTimeClickCommand()
        {
            int restTime = 0;

            // Get the max rest time from the workout and pass that to the RestTimer

            if (SelectedWorkout != null)
            {
                SelectedWorkout.Set.ForEach(set => set.Exercise.ForEach(exercise => exercise.Reps.ForEach(reps => restTime = Math.Max(restTime, int.Parse(reps.TimeToNextExercise)))));

                await ExecuteStartRestTimerCommand(restTime);
            }
        }

        public async Task ExecuteWorkoutEndedCommand()
        {
            var page = new WorkoutEndedPopup();
            // make workout payload and send it to the view model
            WorkoutEndedPayload payload = new WorkoutEndedPayload() { SelectedWorkout = SelectedWorkout, StartWorkoutTime = StartWorkoutTime };
            MessagingCenter.Send<object, WorkoutEndedPayload>(this, MessageConstants.PopupTimer, payload);
            await _navigation.PushAsync(page, false);
        }

        protected async Task CheckToIncreaseWeight(int workoutId)
        {
            var savedWorkouts = await _workoutService.GetSavedWorkouts(workoutId);

            // check to see if it is time to increase weight in the workout
            if (savedWorkouts.Count > 0 && Settings.Settings.IncreaseWorkoutNotification)
            {
                IsIncreaseWeight = (savedWorkouts.Count % Settings.Settings.WorkoutsToIncreaseWeight) == 0;
            }
        }

        #endregion Command Implementations
    }
}