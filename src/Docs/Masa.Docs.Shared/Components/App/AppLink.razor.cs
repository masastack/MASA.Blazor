using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace Masa.Docs.Shared.Components;

public partial class AppLink
{
    [Inject]
    public IJSRuntime Js { get; set; } = null!;

    [Parameter]
    public string Href { get; set; } = string.Empty;

    [Parameter]
    [EditorRequired]
    public string Content { get; set; } = null!;

    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object> Attributes { get; set; } = new();

    private const string POUND = "mdi-pound";
    private const string OPEN_IN_NEW = "mdi-open-in-new";
    private const string PAGE_NEXT = "mdi-page-next";

    private string? _target;

    private string? ComputedHref
    {
        get
        {
            if (IsSamePage)
            {
                // if TODO:[issue](https://github.com/dotnet/aspnetcore/issues/36605) fixed, use 
                // var relativePath = (new Uri(NavigationManager.Uri)).AbsolutePath;
                // return relativePath + Href;
                // else,
                return null;
            }

            return Href;
        }
    }

    private string? Icon
    {
        get
        {
            if (IsSamePage) return POUND;
            if (IsExternal) return OPEN_IN_NEW;
            if (!string.IsNullOrWhiteSpace(Href)) return PAGE_NEXT;
            return null;
        }
    }

    private bool IsExternal => Href.StartsWith("http") || Href.StartsWith("mailto");

    private bool IsSamePage => !IsExternal && Href.StartsWith("#");

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        await base.SetParametersAsync(parameters);

        ArgumentNullException.ThrowIfNull(Href);
        ArgumentNullException.ThrowIfNull(Content);
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (IsExternal)
        {
            _target = "_blank";
        }
    }

    private async Task OnClick(MouseEventArgs args)
    {
        // if TODO:[issue](https://github.com/dotnet/aspnetcore/issues/36605) fixed, remove @onclick
        if (IsSamePage)
        {
            // await Js.ScrollToHash(Href);
        }
    }
}
