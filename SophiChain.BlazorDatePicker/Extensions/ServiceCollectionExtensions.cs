using Microsoft.Extensions.DependencyInjection;
using SophiChain.BlazorDatePicker.Localizations;
using SophiChain.BlazorDatePicker.Services;

namespace SophiChain.BlazorDatePicker.Extensions;

/// <summary>
/// Extension methods for IServiceCollection to register SophiChain DatePicker services.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds SophiChain Blazor DatePicker services to the service collection with automatic culture-based localization.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddSophiChainBlazorDatePicker(this IServiceCollection services)
    {
        services.AddScoped<IScrollManager, ScrollManager>();
        services.AddScoped<IJsApiService, JsApiService>();
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<IDatePickerLocalizerFactory, DatePickerLocalizerFactory>();
        
        return services;
    }

    /// <summary>
    /// Adds SophiChain Blazor DatePicker services to the service collection with a specific custom localizer.
    /// This will override the automatic culture-based localization.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddSophiChainBlazorDatePickerWithCustomLocalizer<TLocalizer>(this IServiceCollection services) 
        where TLocalizer : class, IDatePickerLocalizer, new()
    {
        services.AddScoped<IScrollManager, ScrollManager>();
        services.AddScoped<IJsApiService, JsApiService>();
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<IDatePickerLocalizerFactory>(provider => new CustomLocalizerFactory<TLocalizer>());
        
        return services;
    }

    /// <summary>
    /// Adds SophiChain Blazor DatePicker services to the service collection using the default English localizer only.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddSophiChainBlazorDatePickerEnglishOnly(this IServiceCollection services)
    {
        services.AddScoped<IScrollManager, ScrollManager>();
        services.AddScoped<IJsApiService, JsApiService>();
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<IDatePickerLocalizerFactory>(provider => new CustomLocalizerFactory<DefaultDatePickerLocalizer>());
        
        return services;
    }
}
