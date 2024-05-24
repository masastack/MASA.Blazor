using Masa.Blazor;
using Masa.Blazor.Components.Drawflow;
using Masa.Blazor.Components.ScrollToTarget;
using Masa.Blazor.Components.Sortable;
using Masa.Blazor.Components.Xgplayer;
using Masa.Blazor.Presets.PageStack.NavController;
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
        services.AddBlazorComponent(masaBlazorServiceLifetime: masaBlazorServiceLifetime);
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
        var options = new MasaBlazorOptions();
        optionsAction.Invoke(options);

        services.AddBlazorComponent(o => { o.Locale = options.Locale; }, masaBlazorServiceLifetime);
        return services.AddMasaBlazorInternal(optionsAction, masaBlazorServiceLifetime);
    }

    private static IMasaBlazorBuilder AddMasaBlazorInternal(this IServiceCollection services,
        Action<MasaBlazorOptions>? optionsAction = null,
        ServiceLifetime masaBlazorServiceLifetime = ServiceLifetime.Scoped)
    {
        services.TryAdd<Application>(masaBlazorServiceLifetime);
        services.TryAdd(ServiceDescriptor.Describe(typeof(MasaBlazor), sp =>
        {
            var options = new MasaBlazorOptions();
            optionsAction?.Invoke(options);

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
        services.TryAddScoped<MarkdownItJSModule>();
        services.TryAddScoped<GridstackJSModule>();
        services.TryAddScoped<BaiduMapJSModule>();
        services.TryAddScoped<SwiperJsModule>();
        services.TryAddScoped<XgplayerJSModule>();
        services.TryAddScoped<DrawflowJSModule>();
        services.TryAddScoped<IntersectJSModule>();
        services.TryAddScoped<IResizeJSModule, ResizeJSModule>();
        services.TryAddScoped<ScrollToTargetJSModule>();
        services.TryAddScoped<SortableJSModule>();
        
        services.TryAddTransient<ActivatableJsModule>();

        services.TryAddScoped<IPageStackNavControllerFactory, PageStackNavControllerFactory>();
        services.TryAddScoped(s => s.GetRequiredService<IPageStackNavControllerFactory>().Create(string.Empty));

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