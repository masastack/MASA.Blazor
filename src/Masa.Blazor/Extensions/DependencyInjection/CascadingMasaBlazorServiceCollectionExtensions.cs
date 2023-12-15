using Masa.Blazor;

namespace Microsoft.Extensions.DependencyInjection;

public static class CascadingMasaBlazorServiceCollectionExtensions
{
    /// <summary>
    /// Adds cascading MasaBlazor to the <paramref name="serviceCollection"/>.
    /// </summary>
    /// <param name="serviceCollection"></param>
    /// <returns></returns>
    internal static void AddCascadingMasaBlazor(this IServiceCollection serviceCollection)
    {
#if NET8_0_OR_GREATER
        serviceCollection.AddScoped<MasaBlazorProvider>();

        serviceCollection.AddCascadingValue<MasaBlazorState>(services =>
        {
            var masaBlazorProvider = services.GetRequiredService<MasaBlazorProvider>();
            return new MasaBlazorCascadingValueSource(masaBlazorProvider);
        });
#endif
    }

#if NET8_0_OR_GREATER
    private sealed class MasaBlazorCascadingValueSource : CascadingValueSource<MasaBlazorState>, IDisposable
    {
        private readonly MasaBlazorProvider _masaBlazorProvider;

        public MasaBlazorCascadingValueSource(MasaBlazorProvider masaBlazorProvider)
            : base(masaBlazorProvider.State, isFixed: false)
        {
            _masaBlazorProvider = masaBlazorProvider;
            _masaBlazorProvider.MasaBlazorChanged += HandleMasaBlazorChanged;
        }

        private void HandleMasaBlazorChanged(MasaBlazorState state)
        {
            _ = NotifyChangedAsync(state);
        }

        public void Dispose()
        {
            _masaBlazorProvider.MasaBlazorChanged -= HandleMasaBlazorChanged;
        }
    }
#endif
}
