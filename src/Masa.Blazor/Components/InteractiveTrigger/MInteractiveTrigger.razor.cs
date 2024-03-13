namespace Masa.Blazor;

public partial class MInteractiveTrigger<TValue> : MInteractiveTriggerBase<TValue, TValue>
{
    protected override string ComponentName => nameof(MInteractiveTrigger<TValue>);

    protected override bool CheckInteractive()
    {
        return EqualityComparer<TValue>.Default.Equals(QueryValue, InteractiveValue);
    }
}
