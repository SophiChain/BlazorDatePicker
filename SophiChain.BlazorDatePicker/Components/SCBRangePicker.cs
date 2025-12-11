using System.Globalization;
using Microsoft.AspNetCore.Components;
using SophiChain.BlazorDatePicker.Extensions;
using SophiChain.BlazorDatePicker.Utilities;

namespace SophiChain.BlazorDatePicker.Components;

/// <summary>
/// Represents a comprehensive date range picker with presets, manual input, and visual calendar selection.
/// Provides three ways to select ranges: quick presets, text inputs, and dual-month calendar with Apply/Cancel confirmation.
/// </summary>
public partial class SCBRangePicker : SCBBaseDatePicker
{
    private DateTime? _firstDate = null, _secondDate;
    private DateRange? _dateRange;
    private DateRange? _previewRange;
    private bool _isInSelectionMode = false;
    private bool _showPicker = false;
    private RangeShortcut? _selectedPreset = null;
    private List<DateRange> _recentRanges = new();
    private bool _hasValidRange = false;

    protected override bool IsRange => true;

    /// <summary>
    /// Creates a new instance.
    /// </summary>
    public SCBRangePicker()
    {
        DisplayMonths = 2;
    }

    /// <summary>
    /// The maximum number of selectable days.
    /// </summary>
    [Parameter]
    public int? MaxDays { get; set; }

    /// <summary>
    /// The minimum number of selectable days.
    /// </summary>
    [Parameter]
    public int? MinDays { get; set; }

    /// <summary>
    /// Whether to restrict selections to past dates only.
    /// </summary>
    [Parameter]
    public bool PastOnly { get; set; }

    /// <summary>
    /// Whether to restrict selections to future dates only.
    /// </summary>
    [Parameter]
    public bool FutureOnly { get; set; }

    /// <summary>
    /// The list of range shortcuts to display. If null, uses default presets.
    /// </summary>
    [Parameter]
    public RangeShortcut[]? Presets { get; set; }

    /// <summary>
    /// Whether to show the Clear button.
    /// </summary>
    [Parameter]
    public bool ShowClear { get; set; } = true;

    /// <summary>
    /// Whether to remember recent custom ranges.
    /// </summary>
    [Parameter]
    public bool RememberRecentRanges { get; set; } = true;

    /// <summary>
    /// The label for the Apply button.
    /// </summary>
    [Parameter]
    public string ApplyButtonText { get; set; } = "Apply";

    /// <summary>
    /// The label for the Cancel button.
    /// </summary>
    [Parameter]
    public string CancelButtonText { get; set; } = "Cancel";

    /// <summary>
    /// The label for the Clear button.
    /// </summary>
    [Parameter]
    public string ClearButtonText { get; set; } = "Clear";

    /// <summary>
    /// Occurs when DateRange has changed and been applied.
    /// </summary>
    [Parameter]
    public EventCallback<DateRange?> DateRangeChanged { get; set; }

    /// <summary>
    /// Occurs when the picker is opened.
    /// </summary>
    [Parameter]
    public EventCallback OnOpen { get; set; }

    /// <summary>
    /// Occurs when the picker is closed.
    /// </summary>
    [Parameter]
    public EventCallback OnClose { get; set; }

    /// <summary>
    /// Occurs when the range preview changes (before apply).
    /// </summary>
    [Parameter]
    public EventCallback<DateRange?> OnPreviewChange { get; set; }

    /// <summary>
    /// Occurs when Apply is clicked.
    /// </summary>
    [Parameter]
    public EventCallback<DateRange?> OnApply { get; set; }

    /// <summary>
    /// Occurs when Cancel is clicked.
    /// </summary>
    [Parameter]
    public EventCallback OnCancel { get; set; }

    /// <summary>
    /// Occurs when Clear is clicked.
    /// </summary>
    [Parameter]
    public EventCallback OnClear { get; set; }

    /// <summary>
    /// The currently selected and confirmed date range.
    /// </summary>
    [Parameter]
    public DateRange? DateRange
    {
        get => _dateRange;
        set => SetDateRangeAsync(value).CatchAndLog();
    }

    /// <summary>
    /// The currently previewed range (before apply).
    /// </summary>
    public DateRange? PreviewRange => _previewRange;

