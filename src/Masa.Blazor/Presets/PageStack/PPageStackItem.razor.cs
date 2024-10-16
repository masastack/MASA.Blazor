namespace Masa.Blazor.Presets.PageStack;

public partial class PPageStackItem : MasaComponentBase
{
    [Inject] private PageStackNavController NavController { get; set; } = null!;

    [Parameter] [EditorRequired] public StackPageData Data { get; set; } = null!;

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public bool CanRender { get; set; }

    [Parameter] public EventCallback OnGoBack { get; set; }

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

    private static Block _block = new("m-page-stack-item");
    private ModifierBuilder _modifierBuilder = _block.CreateModifierBuilder();

    protected override IEnumerable<string> BuildComponentClass()
    {
        yield return _modifierBuilder.Add(CenterTitle).Build();
    }

    private async Task HandleOnGoBack()
    {
        await OnGoBack.InvokeAsync();
    }

    internal void Render()
    {
        StateHasChanged();
    }

    protected override ValueTask DisposeAsyncCore()
    {
        NavController.NotifyPageClosed(Data.AbsolutePath);

        return base.DisposeAsyncCore();
    }
}