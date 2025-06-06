using Masa.Blazor.Components.Select;

namespace Masa.Blazor;

public partial class MCombobox<TItem, TValue> : MAutocomplete<TItem, string, TValue>
{
    /// <summary>
    /// Accepts an array of strings that will trigger a new tag when typing. Does not replace the normal Tab and Enter keys.
    /// </summary>
    [Parameter] public string[] Delimiters { get; set; } = [];

    private int _editingIndex = -1;

    protected override IEnumerable<string> KeysForKeyDownWithPreventDefault { get; } = [KeyCodes.Tab, KeyCodes.End];

    protected override bool MenuCanShow
    {
        get
        {
            if (!IsFocused) return false;

            return HasDisplayedItems || (NoDataContent is not null && !HideNoData);
        }
    }

    protected override async void OnInternalSearchChanged(string? val)
    {
        if (val != null && Multiple && Delimiters.Length > 0)
        {
            var delimiter = Delimiters.FirstOrDefault(val.EndsWith);
            if (delimiter != null)
            {
                InternalSearch = val[..^delimiter.Length];
                await UpdateTags();
            }
        }

        base.OnInternalSearchChanged(InternalSearch);
    }

    internal override async Task OnChipInput(SelectedItem<TItem> item)
    {
        await base.OnChipInput(item);

        _editingIndex = -1;
    }

    public override async Task HandleOnKeyDownAsync(KeyboardEventArgs args)
    {
        var keyCode = args.Key;

        if (args.CtrlKey || !(new[] { KeyCodes.Home, KeyCodes.End }.Contains(keyCode)))
        {
            await base.HandleOnKeyDownAsync(args);
        }

        // if use is at selection index of 0,
        // create a new tag
        var selectionStart = await Js.InvokeAsync<int>(JsInteropConstants.GetProp, InputElement, "selectionStart");
        if (Multiple && keyCode == KeyCodes.ArrowLeft && selectionStart == 0)
        {
            await UpdateSelf();
        }
        else if (keyCode == KeyCodes.Enter)
        {
            OnEnterDown();
        }
    }

    private void OnEnterDown()
    {
        var menuIndex = GetMenuIndex();
        if (menuIndex > -1)
        {
            return;
        }

        NextTick(UpdateSelf);
    }

    protected override async Task OnTabDown(KeyboardEventArgs args)
    {
        if (Multiple && !string.IsNullOrEmpty(InternalSearch) && GetMenuIndex() == -1)
        {
            await UpdateTags();
            return;
        }

        await base.OnTabDown(args);
    }

    internal override async Task SelectItem(SelectedItem<TItem> item, bool closeOnSelect = true)
    {
        if (_editingIndex > -1)
        {
            await UpdateEditing();
        }
        else
        {
            await base.SelectItem(item, closeOnSelect);

            if (!string.IsNullOrEmpty(InternalSearch) && Multiple &&
                GetText(item.Item)?.Contains(InternalSearch, StringComparison.CurrentCultureIgnoreCase) is true)
            {
                InternalSearch = null;
            }
        }
    }

    protected override void SetSelectedItems()
    {
        if (InternalValue is null || (InternalValue is string str && string.IsNullOrEmpty(str)))
        {
            SelectedItems.Clear();
        }
        else
        {
            SelectedItems.Clear();
            foreach (var value in InternalValues)
            {
                var index = AllItems.FindIndex(v => EqualityComparer<string>.Default.Equals(value, GetValue(v)));
                SelectedItems.Add(index > -1
                    ? new SelectedItem<TItem>(AllItems[index], value)
                    : new SelectedItem<TItem>(default, value));
            }
        }

        StateHasChanged();
    }

    protected override Task SetsValue(TValue value)
    {
        return base.SetsValue(EqualityComparer<TValue>.Default.Equals(value, default)
            ? (TValue)(object)InternalSearch
            : value);
    }

    protected override async Task UpdateSelf()
    {
        if (Multiple)
        {
            await UpdateTags();
        }
        else
        {
            await UpdateCombobox();
        }
    }

    private async Task UpdateTags()
    {
        var menuIndex = GetMenuIndex();

        // If the user is not searching,
        // and no menu item is selected
        // or if the search is empty,
        // do nothing
        if ((menuIndex < 0 && !SearchIsDirty) || string.IsNullOrEmpty(InternalSearch))
        {
            return;
        }

        if (_editingIndex > -1)
        {
            await UpdateEditing();
            return;
        }

        var index = SelectedItems.FindIndex(item => InternalSearch == GetText(item));

        var itemToSelect = index > -1 && SelectedItems[index].Item is not null
            ? SelectedItems[index]
            : new SelectedItem<TItem>(default, InternalSearch);

        if (index > -1)
        {
            // var internalValue = ((IList<string>)InternalValue);
            // internalValue.RemoveAt(index);
            // InternalValues.RemoveAt(index);
            // await SetsValue((TValue)internalValue);
            InternalValues.RemoveAt(index);
        }

        if (menuIndex > -1)
        {
            InternalSearch = null;
            return;
        }

        await SelectItem(itemToSelect);

        InternalSearch = null;
    }

    private async Task UpdateEditing()
    {
        var value = InternalValues.ToList();
        var index = SelectedItems.FindIndex(item =>
            GetText(item)?.Equals(InternalSearch, StringComparison.OrdinalIgnoreCase) is true);

        // If a user enters a duplicate text on chip edit,
        // don't add it, move it to the end of the list
        if (index > -1)
        {
            var item = value[index];

            value.RemoveAt(index);
            value.Add(item);
        }
        else
        {
            value[_editingIndex] = InternalSearch;
        }

        await SetsValue((TValue)(object)value);
        _editingIndex = -1;
        InternalSearch = null;
    }

    private async Task UpdateCombobox()
    {
        // If search is not dirty, do nothing
        if (!SearchIsDirty)
        {
            return;
        }

        // The internal search is not matching
        // the internal value, update the input
        if (InternalSearch != InternalValue?.ToString())
        {
            await SetsValue(default);
        }

        var isUsingSlot = SelectionContent is not null || HasChips;
        if (isUsingSlot)
        {
            InternalSearch = null;
        }
    }

    public override Task HandleOnClearClickAsync(MouseEventArgs args)
    {
        _editingIndex = -1;

        return base.HandleOnClearClickAsync(args);
    }
}