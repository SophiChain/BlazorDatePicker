using System.ComponentModel;
using System.Reflection;

namespace SophiChain.BlazorDatePicker.Extensions;

/// <summary>
/// Extension methods for enums.
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    /// Gets the description string from the Description attribute of an enum value.
    /// </summary>
    /// <param name="value">The enum value.</param>
    /// <returns>The description string, or the enum name if no description is found.</returns>
    public static string ToDescriptionString(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        if (field != null)
        {
            var attribute = field.GetCustomAttribute<DescriptionAttribute>();
            if (attribute != null)
            {
                return attribute.Description;
            }
        }
        return value.ToString().ToLowerInvariant();
    }
}
