namespace Masa.Blazor;

public interface IEChartsJsCallbacks
{
    EventCallback<EChartsEventArgs> OnClick { get; }

    EventCallback<EChartsEventArgs> OnDoubleClick { get; }

    EventCallback<EChartsEventArgs> OnMouseDown { get; }

    EventCallback<EChartsEventArgs> OnMouseMove { get; }

    EventCallback<EChartsEventArgs> OnMouseUp { get; }

    EventCallback<EChartsEventArgs> OnMouseOver { get; }

    EventCallback<EChartsEventArgs> OnMouseOut { get; }

    EventCallback OnGlobalOut { get; }

    EventCallback<EChartsEventArgs> OnContextMenu { get; }
}
