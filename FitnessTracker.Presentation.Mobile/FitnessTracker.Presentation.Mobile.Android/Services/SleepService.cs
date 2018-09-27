using Android.App;
using Android.Views;
using FitnessTracker.Mobile.Droid.Services;
using FitnessTracker.Mobile.Services;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(SleepService))]

namespace FitnessTracker.Mobile.Droid.Services
{
    public class SleepService : ISleep
    {
        public SleepService()
        {
        }
        public void SleepOff()
        {
            Window window = (Forms.Context as Activity).Window;
            window.SetFlags(WindowManagerFlags.KeepScreenOn, WindowManagerFlags.KeepScreenOn);

        }
    }
}