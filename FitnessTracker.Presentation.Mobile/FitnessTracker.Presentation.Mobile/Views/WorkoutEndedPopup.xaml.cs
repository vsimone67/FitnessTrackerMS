using FitnessTracker.Mobile.Models;
using FitnessTracker.Mobile.ViewModels;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitnessTracker.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorkoutEndedPopup : PopupPage
    {
        protected WorkoutEndedPopupViewModel viewModel;

        public WorkoutEndedPopup()
        {
            InitializeComponent();

            MessagingCenter.Subscribe<object, string>(this, MessageConstants.WorkoutSaved, (sender, message) =>
            {
                CloseDialogAsync();
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            viewModel = (WorkoutEndedPopupViewModel)BindingContext;
            viewModel.WorkoutDuration = (DateTime.Now.Subtract(viewModel.WorkoutStarted).Hours * 60) + DateTime.Now.Subtract(viewModel.WorkoutStarted).Minutes;
            viewModel.WorkoutDurationText = "The Workout Duration Was: " + viewModel.WorkoutDuration.ToString() + " minutes";
        }

        private void SaveWorkout_Clicked(object sender, EventArgs e)
        {
            Button saveButton = (Button)sender;
            saveButton.IsEnabled = false;
            viewModel.SaveWorkoutCommand.Execute(true);
        }

        private void CloseDialog_Clicked(object sender, EventArgs e)
        {
            CloseDialogAsync();
        }

        private async Task CloseDialogAsync()
        {
            await Navigation.PopPopupAsync();
        }
    }
}