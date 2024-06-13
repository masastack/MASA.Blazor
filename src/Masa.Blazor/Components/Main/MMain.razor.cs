using System.ComponentModel;
using StyleBuilder = Masa.Blazor.Core.StyleBuilder;

namespace Masa.Blazor;

public partial class MMain : MasaComponentBase
{
    [Inject] public MasaBlazor MasaBlazor { get; set; } = null!;

    [Parameter] public RenderFragment? ChildContent { get; set; }

    private static string[] _applicationProperties =
    {
        "Top", "Bar", "Right", "Footer", "InsetFooter", "Bottom", "Left"
    };

    private bool _sized;

    private bool IsSsr => MasaBlazor.IsSsr;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        MasaBlazor.Application.PropertyChanged += OnApplicationPropertyChanged;

#if NET8_0_OR_GREATER
        if (IsSsr)
        {
            Attributes["data-booted"] = "true";
        }
#endif
    }

    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);

        if (firstRender)
        {
            Attributes["data-booted"] = "true";
        }
    }

    private void OnApplicationPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (_applicationProperties.Contains(e.PropertyName))
        {
            _sized = true;
            InvokeStateHasChanged();
        }
    }

    private static Block _block = new("m-main");

    protected override IEnumerable<string> BuildComponentClass()
    {
        yield return _block.Name;
        if (_sized)
        {
            yield return "app--sized";
        }
    }

    protected override IEnumerable<string> BuildComponentStyle()
    {
        return StyleBuilder.Create()
            .AddIf("padding-top", $"{MasaBlazor.Application.Top + MasaBlazor.Application.Bar}px", !IsSsr)
            .AddIf("padding-right", $"{MasaBlazor.Application.Right}px", !IsSsr)
            .AddIf("padding-bottom",
                $"{MasaBlazor.Application.Footer + MasaBlazor.Application.InsetFooter + MasaBlazor.Application.Bottom}px",
                !IsSsr)
            .AddIf("padding-left", $"{MasaBlazor.Application.Left}px", !IsSsr)
            .GenerateCssStyles();
    }

    protected override ValueTask DisposeAsyncCore()
    {
        MasaBlazor.Application.PropertyChanged -= OnApplicationPropertyChanged;

        return base.DisposeAsyncCore();
    }
}