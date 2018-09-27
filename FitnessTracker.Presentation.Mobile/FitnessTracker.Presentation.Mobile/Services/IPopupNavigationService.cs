using Rg.Plugins.Popup;
using Rg.Plugins.Popup.Contracts;

namespace FitnessTracker.Mobile.Services
{
    public interface IPopupNavigationService
    {
        IPopupNavigation Navigation { get; }
    }
}
