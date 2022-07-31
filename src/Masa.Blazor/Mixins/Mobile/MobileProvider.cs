namespace Masa.Blazor;

public class MobileProvider : IMobile
{
    public MobileProvider(IMobile instance)
    {
        MasaBlazor = instance.MasaBlazor;
        MobileBreakpoint = instance.MobileBreakpoint;
    }

    public MasaBlazor MasaBlazor { get; }

    public OneOf<Breakpoints, double> MobileBreakpoint { get; }

    public bool IsMobile
    {
        get
        {
            var (width, mobile, name, mobileBreakpoint) = MasaBlazor.Breakpoint;

            if (mobileBreakpoint.Equals(this.MobileBreakpoint))
            {
                return mobile;
            }

            if (this.MobileBreakpoint.IsT1)
            {
                return width < this.MobileBreakpoint.AsT1;
            }

            return name == this.MobileBreakpoint.AsT0;
        }
    }
}
