namespace SophiChain.BlazorDatePicker.Extensions;

/// <summary>
/// Extension methods for Task handling.
/// </summary>
public static class TaskExtensions
{
    /// <summary>
    /// Executes the Task asynchronously as a fire-and-forget operation and handles any exceptions.
    /// </summary>
    /// <param name="task">The task to be executed.</param>
    /// <param name="ignoreExceptions">If set to true, exceptions are ignored; otherwise, exceptions are logged to console.</param>
    public static async void CatchAndLog(this Task task, bool ignoreExceptions = false)
    {
        try
        {
            await task;
        }
        catch (Exception ex)
        {
            if (!ignoreExceptions)
            {
                Console.WriteLine($"[SCB DatePicker] Unhandled exception: {ex}");
            }
        }
    }

    /// <summary>
    /// Executes the ValueTask asynchronously as a fire-and-forget operation and handles any exceptions.
    /// </summary>
    /// <param name="task">The task to be executed.</param>
    /// <param name="ignoreExceptions">If set to true, exceptions are ignored; otherwise, exceptions are logged to console.</param>
    public static async void CatchAndLog(this ValueTask task, bool ignoreExceptions = false)
    {
        try
        {
            await task;
        }
        catch (Exception ex)
        {
            if (!ignoreExceptions)
            {
                Console.WriteLine($"[SCB DatePicker] Unhandled exception: {ex}");
            }
        }
    }

    /// <summary>
    /// Executes the ValueTask&lt;T&gt; asynchronously as a fire-and-forget operation and handles any exceptions.
    /// </summary>
    /// <param name="task">The task to be executed.</param>
    /// <param name="ignoreExceptions">If set to true, exceptions are ignored; otherwise, exceptions are logged to console.</param>
    public static async void CatchAndLog<T>(this ValueTask<T> task, bool ignoreExceptions = false)
    {
        try
        {
            await task;
        }
        catch (Exception ex)
        {
            if (!ignoreExceptions)
            {
                Console.WriteLine($"[SCB DatePicker] Unhandled exception: {ex}");
            }
        }
    }
}
