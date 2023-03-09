namespace Masa.Blazor
{
    public interface IPolyline
    {
        public IEnumerable<GeoPoint> Points { get; set; }
    }
}