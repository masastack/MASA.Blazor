using Masa.Blazor.Components.MobilePicker;
using StyleBuilder = Masa.Blazor.Core.StyleBuilder;

namespace Masa.Blazor;

public partial class MMobilePickerView<TColumn, TColumnItem, TColumnItemValue> : MasaComponentBase
{
    [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

    [Parameter, EditorRequired] public virtual List<TColumn> Columns { get; set; } = null!;

    [Parameter, EditorRequired] public virtual Func<TColumnItem, string> ItemText { get; set; } = null!;

    [Parameter, EditorRequired] public virtual Func<TColumnItem, TColumnItemValue> ItemValue { get; set; } = null!;

    [Parameter] public virtual Func<TColumnItem, List<TColumnItem>>? ItemChildren { get; set; }

    [Parameter] public Func<TColumnItem, bool>? ItemDisabled { get; set; }

    // TODO: change int to StringNumber, support px, vh, vw, rem
    [Parameter] [MasaApiParameter(40)] public int ItemHeight { get; set; } = 40;

    [Parameter] public EventCallback<List<TColumnItem>> OnSelect { get; set; }

    [Parameter] [MasaApiParameter(1000)] public int SwipeDuration { get; set; } = 1000;

    [Parameter] [MasaApiParameter(6)] public int VisibleItemCount { get; set; } = 6;

    [Parameter]
    public List<TColumnItemValue> Value
    {
        get => _value;
        set => _value = value ?? new List<TColumnItemValue>();
    }

    [Parameter] public EventCallback<List<TColumnItemValue>> ValueChanged { get; set; }

    [Parameter] public bool Dark { get; set; }

    [Parameter] public bool Light { get; set; }

    [CascadingParameter(Name = "IsDark")] public bool CascadingIsDark { get; set; }

    public bool IsDark
    {
        get
        {
            if (Dark)
            {
                return true;
            }

            if (Light)
            {
                return false;
            }

            return CascadingIsDark;
        }
    }

    protected List<MobilePickerColumn<TColumnItem>> FormattedColumns { get; set; } = new();

    private string? _dataType;
    private List<TColumn>? _prevColumns;
    private List<TColumnItemValue>? _preValue;
    private List<TColumnItemValue> _value = new();

    private List<TColumnItemValue> InternalValue { get; set; } = new();

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        await base.SetParametersAsync(parameters);

        Columns.ThrowIfNull(ComponentName);
        ItemText.ThrowIfNull(ComponentName);
        ItemValue.ThrowIfNull(ComponentName);
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

#if NET8_0_OR_GREATER
        if (MasaBlazor.IsSsr && !IndependentTheme)
        {
            CascadingIsDark = MasaBlazor.Theme.Dark;
        }

#endif
        ComputeDataType();

        var isChanged = false;

        if (_preValue == null || !_preValue.SequenceEqual(Value))
        {
            _preValue = [..Value];
            InternalValue = Value.ToList();

            isChanged = true;
        }

        if (_prevColumns != Columns)
        {
            _prevColumns = Columns;
            isChanged = true;
        }

        if (isChanged)
        {
            Format();

            if (ValueChanged.HasDelegate)
            {
                _ = ValueChanged.InvokeAsync(InternalValue);
            }
        }
    }

    private bool IndependentTheme =>
        (IsDirtyParameter(nameof(Dark)) && Dark) || (IsDirtyParameter(nameof(Light)) && Light);

    private Block _block = new("m-mobile-picker");

    protected override IEnumerable<string> BuildComponentClass()
    {
        return _block.Element("view").AddTheme(IsDark, IndependentTheme).GenerateCssClasses();
    }

    protected override IEnumerable<string> BuildComponentStyle()
    {
        return StyleBuilder.Create().AddHeight(WrapHeight).GenerateCssStyles();
    }

    // TODO: use StringNumber support(px vh vw rem)
    protected int ItemPxHeight => ItemHeight;

