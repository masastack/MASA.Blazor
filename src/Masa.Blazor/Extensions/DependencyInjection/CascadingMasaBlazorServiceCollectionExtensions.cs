#if NET8_0_OR_GREATER
using Masa.Blazor;

namespace Microsoft.Extensions.DependencyInjection;

public static class CascadingMasaBlazorServiceCollectionExtensions
{
    /// <summary>
    /// Adds cascading MasaBlazor to the <paramref name="serviceCollection"/>.
    /// </summary>
    /// <param name="serviceCollection"></param>
    /// <returns></returns>
    internal static IServiceCollection AddCascadingMasaBlazor(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<MasaBlazorProvider>();

        return serviceCollection.AddCascadingValue<MasaBlazor>(services =>
        {
            var masaBlazorProvider = services.GetRequiredService<MasaBlazorProvider>();
            return new MasaBlazorCascadingValueSource(masaBlazorProvider);
        });
    }

    private sealed class MasaBlazorCascadingValueSource : CascadingValueSource<MasaBlazor>, IDisposable
    {
        private readonly MasaBlazorProvider _masaBlazorProvider;

        public MasaBlazorCascadingValueSource(MasaBlazorProvider masaBlazorProvider)
            : base(masaBlazorProvider.MasaBlazor, isFixed: false)
        {
            _masaBlazorProvider = masaBlazorProvider;
            _masaBlazorProvider.MasaBlazorChanged += HandleMasaBlazorChanged;
        }

        private void HandleMasaBlazorChanged(MasaBlazor masaBlazor)
        {
            _ = NotifyChangedAsync(masaBlazor);
        }

        public void Dispose()
        {
            _masaBlazorProvider.MasaBlazorChanged -= HandleMasaBlazorChanged;
        }
    }
}
#endif
