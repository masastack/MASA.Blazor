namespace Masa.Blazor
{
    public interface IBaiduMapJSObjectReferenceProxy: IJSObjectReference
    {
        Func<Task> NotifyZoomChangedInJS { get; set; }

        Func<Task> NotifyCenterChangedInJS { get; set; }

        Func<Task> NotifyMapTypeChangedInJS { get; set; }
    }
}
