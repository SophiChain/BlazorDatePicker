using Microsoft.JSInterop;

namespace SophiChain.BlazorDatePicker.Services;

/// <summary>
/// Implementation of JavaScript API service.
/// </summary>
public class JsApiService : IJsApiService
{
    private readonly IJSRuntime _jsRuntime;

    public JsApiService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    /// <inheritdoc />
    public ValueTask UpdateStyleProperty(string elementId, string property, object value)
    {
        return _jsRuntime.InvokeVoidAsync("scbJsApi.updateStyleProperty", elementId, property, value);
    }

    /// <inheritdoc />
    public ValueTask<BoundingBox?> GetBoundingClientRect(string elementId)
    {
        return _jsRuntime.InvokeAsync<BoundingBox?>("scbJsApi.getBoundingClientRect", elementId);
    }

    /// <inheritdoc />
    public ValueTask AddClickOutsideListener<T>(string elementId, DotNetObjectReference<T> dotNetRef) where T : class
    {
        return _jsRuntime.InvokeVoidAsync("scbClickOutside.addListener", elementId, dotNetRef);
    }

    /// <inheritdoc />
    public ValueTask RemoveClickOutsideListener(string elementId)
    {
        return _jsRuntime.InvokeVoidAsync("scbClickOutside.removeListener", elementId);
    }

    /// <inheritdoc />
    public ValueTask ScrollToYear(int year)
    {
        return _jsRuntime.InvokeVoidAsync("scbDatePicker.scrollToYear", year);
    }

    /// <inheritdoc />
    public ValueTask ScrollToMonth(int month)
    {
        return _jsRuntime.InvokeVoidAsync("scbDatePicker.scrollToMonth", month);
    }
}
