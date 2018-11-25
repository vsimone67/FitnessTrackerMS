using System;

namespace FitnessTracker.Common.ExtentionMethods
{
    public static class TimeSpanExtensions
    {
        public static String TimeLapse(this TimeSpan timeSpan)
        {
            var hours = timeSpan.Hours;
            var minutes = timeSpan.Minutes;

            if (hours > 0) return String.Format("{0} hours {1} minutes", hours, minutes);
            return String.Format("{0} minutes", minutes);
        }
    }
}