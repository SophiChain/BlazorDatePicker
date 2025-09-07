namespace SophiChain.BlazorDatePicker.Services;

/// <summary>
/// Service for managing scroll operations in DatePicker components.
/// </summary>
public interface IScrollManager
{
    /// <summary>
    /// Scrolls to a specific year element in the DatePicker.
    /// </summary>
    /// <param name="elementId">The ID of the year element to scroll to.</param>
    Task ScrollToYearAsync(string elementId);

    /// <summary>
    /// Scrolls to a specific element by ID.
    /// </summary>
    /// <param name="elementId">The ID of the element to scroll to.</param>
    Task ScrollToElementAsync(string elementId);
}