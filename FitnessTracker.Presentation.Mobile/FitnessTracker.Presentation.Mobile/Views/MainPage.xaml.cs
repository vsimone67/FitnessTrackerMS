using FitnessTracker.Presentation.Mobile.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitnessTracker.Presentation.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();

            MessagingCenter.Subscribe<object, string>(this, MessageConstants.ApplicationError, (sender, message) => HandleError(message));
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