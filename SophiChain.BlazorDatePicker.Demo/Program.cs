using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SophiChain.BlazorDatePicker.Demo.Components;
using SophiChain.BlazorDatePicker.Extensions;
using Microsoft.JSInterop;
using System.Globalization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Add SophiChain DatePicker services (automatically uses culture-aware localization)
builder.Services.AddSophiChainBlazorDatePicker();

// Configure localization for WebAssembly
builder.Services.AddLocalization();

var host = builder.Build();

// Set up Persian culture for WebAssembly with proper month/day names
var persianCulture = new CultureInfo("fa-IR");
persianCulture.DateTimeFormat.Calendar = new System.Globalization.PersianCalendar();

// Set Persian month names explicitly for WebAssembly
persianCulture.DateTimeFormat.MonthNames = new string[]
{
    "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور",
    "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند", ""
};

// Set Persian abbreviated month names
persianCulture.DateTimeFormat.AbbreviatedMonthNames = new string[]
{
    "فرو", "ارد", "خرد", "تیر", "مرد", "شهر",
    "مهر", "آبان", "آذر", "دی", "بهم", "اسف", ""
};

// Set Persian day names
persianCulture.DateTimeFormat.DayNames = new string[]
{
    "یکشنبه", "دوشنبه", "سه‌شنبه", "چهارشنبه", "پنج‌شنبه", "جمعه", "شنبه"
};

// Set Persian abbreviated day names  
persianCulture.DateTimeFormat.AbbreviatedDayNames = new string[]
{
    "ی", "د", "س", "چ", "پ", "ج", "ش"
};

CultureInfo.DefaultThreadCurrentCulture = persianCulture;
CultureInfo.DefaultThreadCurrentUICulture = persianCulture;

await host.RunAsync();
