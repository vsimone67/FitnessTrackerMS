using System;
using System.ComponentModel;

namespace FitnessTracker.Common.Helpers
{
    public static class StringHelpers
    {
        /// <summary>
        /// Removes the white spaces in a string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string RemoveWhiteSpaces(this string value)
        {
            return value?.Replace(" ", string.Empty);
        }

        /// <summary>
        /// Converts the type of a value to the specified type (T)
        /// </summary>
        /// <typeparam name="T">Type to convert to</typeparam>
        /// <param name="value">Object whose value you wish to convert</param>
        /// <returns>object value in form of T (type)</returns>
        public static T ConvertType<T>(object value)
        {
            // get specified conversion type
            var conversionType = typeof(T);
            var isNullable = Nullable.GetUnderlyingType(conversionType) != null;

            // if conversion type is generic and nullable
            if (conversionType.IsGenericType && conversionType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                // if value is null
                if (string.IsNullOrWhiteSpace(value?.ToString()))
                    return default(T); // return null

                // set conversion type as the true underlying type (not the nullable form)
                conversionType = Nullable.GetUnderlyingType(conversionType);
            }

            // if this is an enumerator
            if (conversionType.IsEnum)
            {
                // convert to integer first
                value = Convert.ToInt32(value);
            }

            var converted = default(T);

            if (conversionType == typeof(Guid) && isNullable)
            {
                // Handle nullable guid
                try
                {
                    var attempt = TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(value.ToString());

                    if (attempt != null)
                    {
                        converted = (T)attempt;
                    }
                }
                catch { }
            }
            else if (conversionType == typeof(Guid) && !isNullable)
            {
                // Handle non-nullable Guid
                converted = (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(value.ToString());
            }
            else
            {
                if (isNullable)
                {
                    // Handle all other nullable types
                    try
                    {
                        var attempt = Convert.ChangeType(value, conversionType);

                        if (attempt != null)
                        {
                            converted = (T)attempt;
                        }
                    }
                    catch { }
                }
                else
                {
                    // Handle all other non-nullable types
                    converted = (T)Convert.ChangeType(value, conversionType);
                }
            }

            return converted;
        }
    }
}