using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FitnessTracker.Mobile.Services
{
    public class PopupNavigationService : IPopupNavigationService
    {
        public IPopupNavigation Navigation =>PopupNavigation.Instance;
    }
    

}
