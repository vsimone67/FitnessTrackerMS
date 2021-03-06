﻿using FitnessTracker.Presentation.Mobile.ViewModels;
using Xamarin.Forms.Xaml;

namespace FitnessTracker.Presentation.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SystemSettings : BaseView
    {
        private SystemSettingsViewModel viewModel;

        public SystemSettings()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            UpdateToolBar();

            viewModel = (SystemSettingsViewModel)BindingContext;

            // Get Default Values for items prior to view load, this will enable the screen to show the values
            viewModel.SetWorkoutInterval.Execute(null);
            viewModel.SetWorkoutWeightNotification.Execute(null);
            viewModel.SetServiceURL.Execute(null);
        }
    }
}