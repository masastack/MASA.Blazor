using System.ComponentModel;

namespace Masa.Blazor;

public class MMain : BMain
{
    [Inject] public MasaBlazor MasaBlazor { get; set; } = null!;

    private static string[] s_applicationProperties =
    {
        "Top", "Bar", "Right", "Footer", "InsetFooter", "Bottom", "Left"
    };

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        MasaBlazor.Application.PropertyChanged += OnApplicationPropertyChanged;
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
        if (s_applicationProperties.Contains(e.PropertyName))
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