    /// <summary>
    /// Gets the default presets when none are specified.
    /// </summary>
    protected RangeShortcut[] GetDefaultPresets()
    {
        var defaults = new List<RangeShortcut>
        {
            RangeShortcut.Today,
            RangeShortcut.Yesterday,
            RangeShortcut.Last7Days,
            RangeShortcut.Last30Days,
            RangeShortcut.Last90Days,
            RangeShortcut.WeekToDate,
            RangeShortcut.MonthToDate,
            RangeShortcut.Custom
        };

        // Filter based on PastOnly/FutureOnly flags
        if (PastOnly)
        {
            defaults.RemoveAll(p => p == RangeShortcut.Next7Days || p == RangeShortcut.Next14Days || 
                                   p == RangeShortcut.Next30Days || p == RangeShortcut.Next90Days ||
                                   p == RangeShortcut.NextWeek || p == RangeShortcut.NextMonth ||
                                   p == RangeShortcut.NextQuarter || p == RangeShortcut.NextYear);
        }
        else if (FutureOnly)
        {
            defaults.RemoveAll(p => p == RangeShortcut.Yesterday || p == RangeShortcut.Last7Days || 
                                   p == RangeShortcut.Last14Days || p == RangeShortcut.Last30Days ||
                                   p == RangeShortcut.Last60Days || p == RangeShortcut.Last90Days ||
                                   p == RangeShortcut.LastWeek || p == RangeShortcut.LastMonth ||
                                   p == RangeShortcut.LastQuarter || p == RangeShortcut.LastYear ||
                                   p == RangeShortcut.WeekToDate || p == RangeShortcut.MonthToDate ||
                                   p == RangeShortcut.QuarterToDate || p == RangeShortcut.YearToDate ||
                                   p == RangeShortcut.Rolling7Days || p == RangeShortcut.Rolling30Days ||
                                   p == RangeShortcut.Rolling90Days || p == RangeShortcut.PreviousBusinessWeek ||
                                   p == RangeShortcut.PreviousBusinessMonth);
        }

        return defaults.ToArray();
    }

    /// <summary>
    /// Gets the active preset list.
    /// </summary>
    protected RangeShortcut[] GetActivePresets()
    {
        return Presets ?? GetDefaultPresets();
    }

    /// <summary>
    /// Converts a range shortcut to an actual date range.
    /// </summary>
    protected DateRange? GetRangeForShortcut(RangeShortcut shortcut)
    {
        var today = DateTime.Today;
        var firstDayOfWeek = GetFirstDayOfWeek();

        return shortcut switch
        {
            RangeShortcut.Today => new DateRange(today, today),
            RangeShortcut.Yesterday => new DateRange(today.AddDays(-1), today.AddDays(-1)),
            RangeShortcut.Last7Days => new DateRange(today.AddDays(-6), today),
            RangeShortcut.Last14Days => new DateRange(today.AddDays(-13), today),
            RangeShortcut.Last30Days => new DateRange(today.AddDays(-29), today),
            RangeShortcut.Last60Days => new DateRange(today.AddDays(-59), today),
            RangeShortcut.Last90Days => new DateRange(today.AddDays(-89), today),
            RangeShortcut.LastWeek => GetLastWeek(),
            RangeShortcut.LastMonth => GetLastMonth(),
            RangeShortcut.LastQuarter => GetLastQuarter(),
            RangeShortcut.LastYear => GetLastYear(),
            RangeShortcut.WeekToDate => new DateRange(today.StartOfWeek(firstDayOfWeek), today),
            RangeShortcut.MonthToDate => new DateRange(today.StartOfMonth(Culture), today),
            RangeShortcut.QuarterToDate => GetQuarterToDate(),
            RangeShortcut.YearToDate => new DateRange(new DateTime(today.Year, 1, 1), today),
            RangeShortcut.Rolling7Days => new DateRange(today.AddDays(-7), today),
            RangeShortcut.Rolling30Days => new DateRange(today.AddDays(-30), today),
            RangeShortcut.Rolling90Days => new DateRange(today.AddDays(-90), today),
            RangeShortcut.ThisWeek => GetThisWeek(),
            RangeShortcut.ThisMonth => GetThisMonth(),
            RangeShortcut.ThisQuarter => GetThisQuarter(),
            RangeShortcut.ThisYear => new DateRange(new DateTime(today.Year, 1, 1), new DateTime(today.Year, 12, 31)),
            RangeShortcut.Next1Day => new DateRange(today, today.AddDays(1)),
            RangeShortcut.Next2Days => new DateRange(today, today.AddDays(2)),
            RangeShortcut.Next3Days => new DateRange(today, today.AddDays(3)),
            RangeShortcut.Next7Days => new DateRange(today, today.AddDays(7)),
            RangeShortcut.Next14Days => new DateRange(today, today.AddDays(14)),
            RangeShortcut.Next30Days => new DateRange(today, today.AddDays(30)),
            RangeShortcut.Next90Days => new DateRange(today, today.AddDays(90)),
            RangeShortcut.NextWeek => GetNextWeek(),
            RangeShortcut.NextMonth => GetNextMonth(),
            RangeShortcut.NextQuarter => GetNextQuarter(),
            RangeShortcut.NextYear => GetNextYear(),
            RangeShortcut.PreviousBusinessWeek => GetPreviousBusinessWeek(),
            RangeShortcut.PreviousBusinessMonth => GetPreviousBusinessMonth(),
            RangeShortcut.AllTime => new DateRange(Culture.Calendar.MinSupportedDateTime, Culture.Calendar.MaxSupportedDateTime),
            RangeShortcut.Custom => null,
            _ => null
        };
    }

