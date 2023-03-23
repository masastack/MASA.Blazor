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
        if (eventName == "maptypechange") await _owner.HandleOnMapTypeChanged();
        if (eventName == "movestart") await _owner.OnMoveStart.InvokeAsync();
        if (eventName == "moving") await _owner.HandleOnMoving();
        if (eventName == "moveend") await _owner.OnMoveEnd.InvokeAsync();
        if (eventName == "zoomstart") await _owner.OnZoomStart.InvokeAsync();
        if (eventName == "zoomend") await _owner.HandleOnZoomEnd();
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
        events.Add("moving");
        if (_owner.OnMoveEnd.HasDelegate) events.Add("moveend");
        if (_owner.OnZoomStart.HasDelegate) events.Add("zoomstart");
        events.Add("zoomend");
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

    public async ValueTask AddOverlayAsync(BaiduOverlayBase overlay)
    {
        if (overlay is null)
            return;

        if (overlay.OverlayJSObjectRef is null)
            overlay.OverlayJSObjectRef = overlay switch
            {
                MBaiduCircle circle => await InvokeAsync<IJSObjectReference>("addCircle", circle),
                MBaiduMarker marker => await InvokeAsync<IJSObjectReference>("addMarker", marker),
                MBaiduLabel label => await InvokeAsync<IJSObjectReference>("addLabel", label),
                MBaiduPolyline polyline => await InvokeAsync<IJSObjectReference>("addPolyline", polyline),
                MBaiduPolygon polygon => await InvokeAsync<IJSObjectReference>("addPolygon", polygon),
                _ => null
            };

        else
            await InvokeVoidAsync("addOverlay", overlay.OverlayJSObjectRef);
    }

    protected override async ValueTask DisposeAsync(bool disposing)
    {
        _selfReference.Dispose();
        await InvokeVoidAsync("destroyMap");

        await base.DisposeAsync(disposing);
    }

}