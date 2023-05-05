namespace Masa.Blazor;

public class EChartsJSObjectReferenceProxy : JSObjectReferenceProxy, IEChartsJSObjectReferenceProxy
{
    private readonly IEChartsJsCallbacks _owner;
    private readonly DotNetObjectReference<EChartsJSObjectReferenceProxy> _selfReference;

    public EChartsJSObjectReferenceProxy(IJSObjectReference jsObjectReference, IEChartsJsCallbacks owner)
        : base(jsObjectReference)
    {
        _owner = owner;

        _selfReference = DotNetObjectReference.Create(this);

        jsObjectReference.InvokeVoidAsync("setDotNetObjectReference", _selfReference, GetInvokeEvents());
    }

    public async ValueTask SetOptionAsync(object option, bool notMerge = false, bool lazyUpdate = false)
        => await InvokeVoidAsync("setOption", option, notMerge, lazyUpdate);

   public async ValueTask SetJsonOptionAsync(string option, bool notMerge = false, bool lazyUpdate = false)
         => await InvokeVoidAsync("setJsonOption", option, notMerge, lazyUpdate);

    public async ValueTask ResizeAsync()
        => await InvokeVoidAsync("resize");

    public async ValueTask ResizeAsync(double width, double height)
        => await InvokeVoidAsync("resize", width, height);

    public async ValueTask DisposeEChartsAsync()
        => await InvokeVoidAsync("dispose");

    [JSInvokable("OnEvent")]
    public async Task OnClick(string eventName, EChartsEventArgs? eventParams)
    {
        if (eventName == "click")       await _owner.OnClick.InvokeAsync(eventParams);
        if (eventName == "dbclick")     await _owner.OnDoubleClick.InvokeAsync(eventParams);
        if (eventName == "mousedown")   await _owner.OnMouseDown.InvokeAsync(eventParams);
        if (eventName == "mousemove")   await _owner.OnMouseMove.InvokeAsync(eventParams);
        if (eventName == "mouseup")     await _owner.OnMouseUp.InvokeAsync(eventParams);
        if (eventName == "mouseover")   await _owner.OnMouseOver.InvokeAsync(eventParams);
        if (eventName == "mouseout")    await _owner.OnMouseOut.InvokeAsync(eventParams);
        if (eventName == "globalout")   await _owner.OnGlobalOut.InvokeAsync();
        if (eventName == "contextmenu") await _owner.OnContextMenu.InvokeAsync(eventParams);
    }

    private IEnumerable<string> GetInvokeEvents()
    {
        List<string> events = new();

        if (_owner.OnClick.HasDelegate)       events.Add("click");
        if (_owner.OnDoubleClick.HasDelegate) events.Add("dbclick");
        if (_owner.OnMouseDown.HasDelegate)   events.Add("mousedown");
        if (_owner.OnMouseMove.HasDelegate)   events.Add("mousemove");
        if (_owner.OnMouseUp.HasDelegate)     events.Add("mouseup");
        if (_owner.OnMouseOver.HasDelegate)   events.Add("mouseover");
        if (_owner.OnMouseOut.HasDelegate)    events.Add("mouseout");
        if (_owner.OnGlobalOut.HasDelegate)   events.Add("globalout");
        if (_owner.OnContextMenu.HasDelegate) events.Add("contextmenu");

        return events;
    }

    protected override async ValueTask DisposeAsync(bool disposing)
    {
        _selfReference.Dispose();

        await base.DisposeAsync(disposing);
    }
}
