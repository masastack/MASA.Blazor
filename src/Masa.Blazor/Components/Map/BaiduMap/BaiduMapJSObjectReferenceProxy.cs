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

        _ = jsObjectReference.InvokeVoidAsync("setDotNetObjectReference", _selfReference, GetInvokeEvents());
    }

    public Func<Task> NotifyZoomChangedInJS { get; set; } = null;

    public Func<Task> NotifyCenterChangedInJS { get; set; } = null;

    public Func<Task> NotifyMapTypeChangedInJS { get; set; } = null;

    [JSInvokable]
    public async Task OnEvent(string eventName, BaiduMapEventArgs? eventParams)
    {
        if (eventName == "click") await _owner.OnClick.InvokeAsync(eventParams);
        if (eventName == "dblclick") await _owner.OnDoubleClick.InvokeAsync(eventParams);
        if (eventName == "rightclick") await _owner.OnRightClick.InvokeAsync(eventParams);
        if (eventName == "rightdblclick") await _owner.OnRightDoubleClick.InvokeAsync(eventParams);
        if (eventName == "mousemove") await _owner.OnMouseMove.InvokeAsync(eventParams);
        if (eventName == "mouseover") await _owner.OnMouseOver.InvokeAsync();
        if (eventName == "mouseout") await _owner.OnMouseOut.InvokeAsync();
        if (eventName == "maptypechange")
        {
            if (NotifyMapTypeChangedInJS is null)
                return;

            await NotifyMapTypeChangedInJS();
            await _owner.OnMapTypeChanged.InvokeAsync();
        }
        if (eventName == "movestart") await _owner.OnMoveStart.InvokeAsync();
        if (eventName == "moving")
        {
            if (NotifyCenterChangedInJS is null)
                return;

            await NotifyCenterChangedInJS();
            await _owner.OnMoving.InvokeAsync();
        }
        if (eventName == "moveend") await _owner.OnMoveEnd.InvokeAsync();
        if (eventName == "zoomstart") await _owner.OnZoomStart.InvokeAsync();
        if (eventName == "zoomend")
        {
            if (NotifyZoomChangedInJS is null)
                return;

            await NotifyZoomChangedInJS();
            await _owner.OnZoomEnd.InvokeAsync();
        }
        if (eventName == "addoverlay") await _owner.OnAddOverlay.InvokeAsync();
        if (eventName == "addcontrol") await _owner.OnAddControl.InvokeAsync();
        if (eventName == "removeoverlay") await _owner.OnRemoveOverlay.InvokeAsync();
        if (eventName == "removecontrol") await _owner.OnRemoveControl.InvokeAsync();
        if (eventName == "clearoverlays") await _owner.OnClearOverlays.InvokeAsync();
        if (eventName == "dragstart") await _owner.OnDragStart.InvokeAsync(eventParams);
        if (eventName == "dragging") await _owner.OnDragging.InvokeAsync(eventParams);
        if (eventName == "dragend") await _owner.OnDragEnd.InvokeAsync(eventParams);
        if (eventName == "resize") await _owner.OnResize.InvokeAsync();
    }

    private IEnumerable<string> GetInvokeEvents()
    {
        List<string> events = new();

        if (_owner.OnClick.HasDelegate) events.Add("click");
        if (_owner.OnDoubleClick.HasDelegate) events.Add("dblclick");
        if (_owner.OnRightClick.HasDelegate) events.Add("rightclick");
        if (_owner.OnRightDoubleClick.HasDelegate) events.Add("rightdblclick");
        if (_owner.OnMouseMove.HasDelegate) events.Add("mousemove");
        if (_owner.OnMouseOver.HasDelegate) events.Add("mouseover");
        if (_owner.OnMouseOut.HasDelegate) events.Add("mouseout");
        if (_owner.OnMapTypeChanged.HasDelegate) events.Add("maptypechange");
        if (_owner.OnMoveStart.HasDelegate) events.Add("movestart");
        if (_owner.OnMoving.HasDelegate) events.Add("moving");
        if (_owner.OnMoveEnd.HasDelegate) events.Add("moveend");
        if (_owner.OnZoomStart.HasDelegate) events.Add("zoomstart");
        if (_owner.OnZoomEnd.HasDelegate) events.Add("zoomend");
        if (_owner.OnAddOverlay.HasDelegate) events.Add("addoverlay");
        if (_owner.OnAddControl.HasDelegate) events.Add("addcontrol");
        if (_owner.OnRemoveOverlay.HasDelegate) events.Add("removeoverlay");
        if (_owner.OnRemoveControl.HasDelegate) events.Add("removecontrol");
        if (_owner.OnClearOverlays.HasDelegate) events.Add("clearoverlays");
        if (_owner.OnDragStart.HasDelegate) events.Add("dragstart");
        if (_owner.OnDragging.HasDelegate) events.Add("dragging");
        if (_owner.OnDragEnd.HasDelegate) events.Add("dragend");
        if (_owner.OnResize.HasDelegate) events.Add("resize");

        return events;
    }

    protected override async ValueTask DisposeAsync(bool disposing)
    {
        _selfReference.Dispose();

        await base.DisposeAsync(disposing);
    }
}
