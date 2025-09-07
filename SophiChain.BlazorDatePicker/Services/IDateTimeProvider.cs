namespace SophiChain.BlazorDatePicker.Services;

/// <summary>
/// Service for providing current date and time in DatePicker components.
/// </summary>
public interface IDateTimeProvider
{
    /// <summary>
    /// Gets the current local date and time.
    /// </summary>
    DateTime GetLocalNow();

    /// <summary>
    /// Gets the current local date (without time component).
    /// </summary>
    DateTime GetLocalToday();

    /// <summary>
    /// Gets the current UTC date and time.
    /// </summary>
    DateTime GetUtcNow();
}

/// <summary>
/// Default implementation of IDateTimeProvider.
/// </summary>
public class DateTimeProvider : IDateTimeProvider
{
    /// <inheritdoc />
    public DateTime GetLocalNow() => DateTime.Now;

    /// <inheritdoc />
    public DateTime GetLocalToday() => DateTime.Today;

    /// <inheritdoc />
    public DateTime GetUtcNow() => DateTime.UtcNow;
}
