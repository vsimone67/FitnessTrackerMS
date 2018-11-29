namespace FitnessTracker.Common.ExtentionMethods
{
    public static class StringExtentions
    {
        public static string NullToEmpty(this string stringToCheck)
        {
            string retval = stringToCheck;

            if (string.IsNullOrEmpty(stringToCheck))
                retval = string.Empty;

            return retval;
        }
    }
}