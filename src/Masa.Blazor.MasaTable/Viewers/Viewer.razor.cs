using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.JSInterop;

namespace Masa.Blazor.MasaTable.Viewers;

public partial class Viewer<TItem> : IAsyncDisposable
{
    [Inject] private IJSRuntime JSRuntime { get; set; } = null!;

    [Parameter] public List<ColumnTemplate<TItem, object>> ColumnTemplates { get; set; } = [];

    [Parameter] public List<string> ColumnOrder { get; set; } = [];

    [Parameter] public EventCallback<List<string>> ColumnOrderChanged { get; set; }

    [Parameter] public HashSet<string> HiddenColumnIds { get; set; } = [];

    [Parameter] public RowHeight RowHeight { get; set; }

    [Parameter] public IEnumerable<TItem> Rows { get; set; } = [];

    [Parameter] public Func<TItem, bool>? Filter { get; set; }

    [Parameter] public StringNumber? Height { get; set; }

    [Parameter] public EventCallback<Column> OnColumnEditClick { get; set; }

    [Parameter] public EventCallback<string> OnColumnToggle { get; set; }

    [Parameter] public EventCallback<(string ColumnId, double Width)> OnColumnResize { get; set; }

    [Parameter] public EventCallback<TItem> OnUpdate { get; set; }

    [Parameter] public EventCallback<TItem> OnDelete { get; set; }

    [Parameter] public EventCallback<TItem> OnAction1 { get; set; }

    [Parameter] public EventCallback<TItem> OnAction2 { get; set; }

    // ReSharper disable once StaticMemberInGenericType
    private static Block _block = new("masa-table-viewer");
    private ModifierBuilder _modifierBuilder = _block.CreateModifierBuilder();
    private StyleBuilder _styleBuilder = new();

    private bool _sized;
    private ElementReference _headerTrRef;

    private bool _imageViewer;
    private IList<string> _imagesToView = [];
    private MSimpleTable? _simpleTable;
    private Virtualize<TItem>? _virtualizeComponent;
    private string? _tableSelector;

    private HashSet<string>? _prevHiddenColumnIds;
    private List<string>? _prevColumnOrder;
    private Func<TItem, bool>? _prevFilter;

    private HashSet<EventCallback<TItem>> _actions = [];
    private StyleBuilder _headerColumnStyleBuilder = new();
    private IJSObjectReference? _tableJSObjectReference;
    private DotNetObjectReference<Viewer<TItem>>? _dotNetObjectReference;

    private int ActionsCount => _actions.Count(u => u.HasDelegate);

    private bool HasActions => ActionsCount > 0;

    private int RowHeightValue => RowHeight switch
    {
        RowHeight.Low => 42,
        RowHeight.Medium => 63,
        RowHeight.High => 105,
        _ => throw new ArgumentOutOfRangeException()
    };

    private IList<ColumnTemplate<TItem, object>> OrderedColumnTemplates = [];

    protected override void OnInitialized()
    {
        base.OnInitialized();

        _actions = [OnUpdate, OnDelete, OnAction1, OnAction2];

        _prevFilter = Filter;

        _dotNetObjectReference = DotNetObjectReference.Create(this);
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        OrderedColumnTemplates = ColumnTemplates.OrderBy(u => ColumnOrder.IndexOf(u.Column.Id)).ToList();

        if (_prevFilter != Filter)
        {
            _prevFilter = Filter;
            _virtualizeComponent?.RefreshDataAsync();
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            // TODO: dispose

            _tableJSObjectReference = await JSRuntime.InvokeAsync<IJSObjectReference>("import",
                "./_content/Masa.Blazor.MasaTable/MTemplateTable.razor.js");
            await _tableJSObjectReference.InvokeVoidAsync("init", _simpleTable.Ref,
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

    private ValueTask<ItemsProviderResult<TItem>> ItemsProvider(ItemsProviderRequest _)
    {
        if (Filter is not null)
        {
            var filteredRows = Rows.Where(Filter);
            return new ValueTask<ItemsProviderResult<TItem>>(
                new ItemsProviderResult<TItem>(filteredRows, filteredRows.Count()));
        }

        return new ValueTask<ItemsProviderResult<TItem>>(new ItemsProviderResult<TItem>(Rows, Rows.Count()));
    }

    private void OpenImageViewer(IList<string> images)
    {
        _imageViewer = true;
        _imagesToView = images;
    }

    private void OnImageViewerChanged()
    {
        if (_imageViewer == false)
        {
            _imagesToView = [];
        }
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
}