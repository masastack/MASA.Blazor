namespace Masa.Blazor;

public class MasaBlazorSsrState
{
    public string? Culture { get; set; }

    public PassiveState Passive { get; set; }

    public bool? Rtl { get; set; }

    public bool? Dark { get; set; }

    public class PassiveState
    {
        public ApplicationState Application { get; set; }

        public class ApplicationState
        {
            public double Bar { get; set; }

            public double Top { get; set; }

            public double Right { get; set; }

            public double Bottom { get; set; }

            public double Left { get; set; }

            public double Footer { get; set; }

            public double InsetFooter { get; set; }
        }
    }
}