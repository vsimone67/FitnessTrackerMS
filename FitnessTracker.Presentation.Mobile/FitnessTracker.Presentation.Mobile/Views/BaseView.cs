using FitnessTracker.Presentation.Mobile.ViewModels;
using Xamarin.Forms;

namespace FitnessTracker.Presentation.Mobile.Views
{
    public partial class BaseView : ContentPage
    {
        public BaseView()
        {
        }

        public void UpdateToolBar()
        {
            BaseViewModel viewModel = (BaseViewModel)BindingContext;
            Title = viewModel.Title;
        }
    }
}