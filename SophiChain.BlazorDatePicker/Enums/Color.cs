using System.ComponentModel;

namespace SophiChain.BlazorDatePicker;

/// <summary>
/// The color themes available for DatePicker components.
/// </summary>
public enum Color
{
    /// <summary>
    /// The default color theme.
    /// </summary>
    [Description("default")]
    Default,

    /// <summary>
    /// The primary color theme, usually the main color used in the application.
    /// </summary>
    [Description("primary")]
    Primary,

    /// <summary>
    /// The secondary color theme, often used for accents and highlights.
    /// </summary>
    [Description("secondary")]
    Secondary,

    /// <summary>
    /// The info color theme, used to indicate informational messages.
    /// </summary>
    [Description("info")]
    Info,

    /// <summary>
    /// The success color theme, used to indicate successful operations.
    /// </summary>
    [Description("success")]
    Success,

    /// <summary>
    /// The warning color theme, used to indicate potential issues or warnings.
    /// </summary>
    [Description("warning")]
    Warning,

    /// <summary>
    /// The error color theme, used to indicate errors or dangerous actions.
    /// </summary>
    [Description("error")]
    Error,

    /// <summary>
    /// A dark color theme.
    /// </summary>
    [Description("dark")]
    Dark,
}
