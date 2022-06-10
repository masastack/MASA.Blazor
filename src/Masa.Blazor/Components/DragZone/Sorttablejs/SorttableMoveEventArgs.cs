namespace Masa.Blazor
{
    public class SorttableMoveEventArgs: SorttableEventArgs
    { 
        public RectModel Rect { get; set; }

        public MovedModel Moved { get; set; }

    }

    public class RectModel
    { 
        public double Top { get; set; }

        public double Left { get; set; }

        public double Bottom { get; set; }

        public double Right { get; set; }

        public double Width { get; set; }

        public double Height { get; set; }
    }

    public class MovedModel
    { 
        public double ClientX { get; set; }
        public double ClientY { get; set; }

        public double ScreenX { get; set; }
        public double ScreenY { get; set; }

        public double PageX { get; set; }
        public double PageY { get; set; }

        public double OffsetX { get; set; }
        public double OffsetY { get; set; }

        public int LayerX { get; set; }

        public int LayerY { get; set; }

    }
}
