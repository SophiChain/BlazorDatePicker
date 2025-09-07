using Microsoft.AspNetCore.Components;
using SophiChain.BlazorDatePicker.Extensions;
using SophiChain.BlazorDatePicker.Utilities;

namespace SophiChain.BlazorDatePicker.Components;

/// <summary>
/// Represents a picker for dates with multi-cultural support.
/// </summary>
public partial class SCBDatePicker : SCBBaseDatePicker
{
    private DateTime? _selectedDate;

    /// <summary>
    /// Occurs when the Date has changed.
    /// </summary>
    [Parameter]
    public EventCallback<DateTime?> DateChanged { get; set; }

    /// <summary>
    /// The currently selected date.
    /// </summary>
    [Parameter]
    public DateTime? Date
    {
        get => Value;
        set => SetDateAsync(value).CatchAndLog();
    }

    /// <summary>
    /// Whether to show the Clear button.
    /// </summary>
    [Parameter]
    public bool ShowClear { get; set; } = true;

    protected async Task SetDateAsync(DateTime? date)
    {
        if (Value != null && date != null && date.Value.Kind == DateTimeKind.Unspecified)
        {
            date = DateTime.SpecifyKind(date.Value, Value.Value.Kind);
        }

        if (Value != date)
        {
            HighlightedDate = date;

            if (date is not null && IsDayDisabled(date.Value.Date))
            {
                return;
            }

            if (date is not null)
                PickerMonth = new DateTime(Culture.Calendar.GetYear(date.Value), Culture.Calendar.GetMonth(date.Value), 1, Culture.Calendar);

            await SetValueAsync(date);
            await DateChanged.InvokeAsync(date);
        }
    }

    protected DateTime? PreviewDate { get; set; }

    protected override string GetDayClasses(int month, DateTime day)
    {
        var builder = new CssBuilder("scb-day");
        
        builder.AddClass(AdditionalDateClassesFunc?.Invoke(day) ?? string.Empty);
        
        if (day < GetMonthStart(month) || day > GetMonthEnd(month))
            return builder.AddClass("scb-hidden").Build();

        // Check for preview date first (used in modal), then selected date, then actual date
        var dateToCheck = PreviewDate ?? _selectedDate ?? Date;
        
        if (dateToCheck?.Date == day)
            return builder.AddClass("scb-selected").AddClass($"scb-theme-{Color.ToDescriptionString()}").Build();
            
        var today = DateTime.Today;
        if (day == today)
            return builder.AddClass("scb-current scb-button-outlined").AddClass($"scb-button-outlined-{Color.ToDescriptionString()} scb-{Color.ToDescriptionString()}-text").Build();
            
        return builder.Build();
    }

    protected override async Task OnDayClickedAsync(DateTime dateTime)
    {
        _selectedDate = dateTime;
        await SetDateAsync(dateTime);
        StateHasChanged();
    }

    protected override string GetTitleDateString()
    {
        return (_selectedDate ?? Date)?.ToString(TitleDateFormat, Culture) ?? "";
    }

    protected override DateTime GetCalendarStartOfMonth()
    {
        var today = DateTime.Today;
        var date = StartMonth ?? Date ?? today;
        return date.StartOfMonth(Culture);
    }

    protected override int GetCalendarYear(DateTime yearDate)
    {
        var today = DateTime.Today;
        var date = Date ?? today;
        var diff = Culture.Calendar.GetYear(date) - Culture.Calendar.GetYear(yearDate);
        var calendarYear = Culture.Calendar.GetYear(date);
        return calendarYear - diff;
    }
}
