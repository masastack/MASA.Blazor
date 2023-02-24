namespace Masa.Blazor
{
    public interface IMap<TOverlay>
    {
        public StringNumber Width { get; set; }

        public StringNumber Height { get; set; }

        public float Zoom { get; set; }

        public GeoPoint Center { get; set; }

        public bool EnableScrollWheelZoom { get; set; }

        public ValueTask AddOverlayAsync(TOverlay overlay);

        public ValueTask RemoveOverlayAsync(TOverlay overlay);

        public ValueTask ClearOverlaysAsync();

        public ValueTask<bool> ContainsOverlayAsync(TOverlay overlay);

    }
}