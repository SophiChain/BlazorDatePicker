using System.Globalization;
using Microsoft.AspNetCore.Components;
using SophiChain.BlazorDatePicker.Base;
using SophiChain.BlazorDatePicker.Extensions;
using SophiChain.BlazorDatePicker.Localizations;
using SophiChain.BlazorDatePicker.Services;
using SophiChain.BlazorDatePicker.Utilities;
using SophiChain.BlazorDatePicker.Utilities.Converters;

namespace SophiChain.BlazorDatePicker.Components;

/// <summary>
/// Represents a base class for designing date picker components.
/// </summary>
public abstract class SCBBaseDatePicker : SCBPicker<DateTime?>
{
    protected SCBBaseDatePicker() : base(new DefaultConverter<DateTime?>
    {
        Format = CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern,
        Culture = CultureInfo.CurrentUICulture
    })
    {
    }

    /// <summary>
    /// Injected scroll manager service for handling scroll operations.
    /// </summary>
    [Inject]
    protected IScrollManager ScrollManager { get; set; } = null!;

    /// <summary>
    /// Injected JavaScript API service for DOM operations.
    /// </summary>
    [Inject]
    protected IJsApiService JsApiService { get; set; } = null!;

    /// <summary>
    /// Injected date/time provider service for getting current date/time.
    /// </summary>
    [Inject]
    protected IDateTimeProvider DateTimeProvider { get; set; } = null!;

    /// <summary>
    /// Injected localization factory service for creating culture-specific localizers.
    /// </summary>
    [Inject]
    protected IDatePickerLocalizerFactory LocalizerFactory { get; set; } = null!;
    
    /// <summary>
    /// Gets the culture-specific localizer for this component.
    /// </summary>
    protected IDatePickerLocalizer Localizer => LocalizerFactory.GetLocalizer(Culture);

    /// <summary>
    /// The maximum selectable date.
    /// </summary>
    [Parameter]
    public DateTime? MaxDate { get; set; }

    /// <summary>
    /// The minimum selectable date.
    /// </summary>
    [Parameter]
    public DateTime? MinDate { get; set; }

    /// <summary>
    /// The initial view to display.
    /// </summary>
    [Parameter]
    public OpenTo OpenTo { get; set; } = OpenTo.Date;

    /// <summary>
    /// The format for selected dates.
    /// </summary>
    [Parameter]
    public string? DateFormat
    {
        get => ((DefaultConverter<DateTime?>)Converter).Format;
        set
        {
            if (Converter is DefaultConverter<DateTime?> defaultConverter)
            {
                defaultConverter.Format = value;
            }
        }
    }

    /// <summary>
    /// The day representing the first day of the week.
    /// </summary>
    [Parameter]
    public DayOfWeek? FirstDayOfWeek { get; set; }

    /// <summary>
    /// The current month shown in the date picker.
    /// </summary>
    [Parameter]
    public DateTime? PickerMonth { get; set; }

    /// <summary>
    /// Occurs when PickerMonth has changed.
    /// </summary>
    [Parameter]
    public EventCallback<DateTime?> PickerMonthChanged { get; set; }

    /// <summary>
    /// The number of months to display in the calendar.
    /// </summary>
    [Parameter]
    public int DisplayMonths { get; set; } = 1;

    /// <summary>
    /// The maximum number of months allowed in one row.
    /// </summary>
    [Parameter]
    public int? MaxMonthColumns { get; set; }

    /// <summary>
    /// Shows week numbers at the start of each week.
    /// </summary>
    [Parameter]
    public bool ShowWeekNumbers { get; set; }

    /// <summary>
    /// The format of the selected date in the title.
    /// </summary>
    [Parameter]
    public string TitleDateFormat { get; set; } = "ddd, dd MMM";

    /// <summary>
    /// The function used to disable one or more dates.
    /// </summary>
    [Parameter]
    public Func<DateTime, bool>? IsDateDisabledFunc { get; set; }

    /// <summary>
    /// The function which returns CSS classes for a date.
    /// </summary>
    [Parameter]
    public Func<DateTime, string>? AdditionalDateClassesFunc { get; set; }

    /// <summary>
    /// The color theme for the picker.
    /// </summary>
    [Parameter]
    public Color Color { get; set; } = Color.Primary;

    /// <summary>
    /// The display variant for the input.
    /// </summary>
    [Parameter]
    public Variant Variant { get; set; } = Variant.Filled;

    /// <summary>
    /// The picker display variant.
    /// </summary>
    [Parameter]
    public PickerVariant PickerVariant { get; set; } = PickerVariant.Inline;

    /// <summary>
    /// Closes this picker when a value is selected.
    /// </summary>
    [Parameter]
    public bool AutoClose { get; set; } = false;

    /// <summary>
    /// The delay, in milliseconds, before closing the picker after a value is selected.
    /// </summary>
    [Parameter]
    public int ClosingDelay { get; set; } = 100;

    /// <summary>
    /// Whether to show the toolbar.
    /// </summary>
    [Parameter]
    public bool ShowToolbar { get; set; } = false;

