using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace Masa.Blazor.Doc.Components;

public partial class AppLink
{
    [Inject]
    public IJSRuntime Js { get; set; }

    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object> Attributes { get; set; }

    [Parameter]
    public RenderFragment ChildContent { get; set; }

    [Parameter]
    public bool HideIcon { get; set; }

    [Parameter]
    public string Href { get; set; }

    private const string POUND = "mdi-pound";
    private const string OPEN_IN_NEW = "mdi-open-in-new";
    private const string PAGE_NEXT = "mdi-page-next";

    private string Target { get; set; }

    private string ComputedHref
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

    private string Icon
    {
        get
        {
            if (IsSamePage) return POUND;
            if (IsExternal) return OPEN_IN_NEW;
            if (Href != null) return PAGE_NEXT;
            return null;
        }
    }

    private bool IsExternal => Href.StartsWith("http") || Href.StartsWith("mailto");

    private bool IsSamePage => !IsExternal && Href.StartsWith("#");

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (IsExternal)
        {
            Target = "_blank";
        }
    }

    private async Task OnClick(MouseEventArgs args)
    {
        // if TODO:[issue](https://github.com/dotnet/aspnetcore/issues/36605) fixed, remove @onclick
        if (IsSamePage)
        {
            await Js.ScrollToHash(Href);
        }
    }
}