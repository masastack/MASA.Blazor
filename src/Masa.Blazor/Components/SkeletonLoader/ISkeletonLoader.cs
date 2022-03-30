namespace Masa.Blazor
{
    public interface ISkeletonLoader : IElevatable, IMeasurable, IThemeable
    {
        bool Boilerplate { get; }

        bool Loading { get; }

        bool Tile { get; }

        string Transition { get; }

        string Type { get; }

        Dictionary<string, string> Types { get; }
    }
}
