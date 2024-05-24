using Masa.Blazor.Mixins;

namespace Masa.Blazor.Components.ItemGroup;

public abstract class MItemGroupBase : MasaComponentBase
{
    protected MItemGroupBase(GroupType groupType)
    {
        GroupType = groupType;
    }

    [Parameter]
    public string? ActiveClass { get; set; }

    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter]
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

    public GroupType GroupType { get; private set; }

    public List<IGroupable> Items { get; } = new();

    protected List<StringNumber?> AllValues => Items.Select(item => item.Value).ToList();

    internal List<StringNumber?> InternalValues
    {
        get => GetValue<List<StringNumber?>>(new()) ?? new List<StringNumber?>();
        set => SetValue(value);
    }

    protected StringNumber? InternalValue => InternalValues?.LastOrDefault();

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        InitOrUpdateInternalValues();

        if (TargetGroup.HasValue)
        {
            GroupType = TargetGroup.Value;
        }

        RefreshItemsState();
    }

    private void RefreshItemsState()
    {
        Items.ForEach(item => item.RefreshState());
    }

    protected StringNumber InitDefaultItemValue()
    {
        return _registeredItemsIndex++;
    }

    internal virtual void Register(IGroupable item)
    {
        item.Value ??= InitDefaultItemValue();

        Items.Add(item);

        // if no value provided and mandatory
        // assign first registered item
        if (Mandatory)
        {
            if (InternalValues.Count == 0)
            {
                InternalValues = new List<StringNumber?>() { item.Value };

                if (Multiple)
                {
                    if (ValuesChanged.HasDelegate)
                    {
                        ValuesChanged.InvokeAsync(InternalValues.ToList());
                    }
                }
                else
                {
                    if (ValueChanged.HasDelegate)
                    {
                        ValueChanged.InvokeAsync(item.Value);
                    }
                }
            }
        }

        RefreshItemsState();
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

    public async Task ToggleAsync(StringNumber? key)
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

                StateHasChanged();
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

                StateHasChanged();
            }
        }

        RefreshItemsState();
    }

    private void InitOrUpdateInternalValues()
    {
        if (Multiple)
        {
            if (!IsDirtyParameter(nameof(Values))) return;

            InternalValues = Values == null ? new List<StringNumber?>() : Values.ToList();
        }
        else
        {
            if (!IsDirtyParameter(nameof(Value))) return;

            InternalValues = Value == null ? new List<StringNumber?>() : new List<StringNumber?>() { Value };
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