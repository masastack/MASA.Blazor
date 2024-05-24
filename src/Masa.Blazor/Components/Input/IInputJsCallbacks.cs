namespace Masa.Blazor.Components.Input;

public interface IInputJsCallbacks
{
    ElementReference InputElement { get; }

    ElementReference InputSlotElement { get; }

    int InternalDebounceInterval { get; }

    Task HandleOnInputAsync(ChangeEventArgs args);

    Task HandleOnChangeAsync(ChangeEventArgs args);

    Task HandleOnClickAsync(ExMouseEventArgs args);

    Task HandleOnMouseUpAsync(ExMouseEventArgs args);

    void StateHasChangedForJsInvokable();
}
