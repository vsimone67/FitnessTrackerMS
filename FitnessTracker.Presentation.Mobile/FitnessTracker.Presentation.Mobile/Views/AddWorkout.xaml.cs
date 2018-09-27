using FitnessTracker.Mobile.ViewModels;
using Xamarin.Forms.Xaml;

namespace FitnessTracker.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddWorkout : BaseView
    {        

        public AddWorkout()
        {
            InitializeComponent();
        
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            UpdateToolBar();

        }

    }
}