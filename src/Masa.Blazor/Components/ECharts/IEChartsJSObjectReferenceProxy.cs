namespace Masa.Blazor;

public interface IEChartsJSObjectReferenceProxy : IJSObjectReference
{
    ValueTask SetOptionAsync(object option, bool notMerge = false, bool lazyUpdate = false);

    ValueTask SetJsonOptionAsync(string option, bool notMerge = false, bool lazyUpdate = false);

    ValueTask ResizeAsync();

    ValueTask ResizeAsync(double width, double height);

    ValueTask DisposeEChartsAsync();
}