    // Helper methods for specific date calculations
    private DateRange GetLastWeek()
    {
        var today = DateTime.Today;
        var firstDayOfWeek = GetFirstDayOfWeek();
        var startOfThisWeek = today.StartOfWeek(firstDayOfWeek);
        var startOfLastWeek = startOfThisWeek.AddDays(-7);
        var endOfLastWeek = startOfThisWeek.AddDays(-1);
        return new DateRange(startOfLastWeek, endOfLastWeek);
    }

    private DateRange GetLastMonth()
    {
        var today = DateTime.Today;
        var firstOfThisMonth = today.StartOfMonth(Culture);
        var firstOfLastMonth = Culture.Calendar.AddMonths(firstOfThisMonth, -1);
        var lastOfLastMonth = firstOfThisMonth.AddDays(-1);
        return new DateRange(firstOfLastMonth, lastOfLastMonth);
    }

    private DateRange GetLastQuarter()
    {
        var today = DateTime.Today;
        var currentQuarter = (today.Month - 1) / 3;
        var lastQuarter = currentQuarter == 0 ? 3 : currentQuarter - 1;
        var year = currentQuarter == 0 ? today.Year - 1 : today.Year;
        var startMonth = lastQuarter * 3 + 1;
        var endMonth = startMonth + 2;
        var start = new DateTime(year, startMonth, 1);
        var end = new DateTime(year, endMonth, DateTime.DaysInMonth(year, endMonth));
        return new DateRange(start, end);
    }

    private DateRange GetLastYear()
    {
        var lastYear = DateTime.Today.Year - 1;
        return new DateRange(new DateTime(lastYear, 1, 1), new DateTime(lastYear, 12, 31));
    }

    private DateRange GetQuarterToDate()
    {
        var today = DateTime.Today;
        var currentQuarter = (today.Month - 1) / 3;
        var quarterStartMonth = currentQuarter * 3 + 1;
        var quarterStart = new DateTime(today.Year, quarterStartMonth, 1);
        return new DateRange(quarterStart, today);
    }

    private DateRange GetThisWeek()
    {
        var today = DateTime.Today;
        var firstDayOfWeek = GetFirstDayOfWeek();
        var startOfWeek = today.StartOfWeek(firstDayOfWeek);
        var endOfWeek = startOfWeek.AddDays(6);
        
        // If FutureOnly is set, start from today instead of the beginning of the week
        var start = FutureOnly ? today : startOfWeek;
        
        return new DateRange(start, endOfWeek);
    }

    private DateRange GetThisMonth()
    {
        var today = DateTime.Today;
        var startOfMonth = today.StartOfMonth(Culture);
        var endOfMonth = today.EndOfMonth(Culture);
        
        // If FutureOnly is set, start from today instead of the beginning of the month
        var start = FutureOnly ? today : startOfMonth;
        
        return new DateRange(start, endOfMonth);
    }