    protected int WrapHeight
    {
        get
        {
            var itemHeight = ItemPxHeight;
            var itemCount = VisibleItemCount;

            return itemHeight * itemCount;
        }
    }

    private void ComputeDataType()
    {
        var firstColumn = Columns.FirstOrDefault();
        if (firstColumn is null)
        {
            return;
        }

        if (ItemChildren is not null)
        {
            if (firstColumn is TColumnItem)
            {
                _dataType = "cascade";
            }
        }
        else
        {
            if (firstColumn is IEnumerable<TColumnItem>)
            {
                _dataType = "list";
            }
        }
    }

    private void Format()
    {
        if (_dataType == "list")
        {
            FormatList();
        }
        else if (_dataType == "cascade")
        {
            FormatCascade();
        }
    }

    private void FormatList()
    {
        if (ItemValue == null) return;

        if (Columns is not List<List<TColumnItem>> columns)
        {
            return;
        }

        FormattedColumns.Clear();

        for (int i = 0; i < columns.Count; i++)
        {
            var column = columns[i];
            var index = 0;

            if (InternalValue.Count > i)
            {
                var val = InternalValue[i];
                var itemIndex =
                    column.FindIndex(c => EqualityComparer<TColumnItemValue>.Default.Equals(ItemValue(c), val));
                if (itemIndex > 0)
                    index = itemIndex;
            }

            FormattedColumns.Add(new MobilePickerColumn<TColumnItem>(column, index));
        }

        InternalValue = FormattedColumns.Select(c => ItemValue(c.Values.ElementAt(c.Index))).ToList();
    }

    private class Cursor
    {
        public List<TColumnItem>? Children { get; set; }

        public TColumnItemValue? Value { get; set; }

        public int Index { get; set; }
    }

    private void FormatCascade()
    {
        if (ItemValue == null) return;

        if (Columns is not List<TColumnItem> columns)
        {
            return;
        }

        List<MobilePickerColumn<TColumnItem>> formatted = new();

        int columnIndex = 0;

        Cursor cursor = new() { Children = columns };
        if (InternalValue.Count > columnIndex)
        {
            cursor.Value = InternalValue[columnIndex];
        }

        while (cursor.Children is not null && cursor.Children.Any())
        {
            var children = cursor.Children;

            var index = children.FindIndex(c =>
                EqualityComparer<TColumnItemValue>.Default.Equals(ItemValue(c), cursor.Value));
            if (index == -1)
            {
                index = 0;
            }

            cursor.Index = index;
            var defaultIndex = index;

            while (children.Count > defaultIndex && (ItemDisabled?.Invoke(children[defaultIndex]) is true))
            {
                if (defaultIndex < children.Count - 1)
                {
                    defaultIndex++;
                }
                else
                {
                    defaultIndex = 0;
                    break;
                }
            }

            formatted.Add(new MobilePickerColumn<TColumnItem>(cursor.Children, cursor.Index));

            var columnItem = children[defaultIndex];
            var columnItemChildren = ItemChildren?.Invoke(columnItem);

            columnIndex++;
            cursor = new() { Children = columnItemChildren };
            if (InternalValue.Count > columnIndex)
            {
                cursor.Value = InternalValue[columnIndex];
            }
        }

        FormattedColumns = formatted;

        InternalValue = FormattedColumns.Select(c => ItemValue(c.Values.ElementAt(c.Index))).ToList();
    }

    private async Task HandleOnChange(int columnIndex, TColumnItemValue value)
    {
        InternalValue[columnIndex] = value;

        Format();

        if (ValueChanged.HasDelegate)
        {
            _preValue = [..InternalValue];
            await ValueChanged.InvokeAsync(InternalValue.ToList());
        }

        var items = FormattedColumns.Select(c => c.Values[c.Index]).ToList();

        if (OnSelect.HasDelegate)
        {
            _ = OnSelect.InvokeAsync(items);
        }
    }
}