using Microsoft.JSInterop;

namespace SophiChain.BlazorDatePicker.Services;

/// <summary>
/// Implementation of scroll management service.
/// </summary>
public class ScrollManager : IScrollManager
{
    private readonly IJSRuntime _jsRuntime;

    public ScrollManager(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    /// <inheritdoc />
    public Task ScrollToYearAsync(string elementId)
    {
        return _jsRuntime.InvokeVoidAsync("scbScrollManager.scrollToYear", elementId).AsTask();
    }

    /// <inheritdoc />
    public Task ScrollToElementAsync(string elementId)
    {
        return _jsRuntime.InvokeVoidAsync("scbScrollManager.scrollToElement", elementId).AsTask();
    }
}
