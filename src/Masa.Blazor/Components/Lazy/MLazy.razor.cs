using StyleBuilder = Masa.Blazor.Core.StyleBuilder;

namespace Masa.Blazor;

public partial class MLazy
{
    [Inject] private IntersectJSModule IntersectJSModule { get; set; } = null!;

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public StringNumber? Height { get; set; }

    [Parameter] public StringNumber? MaxHeight { get; set; }

    [Parameter] public StringNumber? MaxWidth { get; set; }

    [Parameter] public StringNumber? MinHeight { get; set; }

    [Parameter] public StringNumber? MinWidth { get; set; }

    [Parameter] public StringNumber? Width { get; set; }

    [Parameter] [MasaApiParameter("div")] public string Tag { get; set; } = "div";

    [Parameter, MasaApiParameter(DefaultTransition)]
    public string Transition { get; set; } = DefaultTransition;

    /// <summary>
    /// The init options for IntersectionObserver.
    /// As soon as the component is created, the options cannot be changed.
    /// </summary>
    [Parameter] public IntersectionObserverInit? Options { get; set; }

    [Parameter] public EventCallback<IntersectEventArgs> OnIntersect { get; set; }

    private const string DefaultTransition = "fade-transition";

    private static Block _block = new("m-lazy");

    private bool _isActive;

    protected override IEnumerable<string> BuildComponentStyle()
    {
        return StyleBuilder.Create()
            .AddHeight(Height)
            .AddMaxHeight(MaxHeight)
            .AddMaxWidth(MaxWidth)
            .AddMinHeight(MinHeight)
            .AddMinWidth(MinWidth)
            .AddWidth(Width)
            .GenerateCssStyles();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            var handleReference = DotNetObjectReference.Create(new IntersectInvoker(OnIntersectAsync));
            await IntersectJSModule.ObserverAsync(Ref, handleReference, Options);
        }
    }

    private async Task OnIntersectAsync(IntersectEventArgs args)
    {
        await OnIntersect.InvokeAsync(args);

        if (_isActive)
        {
            return;
        }

        _isActive = args.IsIntersecting;
        await InvokeStateHasChangedAsync();
    }

    protected override ValueTask DisposeAsyncCore()
    {
        return IntersectJSModule.UnobserveAsync(Ref);
    }
}
