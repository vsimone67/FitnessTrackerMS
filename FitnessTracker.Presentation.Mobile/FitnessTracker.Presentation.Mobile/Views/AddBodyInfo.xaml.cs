using Xamarin.Forms.Xaml;

namespace FitnessTracker.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddBodyInfo : BaseView
    {
        public AddBodyInfo()
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