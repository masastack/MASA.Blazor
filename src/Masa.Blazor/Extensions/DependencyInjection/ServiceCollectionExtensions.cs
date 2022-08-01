using BlazorComponent.Web;
using Masa.Blazor;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IMasaBlazorBuilder AddMasaBlazor(this IServiceCollection services, MasaBlazorOptions options = null)
    {
        InitGlobalVariables(options);

        services.AddBlazorComponent();
        services.TryAddScoped<MasaBlazor>();
        services.TryAddScoped<Application>();
        services.TryAddScoped(serviceProvider => new Breakpoint(serviceProvider.GetService<Window>())
        {
            MobileBreakpoint = MasaBlazorVariables.Breakpoint.MobileBreakpoint,
            ScrollBarWidth = MasaBlazorVariables.Breakpoint.ScrollBarWidth,
            Thresholds = MasaBlazorVariables.Breakpoint.Thresholds
        });
        services.TryAddScoped<IPopupService, PopupService>();
        services.TryAddScoped<IErrorHandler, MErrorHandler>();
        services.AddSingleton<IAbstractComponentTypeMapper, MasaBlazorComponentTypeMapper>();

        return new MasaBlazorBuilder(services);
    }

    public static IMasaBlazorBuilder AddMasaBlazor(this IServiceCollection services, Action<MasaBlazorOptions> optionsAction)
    {
        var options = new MasaBlazorOptions();
        optionsAction?.Invoke(options);

        return services.AddMasaBlazor(options);
    }

    private static void InitGlobalVariables(MasaBlazorOptions options)
    {
        options ??= new MasaBlazorOptions();

        Variables.DarkTheme = options.DarkTheme;
        Variables.Theme = options.Theme;
        MasaBlazorVariables.Breakpoint = options.Breakpoint;
    }
}