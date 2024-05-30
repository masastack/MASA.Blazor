namespace Masa.Blazor;

public interface IRadioGroup<TValue>
{
    TValue Value { get; }

    string? ValidationState { get; }

    bool IsDisabled { get; }

    bool IsReadonly { get; }

    bool Dense { get; }

    Task Toggle(TValue value);

    void AddRadio(IRadio<TValue> radio);
    
    bool HasState { get; }
}