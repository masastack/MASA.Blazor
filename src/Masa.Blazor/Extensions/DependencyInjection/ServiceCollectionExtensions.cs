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
#if NET8_0
            var isSSR = services.Any(s => s.ServiceType.FullName == "Microsoft.AspNetCore.Components.Endpoints.IComponentPrerenderer");
            MasaBlazor.RenderingEnvironment = GetRenderingEnvironment(serviceProvider, isSSR);
#else
            MasaBlazor.RenderingEnvironment = GetRenderingEnvironment(serviceProvider, false);
#endif

            var application = serviceProvider.GetRequiredService<Application>();
            var window = serviceProvider.GetRequiredService<Window>();
            var options = serviceProvider.GetRequiredService<IOptionsSnapshot<MasaBlazorOptions>>();
            options.Value.Breakpoint.SetWindow(window);

            return new MasaBlazor(options.Value.RTL, options.Value.Breakpoint, application, options.Value.Theme, options.Value.Icons,
                options.Value.Defaults);
        });
        services.TryAddScoped<IPopupService, PopupService>();
        services.TryAddScoped<IErrorHandler, MErrorHandler>();
        services.AddSingleton<IAbstractComponentTypeMapper, MasaBlazorComponentTypeMapper>();

        services.TryAddScoped<EChartsJSModule>();
        services.TryAddScoped<MonacoEditorJSModule>();
        services.TryAddScoped<MarkdownItJSModule>();
        services.TryAddScoped<GridstackJSModule>();
        services.TryAddScoped<BaiduMapJSModule>();
        services.TryAddScoped<SwiperJsModule>();
        services.TryAddScoped<DrawflowJSModule>();
        services.TryAddScoped<IntersectJSModule>();

        return new MasaBlazorBuilder(services);
    }

    private static RenderingEnvironment GetRenderingEnvironment(IServiceProvider serviceProvider, bool isSSR)
    {
        RenderingEnvironment renderingEnvironment = RenderingEnvironment.Unknown;

        var jsRuntime = serviceProvider.GetRequiredService<IJSRuntime>();

        if (isSSR)
        {
            if (jsRuntime is JSInProcessRuntime)
            {
                renderingEnvironment = RenderingEnvironment.SSRWebAssembly;
            }
            else if (jsRuntime.GetType().Name == "UnsupportedJavaScriptRuntime")
            {
                renderingEnvironment = RenderingEnvironment.SSRStatic;
            }
            else if (jsRuntime.GetType().Name == "RemoteJSRuntime")
            {
                renderingEnvironment = RenderingEnvironment.SSRServer;
            }
        }
        else if (jsRuntime is JSInProcessRuntime)
        {
            renderingEnvironment = RenderingEnvironment.WebAssembly;
        }
        else if (jsRuntime.GetType().Name == "RemoteJSRuntime")
        {
            renderingEnvironment = RenderingEnvironment.Server;
        }
        else if (jsRuntime.GetType().Name == "WebViewJSRuntime")
        {
            renderingEnvironment = RenderingEnvironment.WebView;
        }
        else if (jsRuntime.GetType().Name == "UnsupportedJavaScriptRuntime")
        {
            renderingEnvironment = RenderingEnvironment.Static;
        }
        else
        {
            renderingEnvironment = RenderingEnvironment.Unknown;
        }

        return renderingEnvironment;
    }
}
