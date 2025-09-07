using Microsoft.AspNetCore.Components;

namespace SophiChain.BlazorDatePicker.Base;

/// <summary>
/// Represents a base class for designing SophiChain Blazor DatePicker components.
/// </summary>
public abstract class SCBComponentBase : ComponentBase
{
    /// <summary>
    /// The CSS classes applied to this component.
    /// </summary>
    [Parameter]
    public string? Class { get; set; }

    /// <summary>
    /// The CSS styles applied to this component.
    /// </summary>
    [Parameter]
    public string? Style { get; set; }

    /// <summary>
    /// The additional HTML attributes to apply to this component.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? UserAttributes { get; set; }
}
