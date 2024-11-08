using Masa.Blazor.Presets.Drawer;

namespace Masa.Blazor.Presets.PageStack;

public partial class PPageStackItem : MasaComponentBase
{
    [Inject] private PageStackNavController NavController { get; set; } = null!;

    [Parameter] [EditorRequired] public StackPageData Data { get; set; } = null!;

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public bool CanRender { get; set; }

    [Parameter] public EventCallback OnGoBack { get; set; }

    private TouchJSObjectResult? _touchJSObjectResult;

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

    private bool _hasRendered;
    private MDialog? dialog;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            // await NextTickIf(UseTouchAsync, () => dialog?.ContentRef.Id is null);

            // await Retry(async () =>
            // {
            //     Console.Out.WriteLine("dialog.ContentRef.Id: " + dialog?.ContentRef.Id);
            //     await UseTouchAsync();
            // }, () => dialog?.ContentRef.Context is null);

            int retryTimes = 10;
            while (dialog?.ContentRef.Context is null && retryTimes > 0)
            {
                await Task.Delay(100);
                retryTimes--;
            }
        }
    }

    private async Task UseTouchAsync()
    {
        var touch = new Touch(Js, OnTouchMove, OnTouchEnd);
        _touchJSObjectResult = await touch.UseTouchAsync(dialog!.ContentRef, GetTouchState());
    }

    private TouchState GetTouchState()
    {
        return new TouchState(Data.Stacked, "right");
    }

    private void OnTouchMove(bool isDragging, double dragProgress)
    {
    }

    private void OnTouchEnd(bool isActive)
    {
    }

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

        _touchJSObjectResult?.Un();

        return base.DisposeAsyncCore();
    }
}