using System.Runtime.CompilerServices;

namespace Masa.Blazor;

public record GridstackItem<TItem>(TItem Item, GridstackWidget Options);

public partial class MGridstack<TItem> : MasaComponentBase
{
    [Parameter, EditorRequired]
    public List<TItem> Items { get; set; } = new();

    [Parameter, EditorRequired]
    public RenderFragment<TItem> ItemContent { get; set; } = null!;

    [Parameter, EditorRequired]
    public Func<TItem, string> ItemKey { get; set; } = null!;

    [Parameter, Obsolete("Use ItemWidget instead.")]
    public Func<TItem, GridstackWidgetPosition>? ItemPosition { get; set; }

    [Parameter, EditorRequired, MasaApiParameter(ReleasedIn = "v1.10.0")]
    public Func<TItem, GridstackWidget>? ItemOptions { get; set; }

    [Parameter]
    public string? ItemClass { get; set; }

    [Parameter]
    public string? ItemStyle { get; set; }

    [Parameter]
    public bool Readonly
    {
        get => GetValue(false);
        set => SetValue(value);
    }

    /// <summary>
    /// Integer > 0 (default 12) which can change on the fly with column(N) API, or 'auto' for nested grids to size themselves to the parent grid container (to make sub-items are the same size). 
    /// </summary>
    [Parameter]
    [MasaApiParameter(12)]
    public int Column { get; set; } = 12;

    /// <summary>
    /// disables the oneColumnMode when the grid width is less than minW (default: 'false')
    /// </summary>
    [Parameter]
    public bool DisableOneColumnMode { get; set; }

    /// <summary>
    /// disallows resizing of widgets (default: false).
    /// </summary>
    [Parameter]
    public bool DisableResize { get; set; }

    /// <summary>
    /// enable floating widgets (default: false)
    /// </summary>
    [Parameter]
    public bool Float { get; set; }

    /// <summary>
    /// gap size around grid item and content (default: 10px)
    /// </summary>
    [Parameter]
    [MasaApiParameter(10)]
    public int Margin { get; set; } = 10;

    /// <summary>
    /// minimum rows amount which is handy to prevent grid from collapsing when empty. Default is 0. You can also do this with min-height CSS attribute on the grid div in pixels, which will round to the closest row.
    /// </summary>
    [Parameter]
    public int MinRow { get; set; }

    /// <summary>
    /// if true turns grid to RTL.
    /// </summary>
    [Parameter]
    public bool Rtl { get; set; }

    [Parameter]
    public EventCallback<GridstackResizeEventArgs> OnResize { get; set; }

    [Parameter]
    [MasaApiParameter("auto", ReleasedIn = "v1.10.0")]
    public StringNumber? CellHeight { get; set; } = "auto";

    [Parameter]
    [MasaApiParameter(ReleasedIn = "v1.10.0")]
    public bool SizeToContent { get; set; }

    [Parameter]
    [MasaApiParameter(GridstackResizeHandle.SouthEast, ReleasedIn = "v1.10.0")]
    public GridstackResizeHandle ResizeHandle { get; set; } = GridstackResizeHandle.SouthEast;

    [Parameter]
    [MasaApiParameter(true, ReleasedIn = "v1.10.0")]
    public bool ResizeAutoHide { get; set; } = true;

    private static Block _block = new("m-gridstack");
    private static Block _itemBlock = _block.Extend("item");

    private HashSet<string> _prevItemKeys = [];
    private HashSet<GridstackItem<TItem>> _itemOptions = new();

    private GridstackJSModule? _gridstackInstance;

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        await base.SetParametersAsync(parameters);

        Items.ThrowIfNull(ComponentName);
        ItemKey.ThrowIfNull(ComponentName);
        ItemContent.ThrowIfNull(ComponentName);
    }

    protected override void RegisterWatchers(PropertyWatcher watcher)
    {
        base.RegisterWatchers(watcher);

        watcher.Watch<bool>(nameof(Readonly), (val) => { _ = SetStatic(val); });
    }

    protected override IEnumerable<string> BuildComponentClass()
    {
        yield return _block.Name;
        yield return "grid-stack";
    }

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        if (Column < 0) Column = 12;

        var itemKeys = Items.Select(ItemKey).ToHashSet();
        if (!_prevItemKeys.SetEquals(itemKeys))
        {
            _prevItemKeys = itemKeys;
            _itemOptions = Items.Select(item =>
            {
                if (ItemOptions is not null)
                {
                    var options = ItemOptions(item);
                    options.Id = ItemKey(item);
                    return new GridstackItem<TItem>(item, options);
                }

                if (ItemPosition is not null)
                {
                    var position = ItemPosition(item);
                    return new GridstackItem<TItem>(item, new GridstackWidget()
                    {
                        Id = ItemKey(item),
                        X = position.X,
                        Y = position.Y,
                        H = position.H,
                        W = position.W
                    });
                }

                return new GridstackItem<TItem>(item, new GridstackWidget());
            }).ToHashSet();

            NextTick(async () => { await Reload(); });
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            _prevItemKeys = Items.Select(ItemKey).ToHashSet();

            var options = new GridstackOptions()
            {
                Column = Column,
                DisableOneColumnMode = DisableOneColumnMode,
                DisableResize = DisableResize,
                Float = Float,
                Margin = Margin,
                MinRow = MinRow,
                Rtl = Rtl,
                CellHeight = FormatCellHeight(CellHeight),
                SizeToContent = SizeToContent,
                Resizable = new GridstackResizable(ResizeAutoHide, ResizeHandle)
            };

            _gridstackInstance = new GridstackJSModule(Js);
            await _gridstackInstance.Init(options, Ref);

            _gridstackInstance.Resize += GridstackOnResize;

            if (Readonly)
            {
                await SetStatic(true);
            }
        }
    }

    private string FormatCellHeight(StringNumber? cellHeight)
    {
        if (cellHeight is null)
        {
            return "auto";
        }

        if (cellHeight.IsT1 || cellHeight.IsT2)
        {
            return cellHeight.ToDouble() == 0 ? "0" : $"{cellHeight}px";
        }

        return cellHeight.AsT0;
    }

    public async ValueTask<List<GridstackWidget>> OnSave()
    {
        if (_gridstackInstance is null) return [];

        return await _gridstackInstance.Save();
    }

    private void GridstackOnResize(object? sender, GridstackResizeEventArgs e)
    {
        if (OnResize.HasDelegate)
        {
            OnResize.InvokeAsync(e);
        }
    }

    public async Task Reload()
    {
        if (_gridstackInstance is null) return;
        await _gridstackInstance.Reload();
    }

    private async Task SetStatic(bool staticValue)
    {
        if (_gridstackInstance is null) return;
        await _gridstackInstance.SetStatic(staticValue);
    }

    protected override async ValueTask DisposeAsyncCore()
    {
        if (_gridstackInstance is null)
        {
            return;
        }

        _gridstackInstance.Resize -= GridstackOnResize;
        await (_gridstackInstance as IAsyncDisposable).DisposeAsync();
        _gridstackInstance = null;
    }
}