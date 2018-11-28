using FitnessTracker.Application.Model.Workout;
using FitnessTracker.Presentation.Mobile.Models;
using FitnessTracker.Presentation.Mobile.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace FitnessTracker.Presentation.Mobile.ViewModels
{
    public class WorkoutEndedPopupViewModel : BaseViewModel
    {
        #region Injected Services

        private IWorkoutService _workoutService;

        #endregion Injected Services

        #region Properties

        public DateTime WorkoutStarted { get; set; }
        public WorkoutDisplayDTO SelectedWorkout { get; set; }
        public int WorkoutDuration { get; set; }

        protected string _workoutDurationText = string.Empty;

        public string WorkoutDurationText
        {
            get => _workoutDurationText;
            set => SetProperty(ref _workoutDurationText, value);
        }

        #endregion Properties

        #region Command Creation

        public ICommand SaveWorkoutCommand => new Command(async () => await ExecuteSaveWorkout());  // When next exercise button is presses

        #endregion Command Creation

        public WorkoutEndedPopupViewModel(IWorkoutService workoutService)
        {
            MessagingCenter.Subscribe<object, WorkoutEndedPayload>(this, MessageConstants.PopupTimer, (sender, payload) =>
            {
                WorkoutStarted = payload.StartWorkoutTime;
                SelectedWorkout = payload.SelectedWorkout;
            });

            _workoutService = workoutService;
        }

        #region Command Implementation

        protected async Task ExecuteSaveWorkout()
        {
            try
            {
                IsBusy = true;
                SelectedWorkout.Duration = WorkoutDuration;
                WorkoutDurationText = "Saving Workout";
                await Task.Delay(1000); // Let UI update before save
                await _workoutService.SaveWorkoutAsync(SelectedWorkout);
            }
            catch (Exception ex)
            {
                SendError("Error Saving Workout: " + ex.Message);
            }
            finally
            {
                IsBusy = false;
                WorkoutDurationText = "Workout Has Been Saved.";
            }
        }

        #endregion Command Implementation
    }
}