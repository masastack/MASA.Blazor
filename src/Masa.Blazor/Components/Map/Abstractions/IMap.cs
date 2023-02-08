namespace Masa.Blazor
{
    public interface IMap
    {
        public StringNumber Width { get; set; }

        public StringNumber Height { get; set; }

        public float Zoom { get; set; }

        public GeoPoint Center { get; set; }

        public bool EnableScrollWheelZoom { get; set; }

        public Task AddOverlayAsync<TOverlay>(TOverlay overlay) where TOverlay : IMapOverlay;

        public Task RemoveOverlayAsync<TOverlay>(TOverlay overlay) where TOverlay : IMapOverlay;

        public Task ClearOverlaysAsync();

    }
}
