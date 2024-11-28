using BemIt;
using Masa.Blazor.Components.TemplateTable.DetailDialogs;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.RenderTree;

namespace Masa.Blazor.Components.TemplateTable.Viewers;

public partial class Viewer : IAsyncDisposable
{
    [Inject] private IJSRuntime JSRuntime { get; set; } = null!;

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

    [Parameter] public ICollection<IReadOnlyDictionary<string, JsonElement>> Rows { get; set; } = [];

    [Parameter] public Sort? Sort { get; set; }

    [Parameter] public EventCallback<Sort> OnSortUpdate { get; set; }

    /// <summary>
    /// The height of the table.
    /// </summary>
    [Parameter]
    public StringNumber? Height { get; set; }

    [Parameter] public bool HasActions { get; set; }

    [Parameter] public bool Detail { get; set; }

    [Parameter] public EventCallback<Column> OnColumnEditClick { get; set; }

    [Parameter] public EventCallback<string> OnColumnToggle { get; set; }

    [Parameter] public EventCallback<(string ColumnId, double Width)> OnColumnResize { get; set; }

    [Parameter] public EventCallback<List<string>> OnImagePreview { get; set; }

    [Parameter] public EventCallback<IReadOnlyDictionary<string, JsonElement>> OnDetail { get; set; }

    [Parameter] public EventCallback<IReadOnlyDictionary<string, JsonElement>> OnUpdate { get; set; }

    [Parameter] public EventCallback<IReadOnlyDictionary<string, JsonElement>> OnDelete { get; set; }

    [Parameter] public EventCallback<IReadOnlyDictionary<string, JsonElement>> OnAction1 { get; set; }

    [Parameter] public EventCallback<IReadOnlyDictionary<string, JsonElement>> OnAction2 { get; set; }

    [Parameter] public RenderFragment? ActionsContent { get; set; }

    // ReSharper disable once StaticMemberInGenericType
    private static Block _block = new("masa-table-viewer");
    private ModifierBuilder _modifierBuilder = _block.CreateModifierBuilder();
    private StyleBuilder _styleBuilder = new();

    private bool _sized;
    private ElementReference _headerTrRef;

    private MSimpleTable? _simpleTable;
    private string? _tableSelector;

    private HashSet<string>? _prevHiddenColumnIds;
    private List<string>? _prevColumnOrder;

    private StyleBuilder _headerColumnStyleBuilder = new();
    private IJSObjectReference? _tableJSObjectReference;
    private DotNetObjectReference<Viewer>? _dotNetObjectReference;

    private int _actionsCount = 0;

    private int RowHeightValue => RowHeight switch
    {
        RowHeight.Low => 42,
        RowHeight.Medium => 63,
        RowHeight.High => 105,
        _ => throw new ArgumentOutOfRangeException()
    };

    private IList<ViewColumnInfo> _orderedViewColumns = [];

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
        _actionsCount = builder.GetFrames().Array.Where(u => u.FrameType == RenderTreeFrameType.Component).Count();
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

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