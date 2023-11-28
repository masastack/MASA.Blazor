using System.ComponentModel;
using Microsoft.AspNetCore.Components.Routing;

namespace Masa.Blazor;

public class MasaBlazorState
{
    public MainPadding Main { get; set; }

    public bool Rtl { get; set; }

    public bool Dark { get; set; }

    public class MainPadding
    {
        public double Top { get; set; }

        public double Right { get; set; }

        public double Bottom { get; set; }

        public double Left { get; set; }
    }
}

public class MSsrAppProvider : ComponentBase, IDisposable
{
    [Inject] private IJSRuntime JSRuntime { get; set; } = null!;

    [CascadingParameter] private MasaBlazor MasaBlazor { get; set; } = null!;

    private CancellationTokenSource? _cancellationTokenSource;

    protected override void OnInitialized()
    {
        MasaBlazor.Application.PropertyChanged += OnApplicationPropertyChanged;
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
            await SaveStateAsync();

            await InitBreakpointAsync();

            // TODO: Window.AddResizeEventListenerAsync(); in MApp
        }
    }

    private Task InitBreakpointAsync() => MasaBlazor.Breakpoint.InitAsync(JSRuntime);

    private async Task SaveStateAsync()
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = new();
        try
        {
            await Task.Delay(300, _cancellationTokenSource.Token);
            Console.Out.WriteLine("[MSsrAppProvider] SaveStateAsync");
            _ = JSRuntime.InvokeVoidAsync(JsInteropConstants.SsrSaveMain, new MasaBlazorState()
            {
                Dark = MasaBlazor.Theme.Dark,
                Main = new MasaBlazorState.MainPadding()
                {
                    Top = MasaBlazor.Application.Top,
                    Right = MasaBlazor.Application.Right,
                    Bottom = MasaBlazor.Application.Bottom,
                    Left = MasaBlazor.Application.Left,
                },
                Rtl = MasaBlazor.RTL
            });
        }
        catch (TaskCanceledException)
        {
            // ignored
        }
    }

    public void Dispose()
    {
        MasaBlazor.Application.PropertyChanged -= OnApplicationPropertyChanged;
    }
}
