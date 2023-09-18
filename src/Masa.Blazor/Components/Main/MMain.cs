using System.ComponentModel;

namespace Masa.Blazor;

public class MMain : BMain
{
    [Inject]
    public MasaBlazor MasaBlazor { get; set; } = null!;

    private readonly string[] _applicationProperties = new string[]
    {
        "Top", "Bar", "Right", "Footer", "InsetFooter", "Bottom", "Left"
    };

    private bool _isRendered;

    /// <summary>
    /// Avoid an entry animation on page load.
    /// </summary>
    private bool IsBooted => _isRendered && (!MasaBlazor.Application.HasNavigationDrawer || MasaBlazor.Application.LeftRightCalculated);

    protected override void OnInitialized()
    {
        base.OnInitialized();

        MasaBlazor.Application.PropertyChanged += OnApplicationPropertyChanged;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            _isRendered = true;
            Attributes["data-booted"] = IsBooted ? "true" : null;
            StateHasChanged();
        }
    }

    private void OnApplicationPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (_applicationProperties.Contains(e.PropertyName))
        {
            Attributes["data-booted"] = IsBooted ? "true" : null;
            InvokeStateHasChanged();
        }
    }

    protected override void SetComponentClass()
    {
        CssProvider
            .Apply(cssBuilder => { cssBuilder.Add("m-main"); }, styleBuilder =>
            {
                styleBuilder
                    .Add($"padding-top:{MasaBlazor.Application.Top + MasaBlazor.Application.Bar}px")
                    .Add($"padding-right:{MasaBlazor.Application.Right}px")
                    .Add($"padding-bottom:{MasaBlazor.Application.Footer + MasaBlazor.Application.InsetFooter + MasaBlazor.Application.Bottom}px")
                    .Add($"padding-left:{MasaBlazor.Application.Left}px");
            })
            .Apply("wrap", cssBuilder => cssBuilder.Add("m-main__wrap"));
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);

        MasaBlazor.Application.PropertyChanged -= OnApplicationPropertyChanged;
    }
}
