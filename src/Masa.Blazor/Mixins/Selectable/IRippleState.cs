namespace Masa.Blazor;

public interface IRippleState
{
    bool IsDisabled { get; }

    string? ValidationState { get; }
}