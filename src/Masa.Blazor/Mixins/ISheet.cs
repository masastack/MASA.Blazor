namespace Masa.Blazor
{
    public interface ISheet : IRoundable, IThemeable, IColorable, IElevatable, IMeasurable
    {
        bool Outlined { get; }

        bool Shaped { get; }

        string Tag { get; }
    }
}
