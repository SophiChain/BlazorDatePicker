using System.ComponentModel;

namespace SophiChain.BlazorDatePicker;

/// <summary>
/// Predefined date range shortcuts for quick selection.
/// </summary>
public enum RangeShortcut
{
    /// <summary>
    /// Today only.
    /// </summary>
    [Description("Today")]
    Today,

    /// <summary>
    /// Yesterday only.
    /// </summary>
    [Description("Yesterday")]
    Yesterday,

    /// <summary>
    /// Last 7 days (including today).
    /// </summary>
    [Description("Last 7 days")]
    Last7Days,

    /// <summary>
    /// Last 14 days (including today).
    /// </summary>
    [Description("Last 14 days")]
    Last14Days,

    /// <summary>
    /// Last 30 days (including today).
    /// </summary>
    [Description("Last 30 days")]
    Last30Days,

    /// <summary>
    /// Last 60 days (including today).
    /// </summary>
    [Description("Last 60 days")]
    Last60Days,

    /// <summary>
    /// Last 90 days (including today).
    /// </summary>
    [Description("Last 90 days")]
    Last90Days,

    /// <summary>
    /// Last week (Monday to Sunday, or based on culture).
    /// </summary>
    [Description("Last week")]
    LastWeek,

    /// <summary>
    /// Last month (1st to last day of previous month).
    /// </summary>
    [Description("Last month")]
    LastMonth,

    /// <summary>
    /// Last quarter (previous 3-month period).
    /// </summary>
    [Description("Last quarter")]
    LastQuarter,

    /// <summary>
    /// Last year (January 1st to December 31st of previous year).
    /// </summary>
    [Description("Last year")]
    LastYear,

    /// <summary>
    /// Week to date (start of current week to today).
    /// </summary>
    [Description("Week to date")]
    WeekToDate,

    /// <summary>
    /// Month to date (1st of current month to today).
    /// </summary>
    [Description("Month to date")]
    MonthToDate,

    /// <summary>
    /// Quarter to date (start of current quarter to today).
    /// </summary>
    [Description("Quarter to date")]
    QuarterToDate,

    /// <summary>
    /// Year to date (January 1st of current year to today).
    /// </summary>
    [Description("Year to date")]
    YearToDate,

    /// <summary>
    /// Rolling 7 days (7 days ago to today).
    /// </summary>
    [Description("Rolling 7 days")]
    Rolling7Days,

    /// <summary>
    /// Rolling 30 days (30 days ago to today).
    /// </summary>
    [Description("Rolling 30 days")]
    Rolling30Days,

    /// <summary>
    /// Rolling 90 days (90 days ago to today).
    /// </summary>
    [Description("Rolling 90 days")]
    Rolling90Days,

    /// <summary>
    /// Current week (start of week to end of week).
    /// </summary>
    [Description("This week")]
    ThisWeek,

    /// <summary>
    /// Current month (1st to last day of current month).
    /// </summary>
    [Description("This month")]
    ThisMonth,

    /// <summary>
    /// Current quarter (start to end of current quarter).
    /// </summary>
    [Description("This quarter")]
    ThisQuarter,

    /// <summary>
    /// Current year (January 1st to December 31st of current year).
    /// </summary>
    [Description("This year")]
    ThisYear,

    /// <summary>
    /// Next 7 days (today to 7 days from today).
    /// </summary>
    [Description("Next 7 days")]
    Next7Days,

    /// <summary>
    /// Next 14 days (today to 14 days from today).
    /// </summary>
    [Description("Next 14 days")]
    Next14Days,

    /// <summary>
    /// Next 30 days (today to 30 days from today).
    /// </summary>
    [Description("Next 30 days")]
    Next30Days,

    /// <summary>
    /// Next 90 days (today to 90 days from today).
    /// </summary>
    [Description("Next 90 days")]
    Next90Days,

    /// <summary>
    /// Next week (Monday to Sunday of next week, or based on culture).
    /// </summary>
    [Description("Next week")]
    NextWeek,

    /// <summary>
    /// Next month (1st to last day of next month).
    /// </summary>
    [Description("Next month")]
    NextMonth,

    /// <summary>
    /// Next quarter (next 3-month period).
    /// </summary>
    [Description("Next quarter")]
    NextQuarter,

    /// <summary>
    /// Next year (January 1st to December 31st of next year).
    /// </summary>
    [Description("Next year")]
    NextYear,

    /// <summary>
    /// Previous business week (Monday to Friday of last week).
    /// </summary>
    [Description("Previous business week")]
    PreviousBusinessWeek,

    /// <summary>
    /// Previous business month (excluding weekends).
    /// </summary>
    [Description("Previous business month")]
    PreviousBusinessMonth,

    /// <summary>
    /// All time (no date restrictions).
    /// </summary>
    [Description("All time")]
    AllTime,

    /// <summary>
    /// Custom range - allows manual selection.
    /// </summary>
    [Description("Custom")]
    Custom
}
