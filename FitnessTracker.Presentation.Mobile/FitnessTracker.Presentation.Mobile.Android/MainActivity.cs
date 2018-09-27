using Android.App;
using Android.Content.PM;
using Android.OS;
using FitnessTracker.Mobile.Droid.Services;
using FitnessTracker.Mobile.IOC;
using FitnessTracker.Mobile.Services;
using FitnessTracker.Mobile.ViewModels;

namespace FitnessTracker.Presentation.Mobile.Droid
{
    [Activity(Label = "FitnessTracker.Presentation.Mobile", Theme = "@style/splashscreen", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.SetTheme(Resource.Style.MainTheme); // Set back to main theme and not the splash screen
            base.OnCreate(savedInstanceState);

            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);

            RegisterDependencies();

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }

        private void RegisterDependencies()
        {
            DependencyResolver.Register<ISleep, SleepService>();
            DependencyResolver.Register<IWorkoutService, WorkoutService>();
            DependencyResolver.Register<IPopupNavigationService, PopupNavigationService>();

            // Add View Modeles for the ViewModel Locator
            DependencyResolver.Register<LogWorkoutViewModel>();
            DependencyResolver.Register<AddBodyInfoViewModel>();
            DependencyResolver.Register<AddWorkoutViewModel>();
            DependencyResolver.Register<CreateDietViewModel>();
            DependencyResolver.Register<WorkoutEndedPopupViewModel>();
            DependencyResolver.Register<TimerPopupViewModel>();
        }
    }
}