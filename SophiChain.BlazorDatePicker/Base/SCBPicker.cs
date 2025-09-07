using System.Globalization;
using Microsoft.AspNetCore.Components;
using SophiChain.BlazorDatePicker.Utilities.Converters;

namespace SophiChain.BlazorDatePicker.Base;

/// <summary>
/// A base class for creating picker components.
/// </summary>
/// <typeparam name="T">The type of value being chosen.</typeparam>
public abstract class SCBPicker<T> : SCBComponentBase
{
    private T? _value;
    private string? _text;

    /// <summary>
    /// Creates a new picker instance.
    /// </summary>
    public SCBPicker() : this(new DefaultConverter<T>()) { }

    /// <summary>
    /// Creates a new picker instance with a custom converter.
    /// </summary>
    /// <param name="converter">The converter to use for value transformation.</param>
    protected SCBPicker(Utilities.Converters.Converter<T, string> converter)
    {
        Converter = converter;
    }

    /// <summary>
    /// The converter used to transform values between T and string.
    /// </summary>
    protected Utilities.Converters.Converter<T, string> Converter { get; }

    /// <summary>
    /// The culture information used for formatting.
    /// </summary>
    [Parameter]
    public CultureInfo Culture 
    { 
        get => Converter.Culture; 
        set => SetCulture(value);
    }

    /// <summary>
    /// The currently selected value.
    /// </summary>
    [Parameter]
    public T? Value 
    {
        get => _value;
        set
        {
            if (EqualityComparer<T>.Default.Equals(_value, value))
                return;
            _value = value;
            _ = ValueChanged.InvokeAsync(value);
            _ = SetText(Converter.Set(value));
        }
    }

    /// <summary>
    /// Occurs when the Value has changed.
    /// </summary>
    [Parameter]
    public EventCallback<T?> ValueChanged { get; set; }

    /// <summary>
    /// The text representation of the current value.
    /// </summary>
    [Parameter]
    public string? Text 
    {
        get => _text;
        set => _ = SetText(value);
    }

    /// <summary>
    /// Occurs when Text has changed.
    /// </summary>
    [Parameter]
    public EventCallback<string?> TextChanged { get; set; }

    /// <summary>
    /// The placeholder text to display when no value is set.
    /// </summary>
    [Parameter]
    public string? Placeholder { get; set; }

    /// <summary>
    /// The label for the component.
    /// </summary>
    [Parameter]
    public string? Label { get; set; }

    /// <summary>
    /// Whether the picker is disabled.
    /// </summary>
    [Parameter]
    public bool Disabled { get; set; }

    /// <summary>
    /// Whether the picker is readonly.
    /// </summary>
    [Parameter]
    public bool ReadOnly { get; set; }

    protected T? GetValue() => _value;

    protected virtual bool SetCulture(CultureInfo value)
    {
        if (Converter.Culture.Equals(value))
            return false;
        
        Converter.Culture = value;
        return true;
    }

    protected virtual async Task SetText(string? value)
    {
        if (_text == value)
            return;
            
        _text = value;
        await TextChanged.InvokeAsync(value);
        StateHasChanged();
    }

    protected virtual async Task SetValueAsync(T? value)
    {
        if (EqualityComparer<T>.Default.Equals(_value, value))
            return;

        _value = value;
        await SetText(Converter.Set(value));
        await ValueChanged.InvokeAsync(value);
        StateHasChanged();
    }
}
