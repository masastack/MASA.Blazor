namespace Masa.Docs.Core.Components;

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

    private const string POUND = "mdi-anchor";
    private const string OPEN_IN_NEW = "mdi-open-in-new";
    private const string PAGE_NEXT = "mdi-page-next";

    private string? _target;

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
        if (IsSamePage)
        {
            // TODO: remove the following lines when #40190 of aspnetcore resolved.
            // TODO: Blazor now does not support automatic scrolling of anchor points.
            // Check this when .NET 8 released.

            await Js.InvokeVoidAsync("setHash");
            //NavigationManager.ReplaceWithHash(Href);
            await Js.ScrollToElement(Href.Replace(".", "-").ToLower(), AppService.AppBarHeight);
        }
    }
}
