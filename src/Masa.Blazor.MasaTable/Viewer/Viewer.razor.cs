using Masa.Blazor.Core;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Masa.Blazor.MasaTable;

public partial class Viewer<TItem> : IAsyncDisposable
{
    [Inject] private IJSRuntime JSRuntime { get; set; } = null!;

    [Parameter] public List<ColumnTemplate<TItem, object>> ColumnTemplates { get; set; } = [];

    [Parameter] public IList<string> ColumnOrder { get; set; } = [];

    [Parameter] public HashSet<string> HiddenColumnIds { get; set; } = [];

    [Parameter] public RowHeight RowHeight { get; set; }

    [Parameter] public IEnumerable<TItem> Rows { get; set; } = [];

    [Parameter] public StringNumber? Height { get; set; }

    [Parameter] public EventCallback<Column> OnColumnEditClick { get; set; }

    [Parameter] public EventCallback<string> OnColumnToggle { get; set; }

    [Parameter] public EventCallback<TItem> OnUpdate { get; set; }

    [Parameter] public EventCallback<TItem> OnDelete { get; set; }

    [Parameter] public EventCallback<TItem> OnAction1 { get; set; }

    [Parameter] public EventCallback<TItem> OnAction2 { get; set; }

    private bool _imageViewer;
    private IList<string> _imagesToView = [];
    private MSimpleTable? _simpleTable;

    private HashSet<EventCallback<TItem>> _actions = [];
    private StyleBuilder _headerColumnStyleBuilder = new();
    private IJSObjectReference? _tableJSObjectReference;

    private int ActionsCount => _actions.Count(u => u.HasDelegate);

    private bool HasActions => ActionsCount > 0;

    private IEnumerable<ColumnTemplate<TItem, object>> ComputedColumnTemplates
    {
        get
        {
            return ColumnTemplates.Where(c => !HiddenColumnIds.Contains(c.Column.Id))
                .OrderBy(u => ColumnOrder.IndexOf(u.Column.Id));
        }
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        _actions = [OnUpdate, OnDelete, OnAction1, OnAction2];
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            // TODO: dispose
            
            // TODO: 隐藏的列显示时需要去注册reisze
            
            _tableJSObjectReference = await JSRuntime.InvokeAsync<IJSObjectReference>("import",
                "./_content/Masa.Blazor.MasaTable/MConfigurableTable.razor.js");
            await _tableJSObjectReference.InvokeVoidAsync("resizableDataTable", _simpleTable.Ref);
        }
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