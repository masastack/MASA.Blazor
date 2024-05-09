namespace Masa.Blazor.Presets.PageStack;

public partial class PPageStackItem : MasaComponentBase
{
    [Parameter] public bool Active { get; set; }

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public bool CanRender { get; set; }

    [Parameter] public EventCallback OnGoBack { get; set; }

    [Parameter] public Action<string>? SelectorCaptureAction { get; set; }

    [Parameter] public int Index { get; set; }

    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {
            SelectorCaptureAction?.Invoke($"[page-stack-id=\"{Index}\"]>.m-dialog");
        }
    }

    internal RenderFragment<PageStackGoBackContext>? AppBarContent { get; set; }
    internal RenderFragment<PageStackGoBackContext>? GoBackContent { get; set; }
    internal RenderFragment<Dictionary<string, object?>>? ImageContent { get; set; }
    internal RenderFragment? ExtensionContent { get; set; }
    internal int ExtensionHeight { get; set; } = 48;
    internal string? AppBarColor { get; set; }
    internal string? AppBarClass { get; set; }
    internal bool AppBarFlat { get; set; }
    internal int AppBarHeight { get; set; }
    internal bool AppBarDense { get; set; }
    internal bool AppBarShort { get; set; }
    internal string? AppBarStyle { get; set; }
    internal string? AppBarTitle { get; set; }
    internal string? AppBarImage { get; set; }
    internal bool CenterTitle { get; set; }
    internal bool ElevateOnScroll { get; set; }
    internal bool ShrinkOnScroll { get; set; }
    internal bool AppBarLight { get; set; }
    internal bool AppBarDark { get; set; }

    private int ComputedBarHeight
    {
        get
        {
            var height = ComputeBarHeight();

            if (ExtensionContent is not null)
            {
                height += ExtensionHeight;
            }

            return height;

            int ComputeBarHeight()
            {
                if (AppBarHeight != 0)
                {
                    return AppBarHeight;
                }

                if (AppBarDense)
                {
                    return 48;
                }

                if (AppBarShort)
                {
                    return 56;
                }

                return 64;
            }
        }
    }

    internal RenderFragment? ActionContent { get; set; }

    private Block _block = new("m-page-stack-item");

    protected override IEnumerable<string> BuildComponentClass()
    {
        return _block
            .Modifier(CenterTitle)
            .GenerateCssClasses();
    }

    private async Task HandleOnGoBack()
    {
        await OnGoBack.InvokeAsync();
    }

    internal void Render()
    {
        StateHasChanged();
    }
}