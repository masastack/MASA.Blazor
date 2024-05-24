namespace BlazorComponent
{
    public class MenuableDimensions
    {
        public MenuablePosition Activator { get; set; } = new();
        public MenuablePosition Content { get; set; } = new();
        public double RelativeYOffset { get; set; }
        public double OffsetParentLeft { get; set; }
    }
}
