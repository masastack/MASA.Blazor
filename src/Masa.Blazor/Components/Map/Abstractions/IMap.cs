namespace Masa.Blazor
{
    public interface IMap<TOverlay>
    {
        public StringNumber Width { get; set; }

        public StringNumber Height { get; set; }

        public float Zoom { get; set; }

        public GeoPoint Center { get; set; }

        public bool EnableScrollWheelZoom { get; set; }

        public Task<IJSObjectReference> AddOverlayAsync(TOverlay overlay);

        public Task RemoveOverlayAsync(TOverlay overlay);

        public Task ClearOverlaysAsync();

    }
}
