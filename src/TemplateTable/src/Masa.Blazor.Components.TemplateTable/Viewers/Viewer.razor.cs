namespace Masa.Blazor.Components.TemplateTable.Viewers;

public partial class Viewer : IAsyncDisposable
{
    [Inject] private IJSRuntime JSRuntime { get; set; } = default!;

    [Inject] private MasaBlazor MasaBlazor { get; set; } = default!;

    [Parameter] public string Class { get; set; } = default!;

    [Parameter] public AlignTypes HeaderTextAlign { get; set; }

    /// <summary>
    /// View columns without an order, that will be ordered by <see cref="ColumnOrder"/>.
    /// </summary>
    [Parameter]
    public ICollection<ViewColumnInfo> ViewColumns { get; set; } = [];

    /// <summary>
    /// The order of view columns.
    /// </summary>
    [Parameter]
    public List<string> ColumnOrder { get; set; } = [];

    [Parameter] public EventCallback<List<string>> ColumnOrderChanged { get; set; }

    /// <summary>
    /// The set of hidden column IDs.
    /// </summary>
    [Parameter]
    public HashSet<string> HiddenColumnIds { get; set; } = [];

    [Parameter] public bool Loading { get; set; }

    [Parameter] public bool Editable { get; set; }

    [Parameter] public RowHeight RowHeight { get; set; }

    [Parameter] public ICollection<Row> Rows { get; set; } = [];

    [Parameter] public Sort? Sort { get; set; }

    [Parameter] public Filter? Filter { get; set; }

    [Parameter] public EventCallback<Sort> OnSortUpdate { get; set; }

    /// <summary>
    /// The height of the table.
    /// </summary>
    [Parameter]
    public StringNumber? Height { get; set; }

    [Parameter] public bool HasActions { get; set; }

    [Parameter] public bool ShowSelect { get; set; }

    [Parameter] public string? CheckboxColor { get; set; }

    [Parameter] public EventCallback<Column> OnColumnEditClick { get; set; }

    [Parameter] public EventCallback<string> OnColumnToggle { get; set; }

    [Parameter] public EventCallback<(string ColumnId, double Width)> OnColumnResize { get; set; }

    [Parameter] public EventCallback<List<string>> OnImagePreview { get; set; }

    [Parameter] public EventCallback<IReadOnlyDictionary<string, JsonElement>> OnDetail { get; set; }

    [Parameter] public RowActionsContext RowActionsContext { get; set; } = null!;

    [Parameter] public bool HasCustomRowActions { get; set; }

    [Parameter] public RenderFragment<RowActionsContext>? RowActionsContent { get; set; }

    [Parameter] public RenderFragment<CustomCellContext>? CustomCellContent { get; set; }

    [Parameter] public List<string> SelectedKeys { get; set; } = [];

    [Parameter] public Action<(Row Item, bool Selected)> OnSelect { get; set; } = default!;

    [Parameter] public Action<bool> OnSelectAll { get; set; } = default!;

    [Parameter] public string TableHeaderClass { get; set; } = default!;

    [Parameter] public string TableHeaderThClass { get; set; } = default!;

    [Parameter] public string TableBodyTrClass { get; set; } = default!;

    [Parameter] public string TableBodyTdClass { get; set; } = default!;

    [Parameter] public string TableStripeClass { get; set; } = default!;

    [Parameter] public EventCallback<IReadOnlyCollection<Row>> OnRemove { get; set; }

    private IDictionary<string, IDictionary<string, object?>> _actionsDefautls =
        new Dictionary<string, IDictionary<string, object?>>
        {
            [nameof(MButton)] = new Dictionary<string, object?>
            {
                [nameof(MButton.Small)] = true
            }
        };

    // ReSharper disable once StaticMemberInGenericType
    private static Block _block = new("masa-table-viewer");
    private ModifierBuilder _rowModifierBuilder = _block.Element("row").CreateModifierBuilder();
    private ModifierBuilder _modifierBuilder = _block.CreateModifierBuilder();
    private StyleBuilder _styleBuilder = new();