    private DateRange GetThisQuarter()
    {
        var today = DateTime.Today;
        var currentQuarter = (today.Month - 1) / 3;
        var startMonth = currentQuarter * 3 + 1;
        var endMonth = startMonth + 2;
        var start = new DateTime(today.Year, startMonth, 1);
        var end = new DateTime(today.Year, endMonth, DateTime.DaysInMonth(today.Year, endMonth));
        return new DateRange(start, end);
    }

    private DateRange GetNextWeek()
    {
        var today = DateTime.Today;
        var firstDayOfWeek = GetFirstDayOfWeek();
        var startOfThisWeek = today.StartOfWeek(firstDayOfWeek);
        var startOfNextWeek = startOfThisWeek.AddDays(7);
        var endOfNextWeek = startOfNextWeek.AddDays(6);
        return new DateRange(startOfNextWeek, endOfNextWeek);
    }

    private DateRange GetNextMonth()
    {
        var today = DateTime.Today;
        var firstOfThisMonth = today.StartOfMonth(Culture);
        var firstOfNextMonth = Culture.Calendar.AddMonths(firstOfThisMonth, 1);
        var lastOfNextMonth = Culture.Calendar.AddMonths(firstOfNextMonth.EndOfMonth(Culture), 0);
        return new DateRange(firstOfNextMonth, lastOfNextMonth);
    }

    private DateRange GetNextQuarter()
    {
        var today = DateTime.Today;
        var currentQuarter = (today.Month - 1) / 3;
        var nextQuarter = currentQuarter == 3 ? 0 : currentQuarter + 1;
        var year = currentQuarter == 3 ? today.Year + 1 : today.Year;
        var startMonth = nextQuarter * 3 + 1;
        var endMonth = startMonth + 2;
        var start = new DateTime(year, startMonth, 1);
        var end = new DateTime(year, endMonth, DateTime.DaysInMonth(year, endMonth));
        return new DateRange(start, end);
    }

    private DateRange GetNextYear()
    {
        var nextYear = DateTime.Today.Year + 1;
        return new DateRange(new DateTime(nextYear, 1, 1), new DateTime(nextYear, 12, 31));
    }

    private DateRange GetPreviousBusinessWeek()
    {
        var today = DateTime.Today;
        var firstDayOfWeek = GetFirstDayOfWeek();
        var startOfThisWeek = today.StartOfWeek(firstDayOfWeek);
        var startOfLastWeek = startOfThisWeek.AddDays(-7);
        
        // Find first Monday of last week
        while (startOfLastWeek.DayOfWeek != DayOfWeek.Monday)
        {
            startOfLastWeek = startOfLastWeek.AddDays(1);
        }
        
        var endOfLastWeek = startOfLastWeek.AddDays(4); // Friday
        return new DateRange(startOfLastWeek, endOfLastWeek);
    }

    private DateRange GetPreviousBusinessMonth()
    {
        var today = DateTime.Today;
        var firstOfThisMonth = today.StartOfMonth(Culture);
        var firstOfLastMonth = Culture.Calendar.AddMonths(firstOfThisMonth, -1);
        var lastOfLastMonth = firstOfThisMonth.AddDays(-1);
        
        // For simplicity, return the full month. A more sophisticated implementation
        // could exclude weekends, but that would require more complex logic.
        return new DateRange(firstOfLastMonth, lastOfLastMonth);
    }

    /// <summary>
    /// Shows the range picker.
    /// </summary>
    public async Task ShowAsync()
    {
        if (!Disabled && !ReadOnly)
        {
            _showPicker = true;
            ResetPreview();
            await OnOpen.InvokeAsync();
            StateHasChanged();
        }
    }

    /// <summary>
    /// Hides the range picker.
    /// </summary>
    public async Task HideAsync()
    {
        _showPicker = false;
        await OnClose.InvokeAsync();
        StateHasChanged();
    }

    /// <summary>
    /// Applies the current preview range as the selected range.
    /// </summary>
    public async Task ApplyAsync()
    {
        if (_hasValidRange && _previewRange != null)
        {
            await SetDateRangeAsync(_previewRange);
            
            // Add to recent ranges if it's a custom selection
            if (RememberRecentRanges && _selectedPreset == RangeShortcut.Custom)
            {
                AddToRecentRanges(_previewRange);
            }
            
            await OnApply.InvokeAsync(_previewRange);
            await HideAsync();
        }
    }

