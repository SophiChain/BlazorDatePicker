using System;
using System.Globalization;

namespace SophiChain.BlazorDatePicker.Utilities.Converters
{
    /// <summary>
    /// A universal T to string binding converter
    /// </summary>
    public class DefaultConverter<T> : Converter<T>
    {
        public DefaultConverter()
        {
            SetFunc = ConvertToString;
            GetFunc = ConvertFromString;
        }

        protected virtual T? ConvertFromString(string? value)
        {
            try
            {
                // Handle null/empty values
                if (string.IsNullOrEmpty(value))
                    return default(T);

                // string
                if (typeof(T) == typeof(string))
                    return (T)(object)value;

                // datetime
                if (typeof(T) == typeof(DateTime) || typeof(T) == typeof(DateTime?))
                {
                    try
                    {
                        return (T)(object)DateTime.ParseExact(value, Format ?? Culture.DateTimeFormat.ShortDatePattern, Culture);
                    }
                    catch (FormatException)
                    {
                        if (DateTime.TryParse(value, Culture, out var dateTime))
                        {
                            return (T)(object)dateTime;
                        }
                        UpdateGetError("Invalid date format");
                    }
                }

                // Generic fallback using ToString for other types
                return (T)Convert.ChangeType(value, typeof(T), Culture);
            }
            catch (Exception e)
            {
                UpdateGetError($"Conversion error: {e.Message}");
            }

            return default(T);
        }

        protected virtual string? ConvertToString(T? arg)
        {
            if (arg == null)
                return null;
            
            try
            {
                // string
                if (typeof(T) == typeof(string))
                    return (string)(object)arg;

                // datetime
                if (typeof(T) == typeof(DateTime))
                {
                    var value = (DateTime)(object)arg;
                    return value.ToString(Format ?? Culture.DateTimeFormat.ShortDatePattern, Culture);
                }
                else if (typeof(T) == typeof(DateTime?))
                {
                    var value = (DateTime?)(object)arg;
                    return value?.ToString(Format ?? Culture.DateTimeFormat.ShortDatePattern, Culture);
                }

                return arg.ToString();
            }
            catch (FormatException e)
            {
                UpdateSetError($"Conversion error: {e.Message}");
                return null;
            }
        }
    }
}