    /// <summary>
    /// The start month when opening the picker.
    /// </summary>
    [Parameter]
    public DateTime? StartMonth { get; set; }

    /// <summary>
    /// The year to use, which cannot be changed.
    /// </summary>
    [Parameter]
    public int? FixYear { get; set; }

    /// <summary>
    /// The month to use, which cannot be changed.
    /// </summary>
    [Parameter]
    public int? FixMonth { get; set; }

    /// <summary>
    /// The day to use, which cannot be changed.
    /// </summary>
    [Parameter]
    public int? FixDay { get; set; }

    /// <summary>
    /// Represents the currently selected date (highlighted in UI).
    /// </summary>
    protected DateTime? HighlightedDate { get; set; }

    protected virtual bool IsRange => false;
    protected OpenTo CurrentView;

    protected DayOfWeek GetFirstDayOfWeek()
    {
        return FirstDayOfWeek ?? Culture.DateTimeFormat.FirstDayOfWeek;
    }

    protected virtual bool IsDayDisabled(DateTime date)
    {
        var dateOnly = date.Date;
        var minDateOnly = MinDate?.Date;
        var maxDateOnly = MaxDate?.Date;
        
        return (minDateOnly.HasValue && dateOnly < minDateOnly.Value) ||
               (maxDateOnly.HasValue && dateOnly > maxDateOnly.Value) ||
               (IsDateDisabledFunc?.Invoke(date) ?? false);
    }

    /// <summary>
    /// Get the first of the month to display
    /// </summary>
    protected DateTime GetMonthStart(int month)
    {
        var today = DateTime.Today;
        var monthStartDate = PickerMonth ?? today.StartOfMonth(Culture);
        var correctYear = FixYear ?? monthStartDate.Year;
        var correctMonth = FixMonth ?? monthStartDate.Month;
        monthStartDate = new DateTime(correctYear, correctMonth, monthStartDate.Day, 0, 0, 0, DateTimeKind.Utc);

        if (PickerMonth is { Year: 1, Month: 1 })
        {
            return Culture.Calendar.MinSupportedDateTime;
        }

        if (PickerMonth.HasValue && PickerMonth.Value.Year == 9999 && PickerMonth.Value.Month == 12 && month >= 1)
        {
            return Culture.Calendar.MaxSupportedDateTime;
        }
        return Culture.Calendar.AddMonths(monthStartDate, month);
    }

    /// <summary>
    /// Get the last of the month to display
    /// </summary>
    protected DateTime GetMonthEnd(int month)
    {
        var today = DateTime.Today;
        var monthStartDate = PickerMonth ?? today.StartOfMonth(Culture);
        return Culture.Calendar.AddMonths(monthStartDate, month).EndOfMonth(Culture);
    }

    /// <summary>
    /// Gets the n-th week of the currently displayed month. 
    /// </summary>
    protected IEnumerable<DateTime> GetWeek(int month, int index)
    {
        if (index is < 0 or > 5)
            throw new ArgumentException("Index must be between 0 and 5");
            
        var month_first = GetMonthStart(month);
        if ((Culture.Calendar.MaxSupportedDateTime - month_first).Days >= index * 7)
        {
            var week_first = month_first.AddDays(index * 7).StartOfWeek(GetFirstDayOfWeek());
            for (var i = 0; i < 7; i++)
            {
                if ((Culture.Calendar.MaxSupportedDateTime - week_first).Days >= i)
                    yield return week_first.AddDays(i);
                else
                    yield return Culture.Calendar.MaxSupportedDateTime;
            }
        }
    }

    protected abstract string GetDayClasses(int month, DateTime day);
    protected abstract Task OnDayClickedAsync(DateTime dateTime);
    protected abstract string GetTitleDateString();
    protected abstract DateTime GetCalendarStartOfMonth();
    protected abstract int GetCalendarYear(DateTime yearDate);

    /// <summary>
    /// Gets the CSS class for the color theme.
    /// </summary>
    /// <returns>The CSS class name for the current color theme.</returns>
    protected string GetColorClass()
    {
        return $"scb-theme-{Color.ToDescriptionString()}";
    }

    /// <summary>
    /// Gets the CSS class for selected elements based on the color theme.
    /// </summary>
    /// <returns>The CSS class name for selected elements.</returns>
    protected string GetSelectedColorClass()
    {
        return $"scb-selected-{Color.ToDescriptionString()}";
    }

    /// <summary>
    /// Gets the CSS class for range selection based on the color theme.
    /// </summary>
    /// <returns>The CSS class name for range selection elements.</returns>
    protected string GetRangeSelectionClass()
    {
        return $"scb-range-selection-{Color.ToDescriptionString()}";
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        CurrentView = OpenTo;
        
        if (HighlightedDate is not null) return;

        var today = DateTime.Today;
        var year = FixYear ?? today.Year;
        var month = FixMonth ?? (year == today.Year ? today.Month : 1);
        var day = FixDay ?? 1;

        if (DateTime.TryParseExact($"{year}-{month}-{day}", "yyyy-M-d", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
        {
            HighlightedDate = date;
        }
    }
}
