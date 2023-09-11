namespace Masa.Blazor;

public partial class MLazy : IAsyncDisposable
{
    [Inject] private IntersectJSModule IntersectJSModule { get; set; } = null!;

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public StringNumber? MinHeight { get; set; }

    [Parameter, ApiDefaultValue(DefaultTransition)]
    public string Transition { get; set; } = DefaultTransition;

    /// <summary>
    /// The init options for IntersectionObserver.
    /// As soon as the component is created, the options cannot be changed.
    /// </summary>
    [Parameter] public IntersectionObserverInit? InitOptions { get; set; }

    [Parameter] public EventCallback<IntersectEventArgs> OnIntersect { get; set; }

    private const string DefaultTransition = "fade-transition";

    private bool _isActive;

    private Block Block => new("m-lazy");

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            var handleReference = DotNetObjectReference.Create(new IntersectInvoker(OnIntersectAsync));
            await IntersectJSModule.ObserverAsync(Ref, handleReference, InitOptions);
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

    public ValueTask DisposeAsync()
    {
        return IntersectJSModule.UnobserveAsync(Ref);
    }
}
