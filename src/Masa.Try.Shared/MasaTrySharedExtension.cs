using Masa.Try.Shared;

namespace Microsoft.Extensions.DependencyInjection;

public static class MasaTrySharedExtension
{
    /// <summary>
    /// WebAssembly Start
    /// </summary>
    public static bool WebAssembly { get; private set; }

    public static void AddMasaTryShared(this IServiceCollection services, bool webAssembly = false)
    {
        WebAssembly = webAssembly;
        services.AddMasaBlazor();
        services.AddScoped<TryJSModule>();
    }
}
