namespace Masa.Blazor;

public interface IRadio<TValue>
{
    TValue Value { get; }

    void RefreshState();
}
