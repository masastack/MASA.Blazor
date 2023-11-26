using Masa.Blazor;
using Masa.Blazor.Components.Drawflow;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IMasaBlazorBuilder AddMasaBlazor(this IServiceCollection services)
    {
        services.AddBlazorComponent();
        return services.AddMasaBlazorInternal(new MasaBlazorOptions());
    }

    public static IMasaBlazorBuilder AddMasaBlazor(this IServiceCollection services, Action<MasaBlazorOptions> optionsAction)
    {
        var options = new MasaBlazorOptions();
        optionsAction.Invoke(options);

        services.AddBlazorComponent(o => { o.Locale = options.Locale; });
        return services.AddMasaBlazorInternal(options);
    }

    private static IMasaBlazorBuilder AddMasaBlazorInternal(this IServiceCollection services, MasaBlazorOptions options)
    {
        services.TryAddScoped<Application>();
        services.TryAddScoped(serviceProvider =>
        {
            var application = serviceProvider.GetRequiredService<Application>();
            var window = serviceProvider.GetRequiredService<Window>();
            options.Breakpoint.SetWindow(window);
            return new MasaBlazor(
                options.RTL,
                options.Breakpoint,
                application,
                options.Theme,
                options.Icons,
                options.SSR,
                options.Defaults);
        });

#if NET8_0_OR_GREATER
        services.AddCascadingMasaBlazor();
#endif

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
        services.TryAddScoped<DrawflowJSModule>();
        services.TryAddScoped<IntersectJSModule>();
        services.TryAddScoped<IResizeJSModule, ResizeJSModule>();

        return new MasaBlazorBuilder(services);
    }
}
