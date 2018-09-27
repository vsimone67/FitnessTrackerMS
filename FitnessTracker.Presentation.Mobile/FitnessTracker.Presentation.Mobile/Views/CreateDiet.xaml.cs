using FitnessTracker.Mobile.ViewModels;
using Xamarin.Forms.Xaml;

namespace FitnessTracker.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CreateDiet : BaseView
	{        

        public CreateDiet()
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