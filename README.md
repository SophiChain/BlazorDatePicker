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

## 📅 Range Shortcuts (Presets)

The Range Picker includes **30+ predefined shortcuts** for quick date selection:

### Built-in Shortcuts
- **Relative**: Today, Yesterday, Last 7/30/90 Days
- **Periods**: This Week/Month/Quarter/Year, Last Week/Month/Quarter/Year  
- **Business**: Previous Business Week/Month, Week/Month/Quarter to Date
- **Future**: Next 7/30 Days, Next Week/Month/Quarter/Year
- **Custom**: Manual date selection

### Custom Preset Arrays
```razor
<!-- Use specific presets for business scenarios -->
<SCBRangePicker Presets="businessPresets" />

@code {
    private RangeShortcut[] businessPresets = new[]
    {
        RangeShortcut.Today,
        RangeShortcut.ThisWeek,
        RangeShortcut.LastWeek,
        RangeShortcut.MonthToDate,
        RangeShortcut.LastMonth,
        RangeShortcut.Custom // Always include for manual selection
    };
}
```

### Smart Filtering
- **PastOnly**: Automatically hides future-related shortcuts
- **FutureOnly**: Automatically hides past-related shortcuts
- **Constraints**: Respects MinDays/MaxDays limits

## 🎨 Customization

### Component Properties

#### SCBDatePicker Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `Date` | `DateTime?` | `null` | Two-way bound selected date |
| `Label` | `string` | `""` | Label text displayed above the picker |
| `Placeholder` | `string` | `"Select date"` | Placeholder text when no date selected |
| `Culture` | `CultureInfo` | `Current UI Culture` | Culture for calendar system and formatting |
| `DateFormat` | `string` | Culture default | Custom date format string |
| `Disabled` | `bool` | `false` | Whether the picker is disabled |
| `ReadOnly` | `bool` | `false` | Whether the picker is read-only |
| `Class` | `string` | `""` | Additional CSS classes |
| `Style` | `string` | `""` | Inline CSS styles |

#### SCBRangePicker Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `DateRange` | `DateRange` | `new()` | Two-way bound selected date range |
| `Label` | `string` | `""` | Label text displayed above the picker |
| `Culture` | `CultureInfo` | `Current UI Culture` | Culture for calendar system and formatting |
| `DateFormat` | `string` | Culture default | Custom date format string |
| `RememberRecentRanges` | `bool` | `true` | Whether to remember recent selections |
| `Disabled` | `bool` | `false` | Whether the picker is disabled |
| `ReadOnly` | `bool` | `false` | Whether the picker is read-only |
| `Class` | `string` | `""` | Additional CSS classes |
| `Style` | `string` | `""` | Inline CSS styles |

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

```csharp
public class DateRange
{
    public DateTime? Start { get; set; }
    public DateTime? End { get; set; }
    public bool IsValid => Start.HasValue && End.HasValue && Start <= End;
    public TimeSpan? Duration => IsValid ? End - Start : null;
}
```

### Service Interfaces

- `IDatePickerLocalizer` - Interface for custom localization
- `IDatePickerLocalizerFactory` - Factory for creating localizers
- `IJsApiService` - JavaScript interop service
- `IScrollManager` - Manages scroll behavior
- `IDateTimeProvider` - Provides current date/time

## 🚀 Performance Tips

1. **Use `RememberRecentRanges="false"`** if you don't need recent range memory
2. **Set explicit Culture** instead of relying on automatic detection for better performance
3. **Use CSS containment** for large lists of date pickers
4. **Implement virtual scrolling** for applications with many date picker instances

## 🐛 Troubleshooting

### Common Issues

**Issue**: DatePicker not showing or styling issues
```html
<!-- Ensure CSS is included -->
<link href="_content/SophiChain.BlazorDatePicker/scb-datepicker.css" rel="stylesheet" />
```

**Issue**: JavaScript errors
```html
<!-- Ensure JS is included after Blazor JS -->
<script src="_framework/blazor.server.js"></script>
<script src="_content/SophiChain.BlazorDatePicker/SophiChainDatePicker.js"></script>
```

**Issue**: Localization not working
```csharp
// Ensure services are registered
builder.Services.AddSophiChainBlazorDatePicker();
```

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

### Development Setup

1. Clone the repository
2. Open in Visual Studio 2022 or VS Code
3. Restore NuGet packages: `dotnet restore`
4. Build the solution: `dotnet build`
5. Run the demo: `dotnet run --project SophiChain.BlazorDatePicker.Demo`

### Guidelines

- Follow the existing coding style
- Add unit tests for new features
- Update documentation for API changes
- Test with multiple cultures and calendars

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