    /// <summary>
    /// Cancels the current selection and closes the picker.
    /// </summary>
    public async Task CancelAsync()
    {
        ResetPreview();
        await OnCancel.InvokeAsync();
        await HideAsync();
    }

    /// <summary>
    /// Clears the current selection.
    /// </summary>
    public async Task ClearAsync()
    {
        await SetDateRangeAsync(null);
        ResetPreview();
        await OnClear.InvokeAsync();
        if (_showPicker)
        {
            await HideAsync();
        }
    }

    /// <summary>
    /// Selects a preset range.
    /// </summary>
    public async Task SelectPresetAsync(RangeShortcut preset)
    {
        _selectedPreset = preset;
        
        if (preset == RangeShortcut.Custom)
        {
            _previewRange = null;
            _hasValidRange = false;
            ResetSelectionState();
        }
        else
        {
            var range = GetRangeForShortcut(preset);
            if (range != null && IsValidRange(range))
            {
                _previewRange = range;
                _hasValidRange = true;
                UpdateCalendarFromPreview();
            }
        }
        
        await OnPreviewChange.InvokeAsync(_previewRange);
        StateHasChanged();
    }

    /// <summary>
    /// Sets the confirmed date range.
    /// </summary>
    protected async Task SetDateRangeAsync(DateRange? range)
    {
        if (_dateRange != range)
        {
            if (range?.Start is not null)
                PickerMonth = new DateTime(Culture.Calendar.GetYear(range.Start.Value), Culture.Calendar.GetMonth(range.Start.Value), 1, Culture.Calendar);

            _dateRange = range;
            HighlightedDate = range?.Start;

            await DateRangeChanged.InvokeAsync(_dateRange);
            StateHasChanged();
        }
    }

    /// <summary>
    /// Resets the preview state.
    /// </summary>
    private void ResetPreview()
    {
        _previewRange = _dateRange;
        _hasValidRange = _dateRange != null && IsValidRange(_dateRange);
        _selectedPreset = GetPresetForRange(_dateRange);
        ResetSelectionState();
    }

    /// <summary>
    /// Resets the calendar selection state for fresh range selection.
    /// </summary>
    public void ResetSelectionState()
    {
        _firstDate = null;
        _secondDate = null;
        _isInSelectionMode = true;
        StateHasChanged();
    }

    /// <summary>
    /// Updates the calendar display based on the current preview range.
    /// </summary>
    private void UpdateCalendarFromPreview()
    {
        if (_previewRange?.Start != null)
        {
            _firstDate = _previewRange.Start;
            _secondDate = _previewRange.End;
            _isInSelectionMode = false;
        }
    }

    /// <summary>
    /// Validates if a range meets all constraints.
    /// </summary>
    private bool IsValidRange(DateRange? range)
    {
        if (range?.Start == null || range?.End == null)
            return false;

        // Normalize dates to date-only for comparison
        var startDate = range.Start.Value.Date;
        var endDate = range.End.Value.Date;
        var minDateOnly = MinDate?.Date;
        var maxDateOnly = MaxDate?.Date;

        // Check min/max date constraints
        if (minDateOnly.HasValue && startDate < minDateOnly.Value)
            return false;
        if (maxDateOnly.HasValue && endDate > maxDateOnly.Value)
            return false;

        // Check past/future only constraints
        var today = DateTime.Today;
        if (PastOnly && (startDate > today || endDate > today))
            return false;
        // For FutureOnly: all dates must be today or in the future
        if (FutureOnly && (startDate < today || endDate < today))
            return false;

        // Check min/max days constraints
        var dayDiff = (endDate - startDate).Days + 1;
        if (MinDays.HasValue && dayDiff < MinDays.Value)
            return false;
        if (MaxDays.HasValue && dayDiff > MaxDays.Value)
            return false;

        return true;
    }

    /// <summary>
    /// Gets the preset that matches a given range, if any.
    /// </summary>
    private RangeShortcut? GetPresetForRange(DateRange? range)
    {
        if (range == null) return null;

        foreach (var preset in GetActivePresets())
        {
            if (preset == RangeShortcut.Custom) continue;
            var presetRange = GetRangeForShortcut(preset);
            if (presetRange?.Start == range.Start && presetRange?.End == range.End)
            {
                return preset;
            }
        }

        return RangeShortcut.Custom;
    }

