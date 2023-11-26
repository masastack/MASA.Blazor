// TODO: Do I should put the class in the namespace Masa.Blazor.Services?
// Because the class is not recommended to be used by the user.

using System.Text.Json;

namespace Masa.Blazor;

public class MasaBlazorProvider
{
    private readonly IJSRuntime _jsRuntime;

    public MasaBlazorProvider(MasaBlazor masaBlazor, IJSRuntime jsRuntime)
    {
        MasaBlazor = masaBlazor;
        _jsRuntime = jsRuntime;
    }

    public MasaBlazor MasaBlazor { get; init; }

    internal event StateChangedHandler? MasaBlazorChanged;

    public void NotifyStateChanged(MasaBlazor masaBlazor)
    {
        Console.Out.WriteLine($"NotifyStateChanged ~~ Dark:{MasaBlazor.Theme.Dark} Left:{masaBlazor.Application.Left}");
        MasaBlazorChanged?.Invoke(masaBlazor);
    }
    
    public void NotifyStateChanged() => NotifyStateChanged(MasaBlazor);

    public void ToggleTheme()
    {
        MasaBlazor.ToggleTheme();
        NotifyStateChanged(MasaBlazor);
        _ = _jsRuntime.InvokeVoidAsync(JsInteropConstants.SsrSetTheme, MasaBlazor.Theme.Dark);
    }
}

public delegate void StateChangedHandler(MasaBlazor masaBlazor);
