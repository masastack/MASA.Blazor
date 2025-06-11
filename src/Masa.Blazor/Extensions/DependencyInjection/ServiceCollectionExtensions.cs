using Masa.Blazor;
using Masa.Blazor.Components.Drawflow;
using Masa.Blazor.Components.ErrorHandler;
using Masa.Blazor.Components.Input;
using Masa.Blazor.Components.ScrollToTarget;
using Masa.Blazor.Components.Sortable;
using Masa.Blazor.Components.Transition;
using Masa.Blazor.JSModules;
using Masa.Blazor.Mixins.Activatable;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add common services required by MASA Blazor components.
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <param name="masaBlazorServiceLifetime">The service lifetime of <see cref="MasaBlazor"/> service.</param>
    public static IMasaBlazorBuilder AddMasaBlazor(this IServiceCollection services,
        ServiceLifetime masaBlazorServiceLifetime = ServiceLifetime.Scoped)
    {
        return services.AddMasaBlazorInternal(masaBlazorServiceLifetime: masaBlazorServiceLifetime);
    }

    /// <summary>
    /// Add common services required by MASA Blazor components.
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <param name="optionsAction">The action to configure the options.</param>
    /// <param name="masaBlazorServiceLifetime">The service lifetime of <see cref="MasaBlazor"/> service.</param>
    public static IMasaBlazorBuilder AddMasaBlazor(this IServiceCollection services,
        Action<MasaBlazorOptions> optionsAction,
        ServiceLifetime masaBlazorServiceLifetime = ServiceLifetime.Scoped)
    {
        return services.AddMasaBlazorInternal(optionsAction, masaBlazorServiceLifetime);
    }

    private static IMasaBlazorBuilder AddMasaBlazorInternal(this IServiceCollection services,
        Action<MasaBlazorOptions>? optionsAction = null,
        ServiceLifetime masaBlazorServiceLifetime = ServiceLifetime.Scoped)
    {
        services.TryAddScoped<MasaBlazorOptions>(_ =>
        {
            var options = new MasaBlazorOptions();
            optionsAction?.Invoke(options);
            return options;
        });

        services.TryAddScoped<LocalStorage>();
        services.TryAddScoped<I18n>(sp =>
        {
            var options = sp.GetRequiredService<MasaBlazorOptions>();
            return new I18n(options);
        });

        services.TryAdd<Application>(masaBlazorServiceLifetime);
        services.TryAdd(ServiceDescriptor.Describe(typeof(MasaBlazor), sp =>
        {
            var options = sp.GetRequiredService<MasaBlazorOptions>();
            var application = sp.GetRequiredService<Application>();
            return new MasaBlazor(
                options.RTL,
                options.Breakpoint,
                application,
                options.Theme,
                options.Icons,
                options.SSR,
                options.Defaults);
        }, masaBlazorServiceLifetime));

        services.TryAdd<IPopupService, PopupService>(masaBlazorServiceLifetime);

        services.TryAddScoped<IErrorHandler, MErrorHandler>();
        services.TryAddSingleton<InternalHttpClient>();
        services.TryAddScoped<EChartsJSModule>();
        services.TryAddScoped<MonacoEditorJSModule>();
        services.TryAddScoped<BaiduMapJSModule>();
        services.TryAddScoped<DrawflowJSModule>();
        services.TryAddScoped<IntersectJSModule>();
        services.TryAddScoped<IResizeJSModule, ResizeJSModule>();
        services.TryAddScoped<ScrollToTargetJSModule>();
        services.TryAddScoped<SortableJSModule>();

        services.TryAddTransient<ActivatableJsModule>();
        services.TryAddTransient<InputJSModule>();
        services.TryAddTransient<TransitionJSModule>();
        services.TryAddTransient<OutsideClickJSModule>();
        services.TryAddScoped<ScrollStrategyJSModule>();

        return new MasaBlazorBuilder(services);
    }

    private static void TryAdd<TService, TImplementation>(this IServiceCollection services, ServiceLifetime lifetime)
        where TService : class
        where TImplementation : class, TService
    {
        services.TryAdd(new ServiceDescriptor(typeof(TService), typeof(TImplementation), lifetime));
    }

    private static void TryAdd<TService>(this IServiceCollection services, ServiceLifetime lifetime)
        where TService : class
    {
        services.TryAdd(new ServiceDescriptor(typeof(TService), typeof(TService), lifetime));
    }
}