    /// <summary>
    /// Adds a range to the recent ranges list.
    /// </summary>
    private void AddToRecentRanges(DateRange range)
    {
        _recentRanges.RemoveAll(r => r.Start == range.Start && r.End == range.End);
        _recentRanges.Insert(0, range);
        if (_recentRanges.Count > 3)
        {
            _recentRanges.RemoveAt(3);
        }
    }

    /// <summary>
    /// Gets the current validation error message, if any.
    /// </summary>
    public string? GetValidationMessage()
    {
        if (_previewRange?.Start == null || _previewRange?.End == null)
            return null;

        // Normalize dates to date-only for comparison
        var startDate = _previewRange.Start.Value.Date;
        var endDate = _previewRange.End.Value.Date;
        var minDateOnly = MinDate?.Date;
        var maxDateOnly = MaxDate?.Date;
        var today = DateTime.Today;
        
        if (minDateOnly.HasValue && startDate < minDateOnly.Value)
            return string.Format(Localizer.StartDateCannotBeEarlierThan, MinDate.Value.ToString("d", Culture));
        if (maxDateOnly.HasValue && endDate > maxDateOnly.Value)
            return string.Format(Localizer.EndDateCannotBeLaterThan, MaxDate.Value.ToString("d", Culture));
            
        if (PastOnly && (startDate > today || endDate > today))
            return Localizer.SelectedDatesMustBeInThePast;
        // For FutureOnly: all dates must be today or in the future
        if (FutureOnly && (startDate < today || endDate < today))
            return Localizer.SelectedDatesMustBeInTheFuture;

        var dayDiff = (endDate - startDate).Days + 1;
        if (MinDays.HasValue && dayDiff < MinDays.Value)
            return string.Format(Localizer.RangeMustBeAtLeastDays, MinDays.Value);
        if (MaxDays.HasValue && dayDiff > MaxDays.Value)
            return string.Format(Localizer.RangeCannotExceedDays, MaxDays.Value);

        return null;
    }

    // Override abstract methods from base class
    protected override string GetDayClasses(int month, DateTime day)
    {
        var builder = new CssBuilder("scb-day");
        builder.AddClass(AdditionalDateClassesFunc?.Invoke(day) ?? string.Empty);
        
        if (day < GetMonthStart(month) || day > GetMonthEnd(month))
        {
            return builder.AddClass("scb-hidden").Build();
        }

        var today = DateTime.Today;
        var isInActiveSelection = _firstDate != null || _secondDate != null;
        
        // Handle active selection state (when user is currently selecting)
        if (isInActiveSelection)
        {
            if (_firstDate?.Date < day && _secondDate?.Date > day)
            {
                return builder
                    .AddClass("scb-range")
                    .AddClass("scb-range-between")
                    .AddClass($"scb-current scb-{Color.ToDescriptionString()}-text scb-button-outlined scb-button-outlined-{Color.ToDescriptionString()}", day == today)
                    .Build();
            }

            if (_firstDate?.Date == day && _secondDate?.Date == day)
            {
                return builder.AddClass("scb-selected")
                    .AddClass($"scb-theme-{Color.ToDescriptionString()}")
                    .Build();
            }

            if (_firstDate?.Date == day)
            {
                return builder.AddClass("scb-selected")
                    .AddClass("scb-range")
                    .AddClass("scb-range-start-selected")
                    .AddClass("scb-range-selection")
                    .AddClass($"scb-theme-{Color.ToDescriptionString()}")
                    .Build();
            }

            if (_secondDate?.Date == day)
            {
                return builder.AddClass("scb-selected")
                    .AddClass("scb-range")
                    .AddClass("scb-range-end-selected")
                    .AddClass($"scb-theme-{Color.ToDescriptionString()}")
                    .Build();
            }

            // Show preview range for hover effect
            if (_firstDate?.Date < day && _secondDate is null)
            {
                return builder.AddClass("scb-range", day != today)
                    .AddClass("scb-range-selection")
                    .AddClass($"scb-range-selection-{Color.ToDescriptionString()}")
                    .AddClass($"scb-current scb-{Color.ToDescriptionString()}-text scb-button-outlined scb-button-outlined-{Color.ToDescriptionString()}", day == today)
                    .Build();
            }
        }
        // Handle preview range display (from preset or confirmed range)
        else if (_previewRange is { Start: not null, End: not null })
        {
            if (_previewRange.Start.Value.Date < day && _previewRange.End.Value.Date > day)
            {
                return builder
                    .AddClass("scb-range")
                    .AddClass("scb-range-between")
                    .AddClass($"scb-current scb-{Color.ToDescriptionString()}-text scb-button-outlined scb-button-outlined-{Color.ToDescriptionString()}", day == today)
                    .Build();
            }

            if (_previewRange.Start.Value.Date == day)
            {
                return builder.AddClass("scb-selected")
                    .AddClass("scb-range")
                    .AddClass("scb-range-start-selected")
                    .AddClass($"scb-theme-{Color.ToDescriptionString()}")
                    .Build();
            }

            if (_previewRange.End.Value.Date == day)
            {
                return builder.AddClass("scb-selected")
                    .AddClass("scb-range")
                    .AddClass("scb-range-end-selected")
                    .AddClass($"scb-theme-{Color.ToDescriptionString()}")
                    .Build();
            }

            if (_previewRange.Start.Value.Date == day && _previewRange.End.Value.Date == day)
            {
                return builder.AddClass("scb-selected").AddClass($"scb-theme-{Color.ToDescriptionString()}").Build();
            }
        }

        // Default today styling
        if (day == today)
        {
            return builder.AddClass("scb-current")
                .AddClass($"scb-button-outlined scb-button-outlined-{Color.ToDescriptionString()}")
                .AddClass($"scb-{Color.ToDescriptionString()}-text")
                .Build();
        }

        return builder.Build();
    }

