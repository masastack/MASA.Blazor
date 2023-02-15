using System.Reflection;

namespace Masa.Blazor;

public class BaiduMapJSObjectReferenceProxy : JSObjectReferenceProxy, IBaiduMapJSObjectReferenceProxy
{
    private readonly IBaiduMapJsCallbacks _owner;
    private readonly DotNetObjectReference<BaiduMapJSObjectReferenceProxy> _selfReference;

    public BaiduMapJSObjectReferenceProxy(IJSObjectReference jsObjectReference, IBaiduMapJsCallbacks owner)
        : base(jsObjectReference)
    {
        _owner = owner;

        _selfReference = DotNetObjectReference.Create(this);

        //jsObjectReference.InvokeVoidAsync("setDotNetObjectReference", _selfReference, GetInvokeEvents());
    }



    //[JSInvokable("OnEvent")]
    //public async Task OnClick(string eventName, EChartsEventArgs? eventParams)
    //{
    //    if (eventName == "click") await _owner.OnClick.InvokeAsync(eventParams);
    //    if (eventName == "dbclick") await _owner.OnDoubleClick.InvokeAsync(eventParams);
    //    if (eventName == "mousedown") await _owner.OnMouseDown.InvokeAsync(eventParams);
    //    if (eventName == "mousemove") await _owner.OnMouseMove.InvokeAsync(eventParams);
    //    if (eventName == "mouseup") await _owner.OnMouseUp.InvokeAsync(eventParams);
    //    if (eventName == "mouseover") await _owner.OnMouseOver.InvokeAsync(eventParams);
    //    if (eventName == "mouseout") await _owner.OnMouseOut.InvokeAsync(eventParams);
    //    if (eventName == "globalout") await _owner.OnGlobalOut.InvokeAsync();
    //    if (eventName == "contextmenu") await _owner.OnContextMenu.InvokeAsync(eventParams);
    //}

    //private IEnumerable<string> GetInvokeEvents()
    //{
    //    List<string> events = new();

    //    if (_owner.OnClick.HasDelegate) events.Add("click");
    //    if (_owner.OnDoubleClick.HasDelegate) events.Add("dbclick");
    //    if (_owner.OnMouseDown.HasDelegate) events.Add("mousedown");
    //    if (_owner.OnMouseMove.HasDelegate) events.Add("mousemove");
    //    if (_owner.OnMouseUp.HasDelegate) events.Add("mouseup");
    //    if (_owner.OnMouseOver.HasDelegate) events.Add("mouseover");
    //    if (_owner.OnMouseOut.HasDelegate) events.Add("mouseout");
    //    if (_owner.OnGlobalOut.HasDelegate) events.Add("globalout");
    //    if (_owner.OnContextMenu.HasDelegate) events.Add("contextmenu");

    //    return events;
    //}

    protected override async ValueTask DisposeAsync(bool disposing)
    {
        _selfReference.Dispose();

        await base.DisposeAsync(disposing);
    }

    public async ValueTask AddOverlayAsync(MBaiduOverlay overlay)
    {
        if (overlay is null)
            return;

        await InvokeVoidAsync("addOverlay", overlay.OverlayRef);
    }

    public async ValueTask RemoveOverlayAsync(MBaiduOverlay overlay)
    {
        if (overlay is null)
            return;

        await InvokeVoidAsync("removeOverlay", overlay.OverlayRef);
    }

    public async ValueTask ClearOverlaysAsync()
        => await InvokeVoidAsync("clearOverlays");
}
