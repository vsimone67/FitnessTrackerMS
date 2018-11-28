using FitnessTracker.Presentation.Mobile.Models;
using FitnessTracker.Presentation.Mobile.ViewModels;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitnessTracker.Presentation.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TimerPopup : PopupPage
    {
        protected TimerPopupViewModel viewModel;

        public TimerPopup()
        {
            InitializeComponent();

            MessagingCenter.Subscribe<object, string>(this, MessageConstants.TimeExpired, (sender, message) =>
            {
                OnClose();
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            viewModel = (TimerPopupViewModel)BindingContext;

            // Only start the timer if the time is more than 0
            if (viewModel.TimeToNextExercise > 0)
                viewModel.StartTimer();
        }

        private void OnClose()
        {
            Navigation.PopPopupAsync();
        }
    }
}