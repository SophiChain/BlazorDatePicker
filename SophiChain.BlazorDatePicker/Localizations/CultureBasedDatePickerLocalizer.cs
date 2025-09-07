using System.Globalization;
using SophiChain.BlazorDatePicker.Services;

namespace SophiChain.BlazorDatePicker.Localizations;

/// <summary>
/// Culture-based localizer that automatically selects the appropriate localizer based on the current culture
/// </summary>
public class CultureBasedDatePickerLocalizer : IDatePickerLocalizer
{
    private readonly IDatePickerLocalizer _currentLocalizer;

    public CultureBasedDatePickerLocalizer()
    {
        var cultureName = CultureInfo.CurrentUICulture.Name.ToLowerInvariant();
        
        _currentLocalizer = cultureName switch
        {
            var c when c.StartsWith("ar") => new ArabicDatePickerLocalizer(),
            var c when c.StartsWith("de") => new GermanDatePickerLocalizer(),
            var c when c.StartsWith("es") => new SpanishDatePickerLocalizer(),
            var c when c.StartsWith("fr") => new FrenchDatePickerLocalizer(), 
            var c when c.StartsWith("fa") => new PersianDatePickerLocalizer(),
            _ => new DefaultDatePickerLocalizer()
        };
    }

    // Common
    public string Close => _currentLocalizer.Close;
    public string Clear => _currentLocalizer.Clear;
    public string Cancel => _currentLocalizer.Cancel;
    public string Apply => _currentLocalizer.Apply;
    public string Selected => _currentLocalizer.Selected;
    public string PreviousMonth => _currentLocalizer.PreviousMonth;
    public string NextMonth => _currentLocalizer.NextMonth;
    
    // Date Picker
    public string SelectDate => _currentLocalizer.SelectDate;
    public string Years => _currentLocalizer.Years;
    public string Months => _currentLocalizer.Months;
    public string QuickSelect => _currentLocalizer.QuickSelect;
    public string Today => _currentLocalizer.Today;
    public string Yesterday => _currentLocalizer.Yesterday;
    public string Tomorrow => _currentLocalizer.Tomorrow;
    public string SelectedDate => _currentLocalizer.SelectedDate;
    public string SelectDateFromCalendar => _currentLocalizer.SelectDateFromCalendar;
    
    // Range Picker
    public string SelectDateRange => _currentLocalizer.SelectDateRange;
    public string SelectDateRangePlaceholder => _currentLocalizer.SelectDateRangePlaceholder;
    public string Recent => _currentLocalizer.Recent;
    public string StartDate => _currentLocalizer.StartDate;
    public string EndDate => _currentLocalizer.EndDate;
    public string Days => _currentLocalizer.Days;
    public string SelectEndDateToComplete => _currentLocalizer.SelectEndDateToComplete;
    public string SelectStartAndEndDates => _currentLocalizer.SelectStartAndEndDates;
    
    // Range Shortcuts
    public string Last7Days => _currentLocalizer.Last7Days;
    public string Last14Days => _currentLocalizer.Last14Days;
    public string Last30Days => _currentLocalizer.Last30Days;
    public string Last60Days => _currentLocalizer.Last60Days;
    public string Last90Days => _currentLocalizer.Last90Days;
    public string LastWeek => _currentLocalizer.LastWeek;
    public string LastMonth => _currentLocalizer.LastMonth;
    public string LastQuarter => _currentLocalizer.LastQuarter;
    public string LastYear => _currentLocalizer.LastYear;
    public string WeekToDate => _currentLocalizer.WeekToDate;
    public string MonthToDate => _currentLocalizer.MonthToDate;
    public string QuarterToDate => _currentLocalizer.QuarterToDate;
    public string YearToDate => _currentLocalizer.YearToDate;
    public string Rolling7Days => _currentLocalizer.Rolling7Days;
    public string Rolling30Days => _currentLocalizer.Rolling30Days;
    public string Rolling90Days => _currentLocalizer.Rolling90Days;
    public string ThisWeek => _currentLocalizer.ThisWeek;
    public string ThisMonth => _currentLocalizer.ThisMonth;
    public string ThisQuarter => _currentLocalizer.ThisQuarter;
    public string ThisYear => _currentLocalizer.ThisYear;
    public string Next7Days => _currentLocalizer.Next7Days;
    public string Next14Days => _currentLocalizer.Next14Days;
    public string Next30Days => _currentLocalizer.Next30Days;
    public string Next90Days => _currentLocalizer.Next90Days;
    public string NextWeek => _currentLocalizer.NextWeek;
    public string NextMonthRange => _currentLocalizer.NextMonthRange;
    public string NextQuarter => _currentLocalizer.NextQuarter;
    public string NextYear => _currentLocalizer.NextYear;
    public string Custom => _currentLocalizer.Custom;
    
    // Method to get localized text for range shortcuts
    public string GetRangeShortcutText(RangeShortcut shortcut) => _currentLocalizer.GetRangeShortcutText(shortcut);
}
