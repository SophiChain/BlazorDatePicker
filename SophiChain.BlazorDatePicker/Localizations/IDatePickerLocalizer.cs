namespace SophiChain.BlazorDatePicker.Localizations;

/// <summary>
/// Service for localizing DatePicker text strings
/// </summary>
public interface IDatePickerLocalizer
{
    // Common
    string Close { get; }
    string Clear { get; }
    string Cancel { get; }
    string Apply { get; }
    string Selected { get; }
    string PreviousMonth { get; }
    string NextMonth { get; }
    
    // Date Picker
    string SelectDate { get; }
    string Years { get; }
    string Months { get; }
    string QuickSelect { get; }
    string Today { get; }
    string Yesterday { get; }
    string Tomorrow { get; }
    string SelectedDate { get; }
    string SelectDateFromCalendar { get; }
    
    // Range Picker
    string SelectDateRange { get; }
    string SelectDateRangePlaceholder { get; }
    string Recent { get; }
    string StartDate { get; }
    string EndDate { get; }
    string Days { get; }
    string SelectEndDateToComplete { get; }
    string SelectStartAndEndDates { get; }
    
    // Range Shortcuts
    string Last7Days { get; }
    string Last14Days { get; }
    string Last30Days { get; }
    string Last60Days { get; }
    string Last90Days { get; }
    string LastWeek { get; }
    string LastMonth { get; }
    string LastQuarter { get; }
    string LastYear { get; }
    string WeekToDate { get; }
    string MonthToDate { get; }
    string QuarterToDate { get; }
    string YearToDate { get; }
    string Rolling7Days { get; }
    string Rolling30Days { get; }
    string Rolling90Days { get; }
    string ThisWeek { get; }
    string ThisMonth { get; }
    string ThisQuarter { get; }
    string ThisYear { get; }
    string Next7Days { get; }
    string Next14Days { get; }
    string Next30Days { get; }
    string Next90Days { get; }
    string NextWeek { get; }
    string NextMonthRange { get; }
    string NextQuarter { get; }
    string NextYear { get; }
    string Custom { get; }
    
    // Method to get localized text for range shortcuts
    string GetRangeShortcutText(RangeShortcut shortcut);
}
