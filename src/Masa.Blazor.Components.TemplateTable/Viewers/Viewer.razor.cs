using BemIt;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.RenderTree;

namespace Masa.Blazor.Components.TemplateTable.Viewers;

public partial class Viewer : IAsyncDisposable
{
    [Inject] private IJSRuntime JSRuntime { get; set; } = default!;

    [Inject] private MasaBlazor MasaBlazor { get; set; } = default!;

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

    [Parameter] public RowHeight RowHeight { get; set; }

    [Parameter] public ICollection<Row> Rows { get; set; } = [];

    [Parameter] public Sort? Sort { get; set; }

    [Parameter] public EventCallback<Sort> OnSortUpdate { get; set; }

    /// <summary>
    /// The height of the table.
    /// </summary>
    [Parameter]
    public StringNumber? Height { get; set; }

    [Parameter] public bool HasActions { get; set; }

    [Parameter] public bool ShowSelect { get; set; }

    [Parameter] public string? CheckboxColor { get; set; }

    [Parameter] public bool HideDetail { get; set; }

    [Parameter] public EventCallback<Column> OnColumnEditClick { get; set; }

    [Parameter] public EventCallback<string> OnColumnToggle { get; set; }

    [Parameter] public EventCallback<(string ColumnId, double Width)> OnColumnResize { get; set; }

    [Parameter] public EventCallback<List<string>> OnImagePreview { get; set; }

    [Parameter] public EventCallback<IReadOnlyDictionary<string, JsonElement>> OnDetail { get; set; }

    [Parameter] public RenderFragment? ActionsContent { get; set; }

    [Parameter] public List<string> SelectedKeys { get; set; } = [];

    // [Parameter] public List<string> SelectableKeys { get; set; } = [];

    [Parameter] public Action<(Row Item, bool Selected)> OnSelect { get; set; } = default!;

    [Parameter] public Action<bool> OnSelectAll { get; set; } = default!;

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
        RowHeight.Low => 42,
        RowHeight.Medium => 63,
        RowHeight.High => 105,
        _ => throw new ArgumentOutOfRangeException()
    };

    private List<ViewColumnInfo> _orderedViewColumns = [];

    protected override void OnInitialized()
    {
        base.OnInitialized();

        InitActionsCount();

        _dotNetObjectReference = DotNetObjectReference.Create(this);
    }

    private void InitActionsCount()
    {
        if (ActionsContent is null)
        {
            return;
        }

        var builder = new RenderTreeBuilder();
        ActionsContent.Invoke(builder);
#pragma warning disable BL0006
        _actionsCount = builder.GetFrames().Array.Count(u => u.FrameType == RenderTreeFrameType.Component);
#pragma warning restore BL0006
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        Console.Out.WriteLine($"[Viewer] OnParametersSet ColumnOrder: {string.Join(", ", ColumnOrder)}");
        Console.Out.WriteLine($"[Viewer] OnParametersSet ViewColumns: {string.Join(", ", ViewColumns.Select(u => u.ColumnId))}");

        _orderedViewColumns = ViewColumns.OrderBy(u => ColumnOrder.IndexOf(u.ColumnId)).ToList();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            // TODO: dispose

            _tableJSObjectReference = await JSRuntime.InvokeAsync<IJSObjectReference>("import",
                "./_content/Masa.Blazor.Components.TemplateTable/MTemplateTable.razor.js");
            await _tableJSObjectReference.InvokeVoidAsync("init", _simpleTable?.Ref,
                _dotNetObjectReference);

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

        StyleBuilder styleBuilder = new();
        if (fixedLeft)
        {
            var index = _orderedViewColumns.IndexOf(template);
            if (index > -1)
            {
                var widths = _orderedViewColumns.Take(index).Sum(u => u.Width);
                styleBuilder.Add(MasaBlazor.RTL ? "right": "left", $"{widths}px");
            }
        }
        else if (fixedRight)
        {
            var count = _orderedViewColumns.Count;
            var lastIndex = _orderedViewColumns.LastIndexOf(template);
            if (lastIndex > -1)
            {
                var widths = _orderedViewColumns.TakeLast(count - lastIndex - 1).Sum(u => u.Width);
                styleBuilder.Add(MasaBlazor.RTL ? "left": "right", $"{widths}px");
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

        StyleBuilder styleBuilder = new();
        styleBuilder.AddWidth(template.Width, predicate: () => template.Width != 0);
        styleBuilder.AddIf("--m-configurable-table-actions-count", _actionsCount.ToString(), template.Column.Type is ColumnType.Actions);

        if (fixedLeft)
        {
            var index = _orderedViewColumns.IndexOf(template);
            if (index > -1)
            {
                var widths = _orderedViewColumns.Take(index).Sum(u => u.Width);
                styleBuilder.Add(MasaBlazor.RTL ? "right": "left", $"{widths}px");
            }
        }
        else if (fixedRight)
        {
            var count = _orderedViewColumns.Count;
            var lastIndex = _orderedViewColumns.LastIndexOf(template);
            if (lastIndex > -1)
            {
                var widths = _orderedViewColumns.TakeLast(count - lastIndex - 1).Sum(u => u.Width);
                styleBuilder.Add(MasaBlazor.RTL ? "left": "right", $"{widths}px");
            }
        }

        return (hidden, cssBuilder.ToString(), styleBuilder.ToString());
    }
}