using System.ComponentModel;

namespace Masa.Blazor;

public class MMain : BMain
{
    [Inject] public MasaBlazor MasaBlazor { get; set; } = null!;

    private bool _sized;

    private bool IsSsr => MasaBlazor.IsSsr;

    private static string[] s_applicationProperties =
    {
        "Top", "Bar", "Right", "Footer", "InsetFooter", "Bottom", "Left"
    };

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
        if (s_applicationProperties.Contains(e.PropertyName))
        {
            _sized = true;
            InvokeStateHasChanged();
        }
    }

    protected override void SetComponentCss()
    {
        CssProvider
            .UseBem("m-main", css => { css.AddIf("app--sized", () => _sized); }, style =>
            {
                if (!IsSsr)
                {
                    style
                        .Add($"padding-top:{MasaBlazor.Application.Top + MasaBlazor.Application.Bar}px")
                        .Add($"padding-right:{MasaBlazor.Application.Right}px")
                        .Add($"padding-bottom:{MasaBlazor.Application.Footer + MasaBlazor.Application.InsetFooter + MasaBlazor.Application.Bottom}px")
                        .Add($"padding-left:{MasaBlazor.Application.Left}px");
                }
            })
            .Element("wrap");
    }

    protected override ValueTask DisposeAsync(bool disposing)
    {
        MasaBlazor.Application.PropertyChanged -= OnApplicationPropertyChanged;

        return base.DisposeAsync(disposing);
    }
}
