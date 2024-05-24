namespace BlazorComponent
{
    public class MenuablePosition : BoundingClientRect
    {
        public double OffsetTop { get; set; }
        public double OffsetLeft { get; set; }
        public double ScrollHeight { get; set; }

        public MenuablePosition()
        {
        }

        public MenuablePosition(BoundingClientRect rect)
        {
            Bottom = rect?.Bottom ?? 0;
            Left = rect?.Left ?? 0;
            Height = rect?.Height ?? 0;
            Right = rect?.Right ?? 0;
            Top = rect?.Top ?? 0;
            Width = rect?.Width ?? 0;
            X = rect?.X ?? 0;
            Y = rect?.Y ?? 0;
        }
    }
}
