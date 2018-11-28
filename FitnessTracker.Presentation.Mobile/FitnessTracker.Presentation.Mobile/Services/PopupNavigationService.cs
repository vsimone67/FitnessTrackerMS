using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Services;

namespace FitnessTracker.Presentation.Mobile.Services
{
    public class PopupNavigationService : IPopupNavigationService
    {
        public IPopupNavigation Navigation => PopupNavigation.Instance;
    }
}