    protected override async Task OnDayClickedAsync(DateTime dateTime)
    {
        // First click or reset after both dates were selected
        if (_firstDate == null || _secondDate != null)
        {
            _secondDate = null;
            _firstDate = dateTime;
            _selectedPreset = RangeShortcut.Custom;
            StateHasChanged();
            return;
        }
        
        // Second click - we have _firstDate and _secondDate is null
        if (_firstDate > dateTime)
        {
            // User selected end date before start date, swap them
            _secondDate = _firstDate;
            _firstDate = dateTime;
        }
        else
        {
            _secondDate = dateTime;
        }

        // Both dates selected, create the preview range
        var newRange = new DateRange(_firstDate, _secondDate);
        _previewRange = newRange;
        _hasValidRange = IsValidRange(newRange);
        
        await OnPreviewChange.InvokeAsync(_previewRange);
        StateHasChanged();
    }

    protected override string GetTitleDateString()
    {
        if (_firstDate != null)
            return $"{FormatTitleDate(_firstDate)} - {FormatTitleDate(_secondDate)}";

        return _previewRange?.Start != null
            ? $"{FormatTitleDate(_previewRange.Start)} - {FormatTitleDate(_previewRange.End)}"
            : "";
    }

    private string FormatTitleDate(DateTime? date)
    {
        return date?.ToString(TitleDateFormat, Culture) ?? "";
    }

    protected override DateTime GetCalendarStartOfMonth()
    {
        var today = DateTime.Today;
        var date = StartMonth ?? _previewRange?.Start ?? _dateRange?.Start ?? today;
        return date.StartOfMonth(Culture);
    }

    protected override int GetCalendarYear(DateTime yearDate)
    {
        var today = DateTime.Today;
        var date = _previewRange?.Start ?? _dateRange?.Start ?? today;
        var diff = Culture.Calendar.GetYear(date) - Culture.Calendar.GetYear(yearDate);
        var calendarYear = Culture.Calendar.GetYear(date);
        return calendarYear - diff;
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        PickerMonth ??= DateTime.Today.StartOfMonth(Culture);
        
        // Set default button texts from localizer if they haven't been customized
        if (ApplyButtonText == "Apply")
            ApplyButtonText = Localizer.Apply;
        if (CancelButtonText == "Cancel")
            CancelButtonText = Localizer.Cancel;
        if (ClearButtonText == "Clear")
            ClearButtonText = Localizer.Clear;
            
        ResetPreview();
    }
}
