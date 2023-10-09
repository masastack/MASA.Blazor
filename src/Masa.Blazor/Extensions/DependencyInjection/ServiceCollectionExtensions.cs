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
        services.AddOptions<MasaBlazorOptions>();
        return services.AddMasaBlazorInternal();
    }

    public static IMasaBlazorBuilder AddMasaBlazor(this IServiceCollection services, Action<MasaBlazorOptions> optionsAction)
    {
        var options = new MasaBlazorOptions();
        optionsAction.Invoke(options);

        services.AddBlazorComponent(o => { o.Locale = options.Locale; });
        services.AddOptions<MasaBlazorOptions>().Configure(optionsAction);
        return services.AddMasaBlazorInternal();
    }

    private static IMasaBlazorBuilder AddMasaBlazorInternal(this IServiceCollection services)
    {
        services.TryAddScoped<Application>();
        services.TryAddScoped(serviceProvider =>
        {
            var application = serviceProvider.GetRequiredService<Application>();
            var window = serviceProvider.GetRequiredService<Window>();
            var options = serviceProvider.GetRequiredService<IOptionsSnapshot<MasaBlazorOptions>>();
            options.Value.Breakpoint.SetWindow(window);
            return new MasaBlazor(
                options.Value.RTL,
                options.Value.Breakpoint,
                application,
                options.Value.Theme,
                options.Value.Icons,
                options.Value.SSR,
                options.Value.Defaults);
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
        services.TryAddScoped<DrawflowJSModule>();
        services.TryAddScoped<IntersectJSModule>();
        services.TryAddScoped<IResizeJSModule, ResizeJSModule>();

        return new MasaBlazorBuilder(services);
    }
}
