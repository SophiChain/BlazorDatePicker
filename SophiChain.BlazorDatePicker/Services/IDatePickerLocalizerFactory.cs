using System.Globalization;
using SophiChain.BlazorDatePicker.Localizations;

namespace SophiChain.BlazorDatePicker.Services;

/// <summary>
/// Factory service for creating culture-specific localizers
/// </summary>
public interface IDatePickerLocalizerFactory
{
    /// <summary>
    /// Gets the appropriate localizer for the specified culture
    /// </summary>
    /// <param name="culture">The culture to get the localizer for</param>
    /// <returns>The appropriate localizer instance</returns>
    IDatePickerLocalizer GetLocalizer(CultureInfo culture);
    
    /// <summary>
    /// Gets the appropriate localizer based on the current UI culture
    /// </summary>
    /// <returns>The appropriate localizer instance</returns>
    IDatePickerLocalizer GetLocalizer();
}
