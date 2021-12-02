using Microsoft.AspNetCore.Components;

namespace MASA.Blazor.Doc.Components;

public partial class ALink
{
    private const string POUND = "mdi-pound";
    private const string OPEN_IN_NEW = "mdi-open-in-new";
    private const string PAGE_NEXT = "mdi-page-next";

    protected string Target { get; set; }

    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object> Attributes { get; set; }

    [Parameter]
    public RenderFragment ChildContent { get; set; }

    [Parameter]
    public string Href { get; set; }

    public string Icon
    {
        get
        {
            if (IsSamePage) return POUND;
            if (IsExternal) return OPEN_IN_NEW;
            if (Href != null) return PAGE_NEXT;
            return null;
        }
    }

    public bool IsExternal => Href.StartsWith("http") || Href.StartsWith("mailto");

    public bool IsSamePage => !IsExternal && Href.StartsWith("#");

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (IsExternal)
        {
            Target = "_blank";
        }
    }
}