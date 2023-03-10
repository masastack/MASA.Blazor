namespace Masa.Blazor
{
    public interface IBaiduMapJsCallbacks
    {
        EventCallback<BaiduMapEventArgs> OnClick { get; }

        EventCallback<BaiduMapEventArgs> OnDoubleClick { get; }

        EventCallback<BaiduMapEventArgs> OnRightClick { get; }

        EventCallback<BaiduMapEventArgs> OnRightDoubleClick { get; }

        EventCallback OnMapTypeChanged { get; }

        EventCallback<BaiduMapEventArgs> OnMouseMove { get; }

        EventCallback OnMouseOver { get; }

        EventCallback OnMouseOut { get; }

        EventCallback OnMoveStart { get; }

        EventCallback OnMoving { get; }

        EventCallback OnMoveEnd { get; }

        EventCallback OnZoomStart { get; }

        EventCallback OnZoomEnd { get; }

        EventCallback OnAddOverlay { get; }

        EventCallback OnAddControl { get; }

        EventCallback OnRemoveOverlay { get; }

        EventCallback OnRemoveControl { get; }

        EventCallback OnClearOverlays { get; }

        EventCallback<BaiduMapEventArgs> OnDragStart { get; }

        EventCallback<BaiduMapEventArgs> OnDragging { get; }

        EventCallback<BaiduMapEventArgs> OnDragEnd { get; }

        EventCallback OnResize { get; }

        ValueTask HandleOnMapTypeChanged();

        ValueTask HandleOnMoving();

        ValueTask HandleOnZoomEnd();
    }
}