using Masa.Blazor;
using Masa.Blazor.Components.Drawflow;
using Masa.Blazor.Components.ScrollToTarget;
using Masa.Blazor.Components.Xgplayer;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IMasaBlazorBuilder AddMasaBlazor(this IServiceCollection services)
    {
        services.AddBlazorComponent();
        return services.AddMasaBlazorInternal();
    }

    public static IMasaBlazorBuilder AddMasaBlazor(this IServiceCollection services, Action<MasaBlazorOptions> optionsAction)
    {
        var options = new MasaBlazorOptions();
        optionsAction.Invoke(options);

        services.AddBlazorComponent(o => { o.Locale = options.Locale; });
        return services.AddMasaBlazorInternal(optionsAction);
    }

    private static IMasaBlazorBuilder AddMasaBlazorInternal(this IServiceCollection services, Action<MasaBlazorOptions>? optionsAction = null)
    {
        services.TryAddScoped<Application>();
        services.TryAddScoped(serviceProvider =>
        {
            var options = new MasaBlazorOptions();
            optionsAction?.Invoke(options);

            var application = serviceProvider.GetRequiredService<Application>();
            return new MasaBlazor(
                options.RTL,
                options.Breakpoint,
                application,
                options.Theme,
                options.Icons,
                options.SSR,
                options.Defaults);
        });

        services.TryAddScoped<IPopupService, PopupService>();
        services.TryAddScoped<IErrorHandler, MErrorHandler>();
        services.AddSingleton<IAbstractComponentTypeMapper, MasaBlazorComponentTypeMapper>();
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

        return new MasaBlazorBuilder(services);
    }
}
