using Masa.Blazor.Mixins;

namespace Masa.Blazor.Components.ItemGroup;

public abstract class MItemGroupBase : MasaComponentBase
{
    protected MItemGroupBase(GroupType groupType)
    {
        GroupType = groupType;
    }

    [Parameter]
    [MasaApiParameter("m-item--active")]
    public string? ActiveClass { get; set; } = "m-item--active";

    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter]
    [MasaApiParameter(Ignored = true)]
    public GroupType? TargetGroup { get; set; }

    [Parameter]
    public bool Mandatory { get; set; }

    [Parameter]
    public bool Multiple { get; set; }

    [Parameter]
    public StringNumber? Value { get; set; }

    [Parameter]
    public EventCallback<StringNumber?> ValueChanged { get; set; }

    [Parameter]
    public List<StringNumber?>? Values { get; set; }

    [Parameter]
    public EventCallback<List<StringNumber?>> ValuesChanged { get; set; }

    private int _registeredItemsIndex;

    public GroupType GroupType { get; protected set; }

    public List<IGroupable> Items { get; } = new();

    protected List<StringNumber?> AllValues => Items.Select(item => item.Value).ToList();

    internal List<StringNumber?> InternalValues
    {
        get => GetValue<List<StringNumber?>>(new()) ?? new List<StringNumber?>();
        set => SetValue(value);
    }

    protected StringNumber? InternalValue => InternalValues.LastOrDefault();

    private HashSet<StringNumber?> _prevInternalValues = [];
    private CancellationTokenSource? _cts;

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        InitOrUpdateInternalValues();

        if (TargetGroup.HasValue)
        {
            GroupType = TargetGroup.Value;
        }

        if (!_prevInternalValues.SetEquals(InternalValues))
        {
            _prevInternalValues = [..InternalValues];
           RefreshItemsState();
        }
    }

    protected virtual void RefreshItemsState()
    {
        Items.ForEach(item => item.RefreshState());
    }

    protected StringNumber InitDefaultItemValue()
    {
        return _registeredItemsIndex++;
    }

    internal virtual async Task Register(IGroupable item)
    {
        item.Value ??= InitDefaultItemValue();

        Items.Add(item);

        // if no value provided and mandatory
        // assign first registered item
        if (Mandatory)
        {
            if (InternalValues.Count == 0)
            {
                InternalValues = [item.Value];

                if (Multiple)
                {
                    await ValuesChanged.InvokeAsync(InternalValues.ToList());
                }
                else
                {
                    await ValueChanged.InvokeAsync(item.Value);
                }
            }
        }

        _cts?.Cancel();
        _cts = new CancellationTokenSource();
        await RunTaskInMicrosecondsAsync(RefreshItemsState, 16, _cts.Token);
    }

    public virtual void Unregister(IGroupable item)
    {
        Items.Remove(item);

        if (_registeredItemsIndex > 0)
        {
            _registeredItemsIndex--;
        }
    }

    private async Task UpdateMandatoryAsync(bool last = false)
    {
        if (Items.Count == 0) return;

        var items = new List<IGroupable>(Items);

        if (last) items.Reverse();

        var item = items.FirstOrDefault(item => !item.Disabled);

        if (item == null) return;

        await ToggleAsync(item.Value);
    }

    public virtual async Task ToggleAsync(StringNumber? key)
    {
        // have to invoke the InternalValues's setter
        InternalValues = UpdateInternalValues(key);

        if (Multiple)
        {
            if (ValuesChanged.HasDelegate)
            {
                await ValuesChanged.InvokeAsync(InternalValues);
            }
            else
            {
                if (Values != null)
                {
                    InternalValues = Values.ToList();
                }
            }
        }
        else
        {
            var value = InternalValues.LastOrDefault();
            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(value);
            }
            else
            {
                if (Value != null)
                {
                    InternalValues = new List<StringNumber?>() { Value };
                }
            }
        }

        RefreshItemsState();
    }

    private void InitOrUpdateInternalValues()
    {
        if (Multiple)
        {
            if (!IsDirtyParameter(nameof(Values))) return;

            InternalValues = Values == null ? [] : Values.ToList();
        }
        else
        {
            if (!IsDirtyParameter(nameof(Value))) return;

            InternalValues = Value == null ? [] : [Value];
        }
    }

    protected virtual List<StringNumber?> UpdateInternalValues(StringNumber? key)
    {
        var internalValues = InternalValues.ToList();

        if (internalValues.Contains(key))
        {
            internalValues.Remove(key);
        }
        else
        {
            if (!Multiple)
            {
                internalValues.Clear();
            }

            internalValues.Add(key);
        }

        if (Mandatory && internalValues.Count == 0)
        {
            internalValues.Add(key);
        }

        return internalValues;
    }
}