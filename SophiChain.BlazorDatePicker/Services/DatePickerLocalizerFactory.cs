using System.Globalization;
using SophiChain.BlazorDatePicker.Localizations;

namespace SophiChain.BlazorDatePicker.Services;

/// <summary>
/// Factory service for creating culture-specific localizers
/// </summary>
public class DatePickerLocalizerFactory : IDatePickerLocalizerFactory
{
    /// <summary>
    /// Gets the appropriate localizer for the specified culture
    /// </summary>
    /// <param name="culture">The culture to get the localizer for</param>
    /// <returns>The appropriate localizer instance</returns>
    public IDatePickerLocalizer GetLocalizer(CultureInfo culture)
    {
        var cultureName = culture.Name.ToLowerInvariant();
        
        return cultureName switch
        {
            var c when c.StartsWith("es") => new SpanishDatePickerLocalizer(),
            var c when c.StartsWith("fr") => new FrenchDatePickerLocalizer(),
            var c when c.StartsWith("fa") => new PersianDatePickerLocalizer(),
            var c when c.StartsWith("ar") => new ArabicDatePickerLocalizer(),
            var c when c.StartsWith("de") => new GermanDatePickerLocalizer(),
            _ => new DefaultDatePickerLocalizer()
        };
    }
    
    /// <summary>
    /// Gets the appropriate localizer based on the current UI culture
    /// </summary>
    /// <returns>The appropriate localizer instance</returns>
    public IDatePickerLocalizer GetLocalizer()
    {
        return GetLocalizer(CultureInfo.CurrentUICulture);
    }
}

/// <summary>
/// Factory service that always returns a specific custom localizer type
/// </summary>
/// <typeparam name="TLocalizer">The type of localizer to always return</typeparam>
public class CustomLocalizerFactory<TLocalizer> : IDatePickerLocalizerFactory
    where TLocalizer : IDatePickerLocalizer, new()
{
    private readonly TLocalizer _localizer = new();

    /// <summary>
    /// Gets the custom localizer (ignores the culture parameter)
    /// </summary>
    /// <param name="culture">The culture (ignored)</param>
    /// <returns>The custom localizer instance</returns>
    public IDatePickerLocalizer GetLocalizer(CultureInfo culture)
    {
        return _localizer;
    }
    
    /// <summary>
    /// Gets the custom localizer
    /// </summary>
    /// <returns>The custom localizer instance</returns>
    public IDatePickerLocalizer GetLocalizer()
    {
        return _localizer;
    }
}
