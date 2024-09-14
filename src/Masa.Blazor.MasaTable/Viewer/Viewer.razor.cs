using BemIt;
using Masa.Blazor.Core;
using Masa.Blazor.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Masa.Blazor.MasaTable;

public partial class Viewer<TItem> : IAsyncDisposable
{
    [Inject] private IJSRuntime JSRuntime { get; set; } = null!;

    [Parameter] public List<ColumnTemplate<TItem, object>> ColumnTemplates { get; set; } = [];

    [Parameter] public List<string> ColumnOrder { get; set; } = [];

    [Parameter] public EventCallback<List<string>> ColumnOrderChanged { get; set; }

    [Parameter] public HashSet<string> HiddenColumnIds { get; set; } = [];

    [Parameter] public RowHeight RowHeight { get; set; }

    [Parameter] public IEnumerable<TItem> Rows { get; set; } = [];

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

    private bool _sized;

    private bool _imageViewer;
    private IList<string> _imagesToView = [];
    private MSimpleTable? _simpleTable;
    private string? _tableSelector;

    private HashSet<string>? _prevHiddenColumnIds;
    private List<string>? _prevColumnOrder;

    private HashSet<EventCallback<TItem>> _actions = [];
    private StyleBuilder _headerColumnStyleBuilder = new();
    private IJSObjectReference? _tableJSObjectReference;
    private DotNetObjectReference<Viewer<TItem>>? _dotNetObjectReference;

    private int ActionsCount => _actions.Count(u => u.HasDelegate);

    private bool HasActions => ActionsCount > 0;

    private IList<ColumnTemplate<TItem, object>> _visibleColumnTemplates = [];
    private IList<ColumnTemplate<TItem, object>> OrderedColumnTemplates = [];

    protected override void OnInitialized()
    {
        base.OnInitialized();

        _actions = [OnUpdate, OnDelete, OnAction1, OnAction2];

        _dotNetObjectReference = DotNetObjectReference.Create(this);
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (_prevHiddenColumnIds is null || _prevHiddenColumnIds.SetEquals(HiddenColumnIds) is false)
        {
            _prevHiddenColumnIds = HiddenColumnIds;
            _visibleColumnTemplates = ColumnTemplates.Where(c => !HiddenColumnIds.Contains(c.Column.Id)).ToList();
        }

        if (_prevColumnOrder is null || _prevColumnOrder.SequenceEqual(ColumnOrder) is false)
        {
            _prevColumnOrder = ColumnOrder;
            OrderedColumnTemplates = _visibleColumnTemplates.OrderBy(u => ColumnOrder.IndexOf(u.Column.Id)).ToList();
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
            await _tableJSObjectReference.InvokeVoidAsync("resizableDataTable", _simpleTable.Ref,
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
                await _tableJSObjectReference.DisposeAsync().ConfigureAwait(false);
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