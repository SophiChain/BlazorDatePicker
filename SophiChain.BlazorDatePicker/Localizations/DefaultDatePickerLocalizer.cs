namespace SophiChain.BlazorDatePicker.Localizations;

/// <summary>
/// Default English localization for DatePicker components
/// </summary>
public class DefaultDatePickerLocalizer : IDatePickerLocalizer
{
    // Common
    public virtual string Close => "Close";
    public virtual string Clear => "Clear";
    public virtual string Cancel => "Cancel";
    public virtual string Apply => "Apply";
    public virtual string Selected => "Selected:";
    public virtual string PreviousMonth => "Previous month";
    public virtual string NextMonth => "Next month";
    
    // Date Picker
    public virtual string SelectDate => "Select Date";
    public virtual string Years => "Years";
    public virtual string Months => "Months";
    public virtual string QuickSelect => "Quick Select";
    public virtual string Today => "Today";
    public virtual string Yesterday => "Yesterday";
    public virtual string Tomorrow => "Tomorrow";
    public virtual string SelectedDate => "Selected Date";
    public virtual string SelectDateFromCalendar => "Select a date from the calendar or shortcuts";
    
    // Range Picker
    public virtual string SelectDateRange => "Select Date Range";
    public virtual string SelectDateRangePlaceholder => "Select date range";
    public virtual string Recent => "Recent";
    public virtual string StartDate => "Start Date";
    public virtual string EndDate => "End Date";
    public virtual string Days => "days";
    public virtual string SelectEndDateToComplete => "Select end date to complete range";
    public virtual string SelectStartAndEndDates => "Select start and end dates";
    
    // Range Shortcuts
    public virtual string Last7Days => "Last 7 days";
    public virtual string Last14Days => "Last 14 days";
    public virtual string Last30Days => "Last 30 days";
    public virtual string Last60Days => "Last 60 days";
    public virtual string Last90Days => "Last 90 days";
    public virtual string LastWeek => "Last week";
    public virtual string LastMonth => "Last month";
    public virtual string LastQuarter => "Last quarter";
    public virtual string LastYear => "Last year";
    public virtual string WeekToDate => "Week to date";
    public virtual string MonthToDate => "Month to date";
    public virtual string QuarterToDate => "Quarter to date";
    public virtual string YearToDate => "Year to date";
    public virtual string Rolling7Days => "Rolling 7 days";
    public virtual string Rolling30Days => "Rolling 30 days";
    public virtual string Rolling90Days => "Rolling 90 days";
    public virtual string ThisWeek => "This week";
    public virtual string ThisMonth => "This month";
    public virtual string ThisQuarter => "This quarter";
    public virtual string ThisYear => "This year";
    public virtual string Next7Days => "Next 7 days";
    public virtual string Next14Days => "Next 14 days";
    public virtual string Next30Days => "Next 30 days";
    public virtual string Next90Days => "Next 90 days";
    public virtual string NextWeek => "Next week";
    public virtual string NextMonthRange => "Next month";
    public virtual string NextQuarter => "Next quarter";
    public virtual string NextYear => "Next year";
    public virtual string Custom => "Custom";
    
    /// <summary>
    /// Gets the localized text for a range shortcut.
    /// </summary>
    public virtual string GetRangeShortcutText(RangeShortcut shortcut)
    {
        return shortcut switch
        {
            RangeShortcut.Today => Today,
            RangeShortcut.Yesterday => Yesterday,
            RangeShortcut.Last7Days => Last7Days,
            RangeShortcut.Last14Days => Last14Days,
            RangeShortcut.Last30Days => Last30Days,
            RangeShortcut.Last60Days => Last60Days,
            RangeShortcut.Last90Days => Last90Days,
            RangeShortcut.LastWeek => LastWeek,
            RangeShortcut.LastMonth => LastMonth,
            RangeShortcut.LastQuarter => LastQuarter,
            RangeShortcut.LastYear => LastYear,
            RangeShortcut.WeekToDate => WeekToDate,
            RangeShortcut.MonthToDate => MonthToDate,
            RangeShortcut.QuarterToDate => QuarterToDate,
            RangeShortcut.YearToDate => YearToDate,
            RangeShortcut.Rolling7Days => Rolling7Days,
            RangeShortcut.Rolling30Days => Rolling30Days,
            RangeShortcut.Rolling90Days => Rolling90Days,
            RangeShortcut.ThisWeek => ThisWeek,
            RangeShortcut.ThisMonth => ThisMonth,
            RangeShortcut.ThisQuarter => ThisQuarter,
            RangeShortcut.ThisYear => ThisYear,
            RangeShortcut.Next7Days => Next7Days,
            RangeShortcut.Next14Days => Next14Days,
            RangeShortcut.Next30Days => Next30Days,
            RangeShortcut.Next90Days => Next90Days,
            RangeShortcut.NextWeek => NextWeek,
            RangeShortcut.NextMonth => NextMonthRange,
            RangeShortcut.NextQuarter => NextQuarter,
            RangeShortcut.NextYear => NextYear,
            RangeShortcut.Custom => Custom,
            _ => shortcut.ToString()
        };
    }
}
