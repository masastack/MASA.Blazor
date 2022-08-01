using OneOf;

namespace Masa.Blazor;

public interface IMobile
{
    OneOf<Breakpoints, double> MobileBreakpoint { get; }

    MasaBlazor MasaBlazor { get; }
}
