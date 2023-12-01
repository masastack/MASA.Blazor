using System.ComponentModel;
using Microsoft.AspNetCore.Components.Routing;

namespace Masa.Blazor;

public class MasaBlazorSsrState
{
    public string? Culture { get; set; }

    public PassiveState Passive { get; set; }

    public bool? Rtl { get; set; }

    public bool? Dark { get; set; }

    public class PassiveState
    {
        public ApplicationState Application { get; set; }

        public class ApplicationState
        {
            public double Bar { get; set; }

            public double Top { get; set; }

            public double Right { get; set; }

            public double Bottom { get; set; }

            public double Left { get; set; }

            public double Footer { get; set; }

            public double InsetFooter { get; set; }
        }
    }
}

public class MSsrStateProvider : ComponentBase, IDisposable
{
    [Inject] private IJSRuntime JSRuntime { get; set; } = null!;

    [Inject] private MasaBlazorProvider MasaBlazorProvider { get; set; } = null!;

    [CascadingParameter] private MasaBlazorState MasaBlazorState { get; set; } = null!;

    private CancellationTokenSource? _cancellationTokenSource;

    protected override void OnInitialized()
    {
        MasaBlazorState.Instance.Application.PropertyChanged += OnApplicationPropertyChanged;
        MasaBlazorState.I18n.CultureChanged += OnCultureChanged;
    }

    private async void OnCultureChanged(object? sender, EventArgs e)
    {
        await SaveStateAsync();
    }

    private async void OnApplicationPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        // MMain is not interactive in SSR, so we need save the main element's position
        // and let the client side to restore it after navigation.
        await SaveStateAsync();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            var state = await JSRuntime.InvokeAsync<MasaBlazorSsrState?>(JsInteropConstants.SsrGetState);
            if (state != null)
            {
                if (state.Dark.HasValue && state.Dark != MasaBlazorState.Instance.Theme.Dark)
                {
                    MasaBlazorProvider.ToggleTheme();
                }

                if (state.Rtl.HasValue && state.Rtl != MasaBlazorState.Instance.RTL)
                {
                    MasaBlazorProvider.ToggleRtl();
                }

                if (!string.IsNullOrWhiteSpace(state.Culture) &&
                    state.Culture.Equals(MasaBlazorState.I18n.Culture.Name, StringComparison.OrdinalIgnoreCase))
                {
                    MasaBlazorProvider.SetCulture(new(state.Culture));
                }
            }

            await SaveStateAsync();

            await InitBreakpointAsync();

            // TODO: Window.AddResizeEventListenerAsync(); in MApp
        }
    }

    private Task InitBreakpointAsync() => MasaBlazorState.Instance.Breakpoint.InitAsync(JSRuntime);

    private async Task SaveStateAsync()
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = new();
        try
        {
            await Task.Delay(300, _cancellationTokenSource.Token);
            Console.Out.WriteLine("[MSsrAppProvider] SaveStateAsync");
            _ = JSRuntime.InvokeVoidAsync(JsInteropConstants.SsrUpdatePassiveState,  new MasaBlazorSsrState.PassiveState()
            {
                Application = new MasaBlazorSsrState.PassiveState.ApplicationState()
                {
                    Bar = MasaBlazorState.Instance.Application.Bar,
                    Top = MasaBlazorState.Instance.Application.Top,
                    Right = MasaBlazorState.Instance.Application.Right,
                    Bottom = MasaBlazorState.Instance.Application.Bottom,
                    Left = MasaBlazorState.Instance.Application.Left,
                    Footer = MasaBlazorState.Instance.Application.Footer,
                    InsetFooter = MasaBlazorState.Instance.Application.InsetFooter
                }
            });
        }
        catch (TaskCanceledException)
        {
            // ignored
        }
    }

    public void Dispose()
    {
        MasaBlazorState.Instance.Application.PropertyChanged -= OnApplicationPropertyChanged;
        MasaBlazorState.I18n.CultureChanged -= OnCultureChanged;
    }
}
