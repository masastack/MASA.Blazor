namespace Masa.Blazor
{
    public interface IFillable
    {
        public string? FillColor { get; set; }

        public float FillOpacity { get; set; }
    }
}