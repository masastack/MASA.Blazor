using BlazorComponent.Web;
using Masa.Blazor;
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
        services.AddBlazorComponent();
        services.AddOptions<MasaBlazorOptions>().Configure(optionsAction);
        return services.AddMasaBlazorInternal();
    }

    private static IMasaBlazorBuilder AddMasaBlazorInternal(this IServiceCollection services)
    {
        services.TryAddScoped<Application>();
        services.TryAddScoped(serviceProvider =>
        {
            var application = serviceProvider.GetService<Application>();
            var window = serviceProvider.GetService<Window>();
            var options = serviceProvider.GetService<IOptionsSnapshot<MasaBlazorOptions>>();
            options.Value.Breakpoint.SetWindow(window);
            return new MasaBlazor(options.Value.Breakpoint, application, options.Value.Theme);
        });
        services.TryAddScoped<IPopupService, PopupService>();
        services.TryAddScoped<IErrorHandler, MErrorHandler>();
        services.AddSingleton<IAbstractComponentTypeMapper, MasaBlazorComponentTypeMapper>();

        services.TryAddScoped<EChartsJSModule>();
        services.TryAddScoped<MonacoEditorJSModule>();
        services.TryAddScoped<MarkdownItJSModule>();
        services.TryAddScoped<GridstackJSModule>();

        return new MasaBlazorBuilder(services);
    }
}
