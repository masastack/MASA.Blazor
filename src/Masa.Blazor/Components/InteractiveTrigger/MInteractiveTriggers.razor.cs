namespace Masa.Blazor;

public partial class MInteractiveTriggers<TValue> : MInteractiveTriggerBase<TValue, IEnumerable<TValue>>
{
    protected override string ComponentName => nameof(MInteractiveTriggers<TValue>);

    protected override void OnInitialized()
    {
        base.OnInitialized();

        InteractiveValue.ThrowIfNull(ComponentName);
    }

    protected override bool CheckInteractive()
    {
        return InteractiveValue!.Any(val => EqualityComparer<TValue>.Default.Equals(QueryValue, val));
    }

}
