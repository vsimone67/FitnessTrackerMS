using FitnessTracker.Application.Model.Workout;
using FitnessTracker.Mobile.Models;
using FitnessTracker.Mobile.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Threading;

namespace FitnessTracker.Mobile.ViewModels
{
    public class WorkoutEndedPopupViewModel : BaseViewModel
    {
        #region Injected Services
        IWorkoutService _workoutService;
        #endregion

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

        #endregion

        #region Command Creation
        public ICommand SaveWorkoutCommand => new Command(async () => await ExecuteSaveWorkout());  // When next exercise button is presses
        #endregion

        public WorkoutEndedPopupViewModel(IWorkoutService workoutService)
        {
            MessagingCenter.Subscribe<object, WorkoutEndedPayload>(this, MessageConstants.PopupTimer, (sender, payload) => {

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
        #endregion
    }
}
