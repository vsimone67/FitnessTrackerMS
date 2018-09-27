using FitnessTracker.Mobile.ViewModels;
using Xamarin.Forms;

namespace FitnessTracker.Mobile.Views
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
            ((MainPage)this.Parent).Title = Title;
        }
    }
}