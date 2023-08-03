namespace Masa.Blazor
{
    public interface IStroke
    {
        public string? StrokeColor { get; set; }

        public float StrokeOpacity { get; set; }

        public float StrokeWeight { get; set; }

        public StrokeStyle StrokeStyle { get; set; }
    }

    public enum StrokeStyle
    {
        Solid = 0,
        Dashed = 1,
    }
}