using FitnessTracker.Mobile.Models;
using System;
using Xamarin.Forms;

namespace FitnessTracker.Mobile.ViewModels
{
    public class TimerPopupViewModel : BaseViewModel
    {
        #region Properties
        protected int _timeToNextExercise;
        public int TimeToNextExercise
        {
            get => _timeToNextExercise;
            set => SetProperty(ref _timeToNextExercise, value);
        }
        #endregion

        public TimerPopupViewModel()
        {          
            MessagingCenter.Subscribe<object, int>(this, MessageConstants.PopupTimer, (sender, timeToNextExercise) => {
                TimeToNextExercise = timeToNextExercise;
            });
        }

        #region Methods
        public void StartTimer()
        {            
            Device.StartTimer(TimeSpan.FromSeconds(1), TimeElapsed);
        }

        // Thisis called every second from the timer class
        private bool TimeElapsed()
        {
            bool isKeepAlive = true;  // True = keep timer going, false = end

            TimeToNextExercise--;
          
            if (TimeToNextExercise == 0)  // if time elapsed or the workout is done, turn off the timer
            {                 
                isKeepAlive = false; // Stop Timer                
                SendMessage<string>(MessageConstants.TimeExpired, "OK");
            }            

            return isKeepAlive;
        }
        #endregion
    }
}
