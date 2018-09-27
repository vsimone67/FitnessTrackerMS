using FitnessTracker.Mobile.Models;
using FitnessTracker.Mobile.Views;

using Xamarin.Forms;

namespace FitnessTracker.Mobile
{
    public class MainPage : TabbedPage
    {
        public MainPage()
        {
            Children.Add(new LogWorkout());
            Children.Add(new SystemSettings());

            MessagingCenter.Subscribe<object, string>(this, MessageConstants.ApplicationError, (sender, message) =>
            {
                HandleError(message);
            });
        }

        protected void HandleError(string message)
        {
            DisplayAlert("Error", message, "OK");
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            Title = CurrentPage?.Title ?? string.Empty;
        }
    }
}