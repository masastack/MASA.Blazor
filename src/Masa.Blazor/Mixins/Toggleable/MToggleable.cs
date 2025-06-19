using Masa.Blazor.Mixins.Delayable;

namespace Masa.Blazor.Mixins.Toggleable;

public abstract class MToggleable : MDelayable
{
    [Parameter]
    public bool Value
    {
        get => _value;
        set
        {
            var tempValue = _value;
            _value = value;

            if (tempValue != _value)
            {
                InternalOnValueChanged(value);
            }
        }
    }

    [Parameter]
    public EventCallback<bool> ValueChanged { get; set; }

    private bool _preventOnValueChanged;
    private bool _value;

    private void InternalOnValueChanged(bool val)
    {
        if (_preventOnValueChanged)
        {
            _preventOnValueChanged = false;
            return;
        }

        OnValueChanged(val);
    }

    protected virtual void OnValueChanged(bool val)
    {
    }

    protected override async Task OnActiveUpdatingAsync(bool active)
    {
        if (ValueChanged.HasDelegate && active != Value)
        {
            _preventOnValueChanged = true;
            await ValueChanged.InvokeAsync(active);
        }
    }
}
