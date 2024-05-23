using System.ComponentModel;

namespace Masa.Blazor;

public partial class MAppBarTitle : MasaComponentBase
{
    [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

    [CascadingParameter] private MAppBar? AppBar { get; set; }

    [Parameter] public RenderFragment? ChildContent { get; set; }

    private Block _block = new("m-app-bar-title");

    private double _contentWidth;
    private double _left;
    private double _width;

    private ElementReference _contentElement;
    private ElementReference _placeholderElement;

    private double _windowsWidth;

    private double AppBarScrollRatio => AppBar?.ScrollRatio ?? 0;

    protected override void OnInitialized()
    {
        base.OnInitialized();

        MasaBlazor.BreakpointChanged += MasaBlazorOnBreakpointChanged;
    }

    private async void MasaBlazorOnBreakpointChanged(object? sender, BreakpointChangedEventArgs e)
    {
        if (Math.Abs(_windowsWidth - MasaBlazor.Breakpoint.Width) > 0.01)
        {
            _windowsWidth = MasaBlazor.Breakpoint.Width;

            await UpdateDimensions();
            await InvokeAsync(StateHasChanged);
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            await UpdateDimensions();
            StateHasChanged();
        }
    }

    protected override IEnumerable<string> BuildComponentClass()
    {
        return _block.AddClass("m-toolbar__title").GenerateCssClasses();
    }

    private string? GetContentStyle()
    {
        if (_contentWidth == 0)
        {
            return null;
        }

        var min = _width;
        var max = _contentWidth;
        var ratio = EaseInOutCubic(Math.Min(1, AppBarScrollRatio * 1.5));

        StringBuilder stringBuilder = new();
        stringBuilder.Append($"width: {min + (max - min) * ratio}px; ");
        stringBuilder.Append($"visibility: {(AppBarScrollRatio > 0 ? "visible" : "hidden")};");
        return stringBuilder.ToString();

        // acceleration until halfway, then deceleration
        double EaseInOutCubic(double t) => t < 0.5 ? 4 * Math.Pow(t, 3) : (t - 1) * (2 * t - 2) * (2 * t - 2) + 1;
    }

    private async Task UpdateDimensions()
    {
        var dimensions =
            await Js.InvokeAsync<BoundingClientRect>(JsInteropConstants.GetBoundingClientRect, _placeholderElement);
        _width = dimensions.Width;
        _left = dimensions.Left;

        var element = await Js.InvokeAsync<BlazorComponent.Web.Element>(JsInteropConstants.GetDomInfo, _contentElement);
        _contentWidth = element.ScrollWidth;
    }

    protected override ValueTask DisposeAsyncCore()
    {
        MasaBlazor.BreakpointChanged -= MasaBlazorOnBreakpointChanged;
        return base.DisposeAsyncCore();
    }
}