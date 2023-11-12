namespace Masa.Blazor;

public partial class MLazy : IAsyncDisposable
{
    [Inject] private IntersectJSModule IntersectJSModule { get; set; } = null!;

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public StringNumber? Height { get; set; }

    [Parameter] public StringNumber? MaxHeight { get; set; }

    [Parameter] public StringNumber? MaxWidth { get; set; }

    [Parameter] public StringNumber? MinHeight { get; set; }

    [Parameter] public StringNumber? MinWidth { get; set; }

    [Parameter] public StringNumber? Width { get; set; }

    [Parameter] [MassApiParameter("div")] public string Tag { get; set; } = "div";

    [Parameter, MassApiParameter(DefaultTransition)]
    public string Transition { get; set; } = DefaultTransition;

    /// <summary>
    /// The init options for IntersectionObserver.
    /// As soon as the component is created, the options cannot be changed.
    /// </summary>
    [Parameter] public IntersectionObserverInit? Options { get; set; }

    [Parameter] public EventCallback<IntersectEventArgs> OnIntersect { get; set; }

    private const string DefaultTransition = "fade-transition";

    private bool _isActive;

    protected override void SetComponentClass()
    {
        base.SetComponentClass();

        CssProvider
            .UseBem("m-lazy", styleAction: styleAction =>
            {
                styleAction.AddHeight(Height)
                           .AddMaxHeight(MaxHeight)
                           .AddMaxWidth(MaxWidth)
                           .AddMinHeight(MinHeight)
                           .AddMinWidth(MinWidth)
                           .AddWidth(Width);
            })
            .Element("wrapper");
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

    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        try
        {
            await IntersectJSModule.UnobserveAsync(Ref);
        }
        catch (Exception)
        {
            // ignored
        }
    }
}
