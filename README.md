# SophiChain Blazor DatePicker

[![NuGet](https://img.shields.io/nuget/v/SophiChain.BlazorDatePicker.svg)](https://www.nuget.org/packages/SophiChain.BlazorDatePicker/)
[![Downloads](https://img.shields.io/nuget/dt/SophiChain.BlazorDatePicker.svg)](https://www.nuget.org/packages/SophiChain.BlazorDatePicker/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

A powerful and customizable DatePicker component for Blazor applications with comprehensive multi-culture support, including Hijri calendar system and automatic localization.

## ✨ Features

- 🗓️ **Single Date Selection**: Clean and intuitive single date picker with keyboard navigation
- 📅 **Date Range Selection**: Advanced date range picker with visual feedback and preset ranges
- 🌍 **Auto-Localization**: Automatically detects UI culture and provides localized text
- 📆 **Multi-Calendar Support**: Full support for Gregorian, Hijri, and other calendar systems
- 🎨 **Highly Customizable**: Easy to style with CSS variables and custom classes
- ⚡ **Lightweight**: Minimal dependencies and optimized performance
- ♿ **Accessible**: WCAG compliant with full keyboard navigation and screen reader support
- 🚀 **Modern**: Built for .NET 8.0 with Blazor Server and WebAssembly support

### Supported Languages & Cultures

- 🇺🇸 **English** (Default)
- 🇸🇦 **Arabic** (with Hijri calendar support)
- 🇮🇷 **Persian** (Farsi)
- 🇫🇷 **French**
- 🇩🇪 **German** 
- 🇪🇸 **Spanish**

## 📦 Installation

### Package Manager Console
```powershell
Install-Package SophiChain.BlazorDatePicker
```

### .NET CLI
```bash
dotnet add package SophiChain.BlazorDatePicker
```

### PackageReference
```xml
<PackageReference Include="SophiChain.BlazorDatePicker" Version="1.0.0" />
```

## 🚀 Quick Start

### 1. Register Services

Add the DatePicker services to your `Program.cs`:

```csharp
using SophiChain.BlazorDatePicker.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Register SophiChain DatePicker with automatic localization
builder.Services.AddSophiChainBlazorDatePicker();

var app = builder.Build();
```

### 2. Add CSS Reference

Add the CSS file to your `_Host.cshtml` (Blazor Server) or `index.html` (Blazor WebAssembly):

```html
<link href="_content/SophiChain.BlazorDatePicker/scb-datepicker.css" rel="stylesheet" />
```

### 3. Add JavaScript Reference

```html
<script src="_content/SophiChain.BlazorDatePicker/SophiChainDatePicker.js"></script>
```

### 4. Import Namespace

Add to your `_Imports.razor`:

```razor
@using SophiChain.BlazorDatePicker.Components
```

## 📖 Basic Usage

### Single Date Picker

```razor
@page "/basic-datepicker"
@using SophiChain.BlazorDatePicker.Components

<h3>Single Date Selection</h3>

<SCBDatePicker @bind-Date="selectedDate" 
               Label="Select a date" 
               Placeholder="Choose a date..." />

<p>Selected Date: @selectedDate?.ToString("yyyy-MM-dd")</p>

@code {
    private DateTime? selectedDate = DateTime.Today;
}
```

### Date Range Picker

```razor
@page "/range-picker"
@using SophiChain.BlazorDatePicker.Components
@using SophiChain.BlazorDatePicker.Utilities

<h3>Date Range Selection</h3>

<SCBRangePicker @bind-DateRange="dateRange" 
                Label="Select date range"
                RememberRecentRanges="true" />

<div>
    @if (dateRange?.Start != null && dateRange?.End != null)
    {
        <p>Start Date: @dateRange.Start.Value.ToString("yyyy-MM-dd")</p>
        <p>End Date: @dateRange.End.Value.ToString("yyyy-MM-dd")</p>
        <p>Duration: @((dateRange.End.Value - dateRange.Start.Value).Days + 1) days</p>
    }
</div>

@code {
    private DateRange dateRange = new();
}
```

## 🌍 Multi-Cultural Support

### Automatic Culture Detection

The DatePicker automatically detects your application's current culture:

```razor
<!-- Will automatically use the current UI culture -->
<SCBDatePicker @bind-Date="selectedDate" 
               Label="Date" />
```

### Explicit Culture Setting

```razor
@using System.Globalization

<SCBDatePicker @bind-Date="selectedDate" 
               Culture="@(new CultureInfo("ar-SA"))"
               Label="التاريخ الهجري" />

@code {
    private DateTime? selectedDate;
}
```

### Hijri Calendar Example

```razor
@using System.Globalization

<h3>Hijri Calendar Support</h3>

<SCBDatePicker @bind-Date="hijriDate" 
               Culture="@arabicCulture"
               Label="التاريخ الهجري"
               DateFormat="dd/MM/yyyy" />

@code {
    private DateTime? hijriDate;
    private CultureInfo arabicCulture = new CultureInfo("ar-SA");
}
```

## ⚙️ Service Registration Options

### Default (Automatic Localization)
```csharp
services.AddSophiChainBlazorDatePicker();
```

### Custom Localizer
```csharp
services.AddSophiChainBlazorDatePickerWithCustomLocalizer<MyCustomLocalizer>();
```

### English Only
```csharp
services.AddSophiChainBlazorDatePickerEnglishOnly();
```

## 📅 RangeShortcut Documentation

The `SCBRangePicker` provides powerful preset functionality through the `RangeShortcut` enum, offering 30+ predefined date ranges for quick selection.

### Available RangeShortcut Values

| Shortcut | Description | Example Range |
|----------|-------------|---------------|
| **Relative Shortcuts** |||
| `Today` | Today only | Dec 15, 2023 - Dec 15, 2023 |
| `Yesterday` | Yesterday only | Dec 14, 2023 - Dec 14, 2023 |
| `Last7Days` | Last 7 days including today | Dec 9, 2023 - Dec 15, 2023 |
| `Last14Days` | Last 14 days including today | Dec 2, 2023 - Dec 15, 2023 |
| `Last30Days` | Last 30 days including today | Nov 16, 2023 - Dec 15, 2023 |
| `Last60Days` | Last 60 days including today | Oct 17, 2023 - Dec 15, 2023 |
| `Last90Days` | Last 90 days including today | Sep 17, 2023 - Dec 15, 2023 |
| **Period Shortcuts** |||
| `LastWeek` | Previous week (Mon-Sun or culture-specific) | Dec 4, 2023 - Dec 10, 2023 |
| `LastMonth` | Previous complete month | Nov 1, 2023 - Nov 30, 2023 |
| `LastQuarter` | Previous complete quarter | Jul 1, 2023 - Sep 30, 2023 |
| `LastYear` | Previous complete year | Jan 1, 2022 - Dec 31, 2022 |
| `ThisWeek` | Current week | Dec 11, 2023 - Dec 17, 2023 |
| `ThisMonth` | Current complete month | Dec 1, 2023 - Dec 31, 2023 |
| `ThisQuarter` | Current complete quarter | Oct 1, 2023 - Dec 31, 2023 |
| `ThisYear` | Current complete year | Jan 1, 2023 - Dec 31, 2023 |
| **To-Date Shortcuts** |||
| `WeekToDate` | Start of current week to today | Dec 11, 2023 - Dec 15, 2023 |
| `MonthToDate` | Start of current month to today | Dec 1, 2023 - Dec 15, 2023 |
| `QuarterToDate` | Start of current quarter to today | Oct 1, 2023 - Dec 15, 2023 |
| `YearToDate` | Start of current year to today | Jan 1, 2023 - Dec 15, 2023 |
| **Rolling Shortcuts** |||
| `Rolling7Days` | 7 days ago to today | Dec 8, 2023 - Dec 15, 2023 |
| `Rolling30Days` | 30 days ago to today | Nov 15, 2023 - Dec 15, 2023 |
| `Rolling90Days` | 90 days ago to today | Sep 16, 2023 - Dec 15, 2023 |
| **Future Shortcuts** |||
| `Next7Days` | Today to 7 days from today | Dec 15, 2023 - Dec 22, 2023 |
| `Next14Days` | Today to 14 days from today | Dec 15, 2023 - Dec 29, 2023 |
| `Next30Days` | Today to 30 days from today | Dec 15, 2023 - Jan 14, 2024 |
| `Next90Days` | Today to 90 days from today | Dec 15, 2023 - Mar 14, 2024 |
| `NextWeek` | Next complete week | Dec 18, 2023 - Dec 24, 2023 |
| `NextMonth` | Next complete month | Jan 1, 2024 - Jan 31, 2024 |
| `NextQuarter` | Next complete quarter | Jan 1, 2024 - Mar 31, 2024 |
| `NextYear` | Next complete year | Jan 1, 2024 - Dec 31, 2024 |
| **Business Shortcuts** |||
| `PreviousBusinessWeek` | Previous Monday-Friday | Dec 4, 2023 - Dec 8, 2023 |
| `PreviousBusinessMonth` | Previous complete month (simplified) | Nov 1, 2023 - Nov 30, 2023 |
| **Special Shortcuts** |||
| `AllTime` | Maximum possible date range | Min supported - Max supported |
| `Custom` | Manual selection mode | User-defined range |

### Basic Usage

```razor
<!-- Default presets (automatically filtered based on constraints) -->
<SCBRangePicker @bind-DateRange="dateRange" 
                Label="Select date range" />

<!-- All available presets -->
<SCBRangePicker @bind-DateRange="dateRange" 
                Presets="GetAllPresets()" />

@code {
    private DateRange? dateRange;
    
    private RangeShortcut[] GetAllPresets() => Enum.GetValues<RangeShortcut>();
}
```

### Custom Preset Arrays

Create specific preset combinations for different scenarios:

```razor
<!-- Business scenario presets -->
<SCBRangePicker @bind-DateRange="businessRange" 
                Presets="businessPresets"
                Label="Business Reports" />

<!-- Analytics scenario presets -->
<SCBRangePicker @bind-DateRange="analyticsRange" 
                Presets="analyticsPresets"
                Label="Analytics Period" />

<!-- Planning scenario presets -->
<SCBRangePicker @bind-DateRange="planningRange" 
                Presets="planningPresets"
                PastOnly="false"
                Label="Planning Period" />

@code {
    private DateRange? businessRange, analyticsRange, planningRange;
    
    // Business reporting presets
    private RangeShortcut[] businessPresets = new[]
    {
        RangeShortcut.Today,
        RangeShortcut.Yesterday,
        RangeShortcut.ThisWeek,
        RangeShortcut.LastWeek,
        RangeShortcut.MonthToDate,
        RangeShortcut.LastMonth,
        RangeShortcut.QuarterToDate,
        RangeShortcut.LastQuarter,
        RangeShortcut.YearToDate,
        RangeShortcut.PreviousBusinessWeek,
        RangeShortcut.PreviousBusinessMonth,
        RangeShortcut.Custom
    };
    
    // Analytics presets
    private RangeShortcut[] analyticsPresets = new[]
    {
        RangeShortcut.Last7Days,
        RangeShortcut.Last30Days,
        RangeShortcut.Last90Days,
        RangeShortcut.Rolling7Days,
        RangeShortcut.Rolling30Days,
        RangeShortcut.Rolling90Days,
        RangeShortcut.MonthToDate,
        RangeShortcut.QuarterToDate,
        RangeShortcut.YearToDate,
        RangeShortcut.Custom
    };
    
    // Planning presets (future-focused)
    private RangeShortcut[] planningPresets = new[]
    {
        RangeShortcut.Today,
        RangeShortcut.Next7Days,
        RangeShortcut.Next30Days,
        RangeShortcut.NextWeek,
        RangeShortcut.NextMonth,
        RangeShortcut.NextQuarter,
        RangeShortcut.ThisWeek,
        RangeShortcut.ThisMonth,
        RangeShortcut.ThisQuarter,
        RangeShortcut.Custom
    };
}
```

### Smart Filtering

The component automatically filters presets based on your constraints:

```razor
<!-- Past-only filtering: hides all future presets -->
<SCBRangePicker @bind-DateRange="pastRange" 
                PastOnly="true"
                Label="Historical Data" />

<!-- Future-only filtering: hides all past presets -->
<SCBRangePicker @bind-DateRange="futureRange" 
                FutureOnly="true"
                Label="Upcoming Events" />

<!-- Constraint-based filtering: hides presets that exceed limits -->
<SCBRangePicker @bind-DateRange="limitedRange" 
                MinDays="1"
                MaxDays="7"
                Label="Short Periods Only" />

@code {
    private DateRange? pastRange, futureRange, limitedRange;
}
```

### Working with Selected Presets

```razor
<SCBRangePicker @bind-DateRange="dateRange" 
                OnPreviewChange="OnRangePreview"
                OnApply="OnRangeApplied" />

<div class="selected-info">
    @if (selectedPreset.HasValue)
    {
        <p>Selected Preset: <strong>@selectedPreset.Value</strong></p>
    }
    
    @if (dateRange is { Start: not null, End: not null })
    {
        <p>Range: @dateRange.Start.Value.ToString("d") - @dateRange.End.Value.ToString("d")</p>
        <p>Duration: @((dateRange.End.Value - dateRange.Start.Value).Days + 1) days</p>
    }
</div>

@code {
    private DateRange? dateRange;
    private RangeShortcut? selectedPreset;
    
    private async Task OnRangePreview(DateRange? range)
    {
        // Get the preset that matches this range (if any)
        selectedPreset = GetPresetForRange(range);
        StateHasChanged();
    }
    
    private async Task OnRangeApplied(DateRange? range)
    {
        Console.WriteLine($"Applied range: {range?.Start:d} - {range?.End:d}");
        if (selectedPreset.HasValue)
        {
            Console.WriteLine($"Using preset: {selectedPreset.Value}");
        }
    }
    
    private RangeShortcut? GetPresetForRange(DateRange? range)
    {
        if (range?.Start == null || range?.End == null) return null;
        
        // This is a simplified example - the actual component does this internally
        var today = DateTime.Today;
        
        return range switch
        {
            var r when r.Start == today && r.End == today => RangeShortcut.Today,
            var r when r.Start == today.AddDays(-1) && r.End == today.AddDays(-1) => RangeShortcut.Yesterday,
            var r when r.Start == today.AddDays(-6) && r.End == today => RangeShortcut.Last7Days,
            _ => RangeShortcut.Custom
        };
    }
}
```

## 📋 Component Properties

### SCBDatePicker Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| **Basic Properties** ||||
| `Date` | `DateTime?` | `null` | Two-way bound selected date |
| `Label` | `string?` | `null` | Label text displayed above the picker |
| `Placeholder` | `string?` | `null` | Placeholder text when no date selected |
| `Culture` | `CultureInfo` | `Current UI Culture` | Culture for calendar system and formatting |
| `DateFormat` | `string?` | Culture default | Custom date format string |
| `Disabled` | `bool` | `false` | Whether the picker is disabled |
| `ReadOnly` | `bool` | `false` | Whether the picker is read-only |
| `Class` | `string?` | `null` | Additional CSS classes |
| `Style` | `string?` | `null` | Inline CSS styles |
| `ShowClear` | `bool` | `true` | Whether to show the Clear button |
| **Date Constraints** ||||
| `MinDate` | `DateTime?` | `null` | The minimum selectable date |
| `MaxDate` | `DateTime?` | `null` | The maximum selectable date |
| **Display Options** ||||
| `OpenTo` | `OpenTo` | `Date` | Initial view to display (Date, Month, Year) |
| `Color` | `Color` | `Primary` | Color theme (Primary, Secondary, Success, Warning, Error, etc.) |
| `Variant` | `Variant` | `Filled` | Display variant (Text, Filled, Outlined) |
| `PickerVariant` | `PickerVariant` | `Inline` | Picker behavior (Inline, Dialog, Static) |
| `FirstDayOfWeek` | `DayOfWeek?` | Culture default | First day of the week |
| `TitleDateFormat` | `string` | `"ddd, dd MMM"` | Format for date in the title |
| `ShowWeekNumbers` | `bool` | `false` | Show week numbers at start of each week |
| `ShowToolbar` | `bool` | `false` | Whether to show the toolbar |
| **Calendar Navigation** ||||
| `PickerMonth` | `DateTime?` | `null` | Current month shown in the calendar |
| `DisplayMonths` | `int` | `1` | Number of months to display |
| `MaxMonthColumns` | `int?` | `null` | Maximum months allowed in one row |
| `StartMonth` | `DateTime?` | `null` | Start month when opening picker |
| **Advanced Options** ||||
| `AutoClose` | `bool` | `false` | Close picker when value is selected |
| `ClosingDelay` | `int` | `100` | Delay before closing (milliseconds) |
| `FixYear` | `int?` | `null` | Fixed year that cannot be changed |
| `FixMonth` | `int?` | `null` | Fixed month that cannot be changed |
| `FixDay` | `int?` | `null` | Fixed day that cannot be changed |
| **Custom Functions** ||||
| `IsDateDisabledFunc` | `Func<DateTime, bool>?` | `null` | Function to disable specific dates |
| `AdditionalDateClassesFunc` | `Func<DateTime, string>?` | `null` | Function to add CSS classes to dates |
| **Events** ||||
| `DateChanged` | `EventCallback<DateTime?>` | - | Occurs when date changes |

### SCBRangePicker Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| **Basic Properties** ||||
| `DateRange` | `DateRange?` | `null` | Two-way bound selected date range |
| `Label` | `string?` | `null` | Label text displayed above the picker |
| `Culture` | `CultureInfo` | `Current UI Culture` | Culture for calendar system and formatting |
| `DateFormat` | `string?` | Culture default | Custom date format string |
| `Disabled` | `bool` | `false` | Whether the picker is disabled |
| `ReadOnly` | `bool` | `false` | Whether the picker is read-only |
| `Class` | `string?` | `null` | Additional CSS classes |
| `Style` | `string?` | `null` | Inline CSS styles |
| `ShowClear` | `bool` | `true` | Whether to show the Clear button |
| **Range Constraints** ||||
| `MinDate` | `DateTime?` | `null` | The minimum selectable date |
| `MaxDate` | `DateTime?` | `null` | The maximum selectable date |
| `MinDays` | `int?` | `null` | Minimum number of selectable days |
| `MaxDays` | `int?` | `null` | Maximum number of selectable days |
| `PastOnly` | `bool` | `false` | Restrict selections to past dates only |
| `FutureOnly` | `bool` | `false` | Restrict selections to future dates only |
| **Range Presets & Recent** ||||
| `Presets` | `RangeShortcut[]?` | Default presets | List of range shortcuts to display |
| `RememberRecentRanges` | `bool` | `true` | Whether to remember recent custom ranges |
| **Display Options** ||||
| `OpenTo` | `OpenTo` | `Date` | Initial view to display (Date, Month, Year) |
| `Color` | `Color` | `Primary` | Color theme (Primary, Secondary, Success, Warning, Error, etc.) |
| `Variant` | `Variant` | `Filled` | Display variant (Text, Filled, Outlined) |
| `PickerVariant` | `PickerVariant` | `Inline` | Picker behavior (Inline, Dialog, Static) |
| `FirstDayOfWeek` | `DayOfWeek?` | Culture default | First day of the week |
| `TitleDateFormat` | `string` | `"ddd, dd MMM"` | Format for date in the title |
| `ShowWeekNumbers` | `bool` | `false` | Show week numbers at start of each week |
| `ShowToolbar` | `bool` | `false` | Whether to show the toolbar |
| **Calendar Navigation** ||||
| `PickerMonth` | `DateTime?` | `null` | Current month shown in the calendar |
| `DisplayMonths` | `int` | `2` | Number of months to display (default 2 for range picker) |
| `MaxMonthColumns` | `int?` | `null` | Maximum months allowed in one row |
| `StartMonth` | `DateTime?` | `null` | Start month when opening picker |
| **Button Labels** ||||
| `ApplyButtonText` | `string` | `"Apply"` | Label for the Apply button |
| `CancelButtonText` | `string` | `"Cancel"` | Label for the Cancel button |
| `ClearButtonText` | `string` | `"Clear"` | Label for the Clear button |
| **Advanced Options** ||||
| `AutoClose` | `bool` | `false` | Close picker when value is selected |
| `ClosingDelay` | `int` | `100` | Delay before closing (milliseconds) |
| `FixYear` | `int?` | `null` | Fixed year that cannot be changed |
| `FixMonth` | `int?` | `null` | Fixed month that cannot be changed |
| `FixDay` | `int?` | `null` | Fixed day that cannot be changed |
| **Custom Functions** ||||
| `IsDateDisabledFunc` | `Func<DateTime, bool>?` | `null` | Function to disable specific dates |
| `AdditionalDateClassesFunc` | `Func<DateTime, string>?` | `null` | Function to add CSS classes to dates |
| **Events** ||||
| `DateRangeChanged` | `EventCallback<DateRange?>` | - | Occurs when date range changes and is applied |
| `OnOpen` | `EventCallback` | - | Occurs when picker opens |
| `OnClose` | `EventCallback` | - | Occurs when picker closes |
| `OnPreviewChange` | `EventCallback<DateRange?>` | - | Occurs when range preview changes (before apply) |
| `OnApply` | `EventCallback<DateRange?>` | - | Occurs when Apply is clicked |
| `OnCancel` | `EventCallback` | - | Occurs when Cancel is clicked |
| `OnClear` | `EventCallback` | - | Occurs when Clear is clicked |

### CSS Customization

The DatePicker uses CSS variables for easy theming:

```css
:root {
    --scb-primary-color: #007bff;
    --scb-primary-hover: #0056b3;
    --scb-background: #ffffff;
    --scb-border-color: #dee2e6;
    --scb-text-color: #212529;
    --scb-disabled-background: #f8f9fa;
    --scb-disabled-color: #6c757d;
}

/* Dark theme example */
[data-theme="dark"] {
    --scb-background: #343a40;
    --scb-border-color: #6c757d;
    --scb-text-color: #ffffff;
    --scb-disabled-background: #495057;
}
```

### Custom CSS Classes

```razor
<SCBDatePicker @bind-Date="selectedDate" 
               Class="my-custom-datepicker"
               Style="width: 300px; margin: 10px;" />
```

## 🔧 Advanced Examples

### Validation Integration

```razor
@using System.ComponentModel.DataAnnotations

<EditForm Model="@model" OnValidSubmit="@OnValidSubmit">
    <DataAnnotationsValidator />
    
    <div class="form-group">
        <SCBDatePicker @bind-Date="@model.BirthDate" 
                       Label="Birth Date" />
        <ValidationMessage For="@(() => model.BirthDate)" />
    </div>
    
    <div class="form-group">
        <SCBRangePicker @bind-DateRange="@model.ProjectDuration" 
                        Label="Project Duration" />
        <ValidationMessage For="@(() => model.ProjectDuration)" />
    </div>
    
    <button type="submit" class="btn btn-primary">Submit</button>
</EditForm>

@code {
    private PersonModel model = new();
    
    private void OnValidSubmit()
    {
        // Handle valid submission
    }
    
    public class PersonModel
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }
        
        [Required]
        public DateRange ProjectDuration { get; set; } = new();
    }
}
```

### Event Handling

```razor
<SCBDatePicker @bind-Date="selectedDate"
               OnDateChanged="@OnDateChanged"
               OnPickerOpened="@OnPickerOpened"
               OnPickerClosed="@OnPickerClosed" />

@code {
    private DateTime? selectedDate;
    
    private async Task OnDateChanged(DateTime? newDate)
    {
        // Handle date change
        Console.WriteLine($"Date changed to: {newDate}");
    }
    
    private async Task OnPickerOpened()
    {
        // Handle picker opened
        Console.WriteLine("Picker opened");
    }
    
    private async Task OnPickerClosed()
    {
        // Handle picker closed  
        Console.WriteLine("Picker closed");
    }
}
```

### Custom Localization

```csharp
public class CustomLocalizer : IDatePickerLocalizer
{
    public string Close => "Close";
    public string Clear => "Clear";
    public string Cancel => "Cancel";
    public string Apply => "Apply";
    public string Selected => "Selected";
    public string PreviousMonth => "Previous Month";
    public string NextMonth => "Next Month";
    public string SelectDate => "Select Date";
    public string Years => "Years";
    public string Months => "Months";
    public string QuickSelect => "Quick Select";
    public string Today => "Today";
    public string Yesterday => "Yesterday";
    public string Tomorrow => "Tomorrow";
    public string SelectedDate => "Selected Date";
    public string SelectDateFromCalendar => "Select Date from Calendar";
    public string SelectDateRange => "Select Date Range";
    public string SelectDateRangePlaceholder => "Select date range";
    public string Recent => "Recent";
    public string StartDate => "Start Date";
    public string EndDate => "End Date";
    public string Days => "Days";
    public string SelectEndDateToComplete => "Select end date to complete";
    public string SelectStartAndEndDates => "Select start and end dates";
}
```

## 🛠️ Requirements

- .NET 8.0 or later
- Blazor Server or Blazor WebAssembly
- Modern web browser with JavaScript enabled

## 📚 API Reference

### DateRange Class

The `DateRange` class represents a date range with start and end dates:

```csharp
public class DateRange
{
    public DateTime? Start { get; set; }
    public DateTime? End { get; set; }
    
    // Useful properties
    public bool IsValid => Start.HasValue && End.HasValue && Start <= End;
    public TimeSpan? Duration => IsValid ? End - Start : null;
    public int Days => IsValid ? ((End!.Value - Start!.Value).Days + 1) : 0;
    
    // Constructors
    public DateRange() { }
    public DateRange(DateTime? start, DateTime? end) 
    { 
        Start = start; 
        End = end; 
    }
}
```

### Enum Values

#### Color Enum
Available color themes for both date and range pickers:
- `Default` - Default theme
- `Primary` - Primary theme (default)
- `Secondary` - Secondary theme
- `Info` - Information theme
- `Success` - Success theme
- `Warning` - Warning theme
- `Error` - Error theme
- `Dark` - Dark theme

#### Variant Enum
Display variants for input styling:
- `Text` - No background or border
- `Filled` - Solid background (default)
- `Outlined` - Border outline

#### PickerVariant Enum
Picker display behavior:
- `Inline` - Shows when input is clicked (default)
- `Dialog` - Shows in a dialog/modal
- `Static` - Always visible

#### OpenTo Enum
Initial view when picker opens:
- `Date` - Day picker view (default)
- `Month` - Month selection view
- `Year` - Year selection view

### Usage Examples

```csharp
// Create and use a date range
var range = new DateRange(DateTime.Today, DateTime.Today.AddDays(7));

if (range.IsValid)
{
    Console.WriteLine($"Range spans {range.Days} days");
    Console.WriteLine($"Duration: {range.Duration?.TotalDays} days");
}
```

```razor
<!-- Using different themes and variants -->
<SCBDatePicker @bind-Date="primaryDate" 
               Color="Color.Primary"
               Variant="Variant.Filled"
               PickerVariant="PickerVariant.Inline" />

<SCBRangePicker @bind-DateRange="warningRange" 
                Color="Color.Warning"
                Variant="Variant.Outlined"
                OpenTo="OpenTo.Month" />

@code {
    private DateTime? primaryDate;
    private DateRange warningRange = new();
}
```

## 🚀 Performance Tips

1. **Disable recent ranges** if not needed: `RememberRecentRanges="false"`
2. **Set explicit culture** for better performance: `Culture="@(new CultureInfo("en-US"))"`
3. **Use specific presets** instead of all defaults to reduce UI complexity
4. **Limit DisplayMonths** for range picker in mobile scenarios

## 🐛 Troubleshooting

### DatePicker not showing or styled incorrectly
Ensure CSS is included in your `_Host.cshtml` or `index.html`:
```html
<link href="_content/SophiChain.BlazorDatePicker/scb-datepicker.css" rel="stylesheet" />
```

### JavaScript errors in browser console
Ensure JS is included after Blazor framework:
```html
<script src="_framework/blazor.server.js"></script>
<script src="_content/SophiChain.BlazorDatePicker/SophiChainDatePicker.js"></script>
```

### Localization not working
Ensure services are registered in `Program.cs`:
```csharp
builder.Services.AddSophiChainBlazorDatePicker();
```

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request. See the project repository for development guidelines.

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 👨‍💻 Author

**Pooria Shariatzadeh** - [SophiChain](https://github.com/sophichain)

## 🙏 Acknowledgments

- Built with ❤️ for the Blazor community
- Inspired by modern date picker UX patterns
- Special thanks to contributors and testers

---

## 📈 Changelog

### Version 1.0.0
- 🎉 Initial release
- ✅ Single date picker component
- ✅ Date range picker component  
- ✅ Multi-cultural support (English, Arabic, Persian, French, German, Spanish)
- ✅ Hijri calendar support
- ✅ Automatic localization
- ✅ Accessibility features
- ✅ Keyboard navigation
- ✅ Customizable styling
- ✅ TypeScript definitions
- ✅ Comprehensive documentation

---

*Made with ❤️ by SophiChain*
