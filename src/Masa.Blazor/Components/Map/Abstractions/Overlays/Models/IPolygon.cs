namespace Masa.Blazor
{
    public interface IPolygon
    {
        public IEnumerable<GeoPoint> Points { get; set; }
    }
}