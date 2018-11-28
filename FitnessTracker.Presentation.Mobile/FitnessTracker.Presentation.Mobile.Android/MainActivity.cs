using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using FitnessTracker.Presentation.Mobile.IOC;
using FitnessTracker.Presentation.Mobile.Services;
using FitnessTracker.Presentation.Mobile.ViewModels;

namespace FitnessTracker.Presentation.Mobile.Droid
{
    [Activity(Label = "FitnessTracker.Presentation.Mobile", Icon = "@drawable/ic_appicon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            //base.SetTheme(Resource.Style.MainTheme); // Set back to main theme and not the splash screen
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            RegisterDependencies();

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void RegisterDependencies()
        {
            DependencyResolver.Register<IWorkoutService, WorkoutService>();
            DependencyResolver.Register<IPopupNavigationService, PopupNavigationService>();

            // Add View Models for the ViewModel Locator
            DependencyResolver.Register<LogWorkoutViewModel>();
            DependencyResolver.Register<WorkoutEndedPopupViewModel>();
            DependencyResolver.Register<TimerPopupViewModel>();
        }
    }
}