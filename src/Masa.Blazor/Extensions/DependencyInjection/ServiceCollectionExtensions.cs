using BlazorComponent.Web;
using Masa.Blazor;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IMasaBlazorBuilder AddMasaBlazor(this IServiceCollection services, Action<MasaBlazorOptions> optionsAction = null)
    {
        services.AddBlazorComponent();

        var options = new MasaBlazorOptions();

        optionsAction?.Invoke(options);

        return services.AddMasaBlazor(options);
    }

    private static IMasaBlazorBuilder AddMasaBlazor(this IServiceCollection services, MasaBlazorOptions options)
    {
        services.TryAddScoped<Application>();
        services.TryAddScoped(serviceProvider =>
        {
            var application = serviceProvider.GetService<Application>();
            var window = serviceProvider.GetService<Window>();
            options.Breakpoint.SetWindow(window);
            return new MasaBlazor(options.Breakpoint, application, options.Theme);
        });
        services.TryAddScoped<IPopupService, PopupService>();
        services.TryAddScoped<IErrorHandler, MErrorHandler>();
        services.AddSingleton<IAbstractComponentTypeMapper, MasaBlazorComponentTypeMapper>();

        return new MasaBlazorBuilder(services);
    }
}
