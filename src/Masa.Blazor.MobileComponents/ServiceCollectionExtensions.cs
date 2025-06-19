using Masa.Blazor.Presets.PageStack.NavController;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IMasaBlazorBuilder AddMobileComponents(this IMasaBlazorBuilder builder,
        ServiceLifetime masaBlazorServiceLifetime = ServiceLifetime.Scoped)
    {
        builder.Services.TryAddScoped<IPageStackNavControllerFactory, PageStackNavControllerFactory>();
        builder.Services.TryAddScoped(sp =>
            sp.GetRequiredService<IPageStackNavControllerFactory>().Create(string.Empty));

        return builder;
    }
}