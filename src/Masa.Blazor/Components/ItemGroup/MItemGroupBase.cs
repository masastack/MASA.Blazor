using Masa.Blazor.Mixins;
using Util.Reflection.Expressions.Abstractions;

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

    internal List<StringNumber?> InternalValues { get; private set; } = [];

    protected StringNumber? InternalValue => InternalValues.LastOrDefault();

    protected bool ToggleButUnselect { get; set; }

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
           OnInternalValuesChanged();
        }
    }

    protected virtual void OnInternalValuesChanged()
    {
        RefreshItemsState();
    }

    protected virtual void RefreshItemsState()
    {
        Items.ForEach(item => item.RefreshState());
    }

    internal async Task Register(IGroupable item)
    {
        item.Value ??= _registeredItemsIndex++;

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
        await RunTaskInMicrosecondsAsync(() =>
        {
            RefreshItemsState();
            OnItemsUpdate();
        }, 16, _cts.Token);
    }

    public virtual void Unregister(IGroupable item)
    {
        Items.Remove(item);

        if (_registeredItemsIndex > 0)
        {
            _registeredItemsIndex--;
        }
    }

    protected virtual void OnItemsUpdate() {}
    
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
        InternalValues = UpdateInternalValues(key);

        if (Multiple)
        {
            if (ValuesChanged.HasDelegate)
            {
                await ValuesChanged.InvokeAsync(InternalValues);
            }
            else
            {
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
        var valueExists = internalValues.Contains(key);

        if (Multiple)
        {
            if (valueExists)
            {
                internalValues.Remove(key);
                ToggleButUnselect = true;
            }
            else
            {
                internalValues.Add(key);
            }
        }
        else
        {
            if (valueExists)
            {
                if (!Mandatory)
                {
                    internalValues.Remove(key);
                }
            }
            else
            {
                internalValues.Clear();
                internalValues.Add(key);
            }
        }

        return internalValues;
    }
}