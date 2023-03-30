using System.ComponentModel;

namespace Masa.Blazor;

public class MMain : BMain
{
    [Inject]
    public MasaBlazor? MasaBlazor { get; set; }

    private readonly string[] _applicationProperties = new string[]
    {
        "Top", "Bar", "Right", "Footer", "InsetFooter", "Bottom", "Left"
    };

    /// <summary>
    /// Avoid an entry animation on page load.
    /// </summary>
    protected override bool IsBooted => MasaBlazor is not null && MasaBlazor.Application.LeftRightCalculated;

    protected override void OnInitialized()
    {
        base.OnInitialized();

        MasaBlazor!.Application.PropertyChanged += OnApplicationPropertyChanged;
    }

    private void OnApplicationPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (_applicationProperties.Contains(e.PropertyName))
        {
            InvokeStateHasChanged();
        }
    }

    protected override void SetComponentClass()
    {
        CssProvider
            .Apply(cssBuilder => { cssBuilder.Add("m-main"); }, styleBuilder =>
            {
                styleBuilder
                    .Add($"padding-top:{MasaBlazor!.Application.Top + MasaBlazor.Application.Bar}px")
                    .Add($"padding-right:{MasaBlazor.Application.Right}px")
                    .Add($"padding-bottom:{MasaBlazor.Application.Footer + MasaBlazor.Application.InsetFooter + MasaBlazor.Application.Bottom}px")
                    .Add($"padding-left:{MasaBlazor.Application.Left}px");
            })
            .Apply("wrap", cssBuilder => cssBuilder.Add("m-main__wrap"));

        Attributes.Add("data-booted", IsBooted);
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);

        MasaBlazor.Application.PropertyChanged -= OnApplicationPropertyChanged;
    }
}
