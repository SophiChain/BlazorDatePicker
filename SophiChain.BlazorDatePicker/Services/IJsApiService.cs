using Microsoft.JSInterop;

namespace SophiChain.BlazorDatePicker.Services;

/// <summary>
/// Service for JavaScript API operations in DatePicker components.
/// </summary>
public interface IJsApiService
{
    /// <summary>
    /// Updates a CSS style property on an element.
    /// </summary>
    /// <param name="elementId">The ID of the element to update.</param>
    /// <param name="property">The CSS property name.</param>
    /// <param name="value">The CSS property value.</param>
    ValueTask UpdateStyleProperty(string elementId, string property, object value);

    /// <summary>
    /// Gets the bounding box of an element.
    /// </summary>
    /// <param name="elementId">The ID of the element.</param>
    ValueTask<BoundingBox?> GetBoundingClientRect(string elementId);

    /// <summary>
    /// Adds a click outside listener to an element.
    /// </summary>
    ValueTask AddClickOutsideListener<T>(string elementId, DotNetObjectReference<T> dotNetRef) where T : class;

    /// <summary>
    /// Removes a click outside listener from an element.
    /// </summary>
    ValueTask RemoveClickOutsideListener(string elementId);

    /// <summary>
    /// Scrolls to a specific year element in the year list.
    /// </summary>
    /// <param name="year">The year to scroll to.</param>
    ValueTask ScrollToYear(int year);

    /// <summary>
    /// Scrolls to a specific month element in the month list.
    /// </summary>
    /// <param name="month">The month to scroll to (1-12).</param>
    ValueTask ScrollToMonth(int month);
}

/// <summary>
/// Represents the bounding box of an element.
/// </summary>
public class BoundingBox
{
    public double X { get; set; }
    public double Y { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
    public double Top { get; set; }
    public double Right { get; set; }
    public double Bottom { get; set; }
    public double Left { get; set; }
}
