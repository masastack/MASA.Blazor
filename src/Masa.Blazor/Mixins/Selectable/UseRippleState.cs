namespace Masa.Blazor;

public readonly struct UseRippleState
{
    private readonly IRippleState _state;

    public UseRippleState(IRippleState state)
    {
        _state = state;
    }

    public string? RippleState
        => _state is { IsDisabled: false, ValidationState: null }
            ? null
            : _state.ValidationState;
}