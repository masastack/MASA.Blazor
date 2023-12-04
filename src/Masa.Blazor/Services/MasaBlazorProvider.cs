// TODO: Do I should put the class in the namespace Masa.Blazor.Services?
// Because the class is not recommended to be used by the user.

namespace Masa.Blazor;

public class MasaBlazorState
{
    public MasaBlazor Instance { get; set; }

    public I18n I18n { get; set; }
}

public class MasaBlazorProvider
{
    private readonly IJSRuntime _jsRuntime;

    public MasaBlazorProvider(MasaBlazor masaBlazor, I18n i18n, IJSRuntime jsRuntime)
    {
        State = new MasaBlazorState()
        {
            Instance = masaBlazor,
            I18n = i18n
        };

        _jsRuntime = jsRuntime;
    }

    public MasaBlazorState State { get; init; }

    public MasaBlazor MasaBlazor => State.Instance;

    public I18n I18n => State.I18n;

    internal event StateChangedHandler? MasaBlazorChanged;

    public void NotifyStateChanged(MasaBlazor masaBlazor)
    {
        State.Instance = masaBlazor;
        MasaBlazorChanged?.Invoke(State);
    }

    public void NotifyStateChanged(I18n i18n)
    {
        State.I18n = i18n;
        MasaBlazorChanged?.Invoke(State);
    }

    public void NotifyStateChanged() => MasaBlazorChanged?.Invoke(State);

    public void ToggleTheme()
    {
        MasaBlazor.ToggleTheme();
        NotifyStateChanged(MasaBlazor);
        _ = _jsRuntime.InvokeVoidAsync(JsInteropConstants.SsrSetTheme, MasaBlazor.Theme.Dark);
    }

    public void ToggleRtl()
    {
        MasaBlazor.RTL = !MasaBlazor.RTL;
        NotifyStateChanged(MasaBlazor);
        _ = _jsRuntime.InvokeVoidAsync(JsInteropConstants.SsrSetRtl, MasaBlazor.RTL);
    }

    public void SetCulture(CultureInfo culture)
    {
        I18n.SetCulture(culture);
        NotifyStateChanged(I18n);
        _ = _jsRuntime.InvokeVoidAsync(JsInteropConstants.SsrSetCulture, culture.Name);
    }
}

public delegate void StateChangedHandler(MasaBlazorState state);