    private bool _sized;
    private ElementReference _headerTrRef;

    private MSimpleTable? _simpleTable;
    private string? _tableSelector;

    private HashSet<string>? _prevHiddenColumnIds;
    private List<string>? _prevColumnOrder;

    private IJSObjectReference? _tableJSObjectReference;
    private DotNetObjectReference<Viewer>? _dotNetObjectReference;

    private int _actionsCount = 0;
    private List<ViewColumnInfo> _orderedViewColumns = [];

    private string? _lastLeftFixedColumnId;
    private string? _firstRightFixedColumnId;

    private (bool Indeterminate, bool AllSelected) SelectionState
    {
        get
        {
            var length = Rows.Count;
            var count = Rows.Count(item => SelectedKeys.Contains(item.Key));
            var allSelected = length == count;
            return (count > 0 && !allSelected, allSelected);
        }
    }

    private int RowHeightValue => RowHeight switch
    {
        RowHeight.Low => 1,
        RowHeight.Medium => 2,
        RowHeight.High => 5,
        _ => throw new ArgumentOutOfRangeException()
    };

    protected override void OnInitialized()
    {
        base.OnInitialized();

        _dotNetObjectReference = DotNetObjectReference.Create(this);
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        _orderedViewColumns = ViewColumns.OrderBy(u => ColumnOrder.IndexOf(u.ColumnId)).ToList();
        _lastLeftFixedColumnId = _orderedViewColumns.LastOrDefault(u => u.Fixed == ColumnFixed.Left)?.ColumnId;
        _firstRightFixedColumnId = _orderedViewColumns.FirstOrDefault(u => u.Fixed == ColumnFixed.Right)?.ColumnId;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            var importJSObjectReference = await JSRuntime.InvokeAsync<IJSObjectReference>("import",
                "./_content/Masa.Blazor.Components.TemplateTable/MTemplateTable.razor.js");
            _tableJSObjectReference = await importJSObjectReference.InvokeAsync<IJSObjectReference>("init",
                _simpleTable?.Ref, _dotNetObjectReference);
            await importJSObjectReference.DisposeAsync();

            _sized = true;
            StateHasChanged();
        }
    }

    internal string? GetTableSelector()
    {
        if (_simpleTable?.Ref.TryGetSelector(out var selector) is true)
        {
            return _tableSelector ??= selector + " table";
        }

        return null;
    }

    [JSInvokable]
    public void OnColumnWidthResize(string columnId, double width)
    {
        OnColumnResize.InvokeAsync((columnId, width));
    }

    public async ValueTask DisposeAsync()
    {
        try
        {
            if (_tableJSObjectReference != null)
            {
                await _tableJSObjectReference.InvokeVoidAsync("dispose");
                await _tableJSObjectReference.DisposeAsync().ConfigureAwait(false);
            }
        }
        catch (JSDisconnectedException)
        {
            // ignored
        }
        // HACK: remove this after https://github.com/dotnet/aspnetcore/issues/52119 is fixed
        catch (JSException e) when (e.Message.Contains("has it been disposed")
                                    && (OperatingSystem.IsWindows() || OperatingSystem.IsAndroid() ||
                                        OperatingSystem.IsIOS()))
        {
            // ignored
        }
        catch (InvalidOperationException e) when (e.Message.Contains("prerendering"))
        {
            // ignored
        }
    }

    private (bool hidden, string? css, string? style) GetCss(ViewColumnInfo template)
    {
        bool hidden;
        if (template.Column.Id == Preset.ActionsColumnId)
        {
            hidden = !HasActions;
        }
        else if (template.Column.Id == Preset.RowSelectColumnId)
        {
            hidden = !ShowSelect;
        }
        else
        {
            hidden = HiddenColumnIds.Contains(template.ColumnId);
        }

        var fixedLeft = template.Column.Type is ColumnType.RowSelect || template.Fixed == ColumnFixed.Left;
        var fixedRight = template.Column.Type is ColumnType.Actions || template.Fixed == ColumnFixed.Right;

        CssBuilder cssBuilder = new();
        cssBuilder.Add("d-none", hidden);
        cssBuilder.Add(template.Column.Type.ToString().ToLowerInvariant());
        cssBuilder.Add("m-data-table__column--fixed-left", fixedLeft);
        cssBuilder.Add("m-data-table__column--fixed-right", fixedRight);
        cssBuilder.Add("first-fixed-column",
            template.ColumnId == _lastLeftFixedColumnId || template.ColumnId == _firstRightFixedColumnId);

        StyleBuilder styleBuilder = new();
        if (fixedLeft)
        {
            var index = _orderedViewColumns.IndexOf(template);
            if (index > -1)
            {
                var widths = _orderedViewColumns.Take(index).Sum(u => u.Width);
                styleBuilder.Add(MasaBlazor.RTL ? "right" : "left", $"{widths}px");
            }
        }
        else if (fixedRight)
        {
            var count = _orderedViewColumns.Count;
            var lastIndex = _orderedViewColumns.LastIndexOf(template);
            if (lastIndex > -1)
            {
                var widths = _orderedViewColumns.TakeLast(count - lastIndex - 1).Sum(u => u.Width);
                styleBuilder.Add(MasaBlazor.RTL ? "left" : "right", $"{widths}px");
            }
        }

        return (hidden, cssBuilder.ToString(), styleBuilder.ToString());
    }

    private (bool hidden, string? css, string? style) GetHeaderColCss(ViewColumnInfo template)
    {
        bool handle, hidden;
        if (template.Column.Id == Preset.ActionsColumnId)
        {
            handle = false;
            hidden = !HasActions;
        }
        else if (template.Column.Id == Preset.RowSelectColumnId)
        {
            handle = false;
            hidden = !ShowSelect;
        }
        else
        {
            handle = true;
            hidden = HiddenColumnIds.Contains(template.ColumnId);
        }

        var fixedLeft = template.Column.Type is ColumnType.RowSelect || template.Fixed == ColumnFixed.Left;
        var fixedRight = template.Column.Type is ColumnType.Actions || template.Fixed == ColumnFixed.Right;

        CssBuilder cssBuilder = new();
        cssBuilder.Add("masa-table-viewer__header-column");
        cssBuilder.Add(handle ? "handle" : "ignore-elements");
        cssBuilder.Add("d-none", hidden);
        cssBuilder.Add(template.Column.Type.ToString().ToLowerInvariant());
        cssBuilder.Add("m-data-table__column--fixed-left", fixedLeft);
        cssBuilder.Add("m-data-table__column--fixed-right", fixedRight);
        cssBuilder.Add("first-fixed-column",
            template.ColumnId == _lastLeftFixedColumnId || template.ColumnId == _firstRightFixedColumnId);

        StyleBuilder styleBuilder = new();

        // var width = template.Column.Type is ColumnType.Actions
        //     ? (RowActionsContext?.Width ?? 0)
        //     : template.Width;

        var width = template.Width;

        if (width > 0)
        {
            styleBuilder.AddWidth(width);
            styleBuilder.AddMinWidth(width);
        }

        if (fixedLeft)
        {
            var index = _orderedViewColumns.IndexOf(template);
            if (index > -1)
            {
                var widths = _orderedViewColumns.Take(index).Sum(u => u.Width);
                styleBuilder.Add(MasaBlazor.RTL ? "right" : "left", $"{widths}px");
            }
        }
        else if (fixedRight)
        {
            var count = _orderedViewColumns.Count;
            var lastIndex = _orderedViewColumns.LastIndexOf(template);
            if (lastIndex > -1)
            {
                var widths = _orderedViewColumns.TakeLast(count - lastIndex - 1).Sum(u => u.Width);
                styleBuilder.Add(MasaBlazor.RTL ? "left" : "right", $"{widths}px");
            }
        }

        return (hidden, cssBuilder.ToString(), styleBuilder.ToString());
    }
}