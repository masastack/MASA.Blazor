namespace Masa.Blazor
{
    public interface ICircle
    {
        public GeoPoint Center { get; set; }

        public float Radius { get; set; }
    }
}