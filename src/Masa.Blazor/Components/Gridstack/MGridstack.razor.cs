using BlazorComponent.Attributes;
using Microsoft.JSInterop;

namespace Masa.Blazor;

public partial class MGridstack<TItem> : BDomComponentBase, IAsyncDisposable
{
    [Inject]
    protected GridstackJSModule Module { get; set; } = null!;

    [Parameter, EditorRequired]
    public List<TItem> Items { get; set; } = new();

    [Parameter, EditorRequired]
    public RenderFragment<TItem> ItemContent { get; set; } = null!;

    [Parameter, EditorRequired]
    public Func<TItem, string> ItemKey { get; set; } = null!;

    [Parameter]
    public Func<TItem, GridstackWidgetPosition>? ItemPosition { get; set; }

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
    [MassApiParameter(12)]
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
    [MassApiParameter(10)]
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

    private string? _prevItemKeys;
    private IJSObjectReference? _gridstackInstance;

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

    protected override void SetComponentClass()
    {
        base.SetComponentClass();

        CssProvider.Apply(cssBuilder => cssBuilder.Add("m-gridstack"))
                   .Apply("item",
                       cssBuilder => cssBuilder.Add("m-gridstack-item").Add(ItemClass),
                       styleBuilder => styleBuilder.Add(ItemStyle));
    }

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        if (Column < 0) Column = 12;

        var itemKeys = string.Join("", Items.Select(ItemKey));
        if (_prevItemKeys is not null && _prevItemKeys != itemKeys)
        {
            _prevItemKeys = itemKeys;
            NextTick(async () => { await Reload(); });
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            _prevItemKeys = string.Join("", Items.Select(ItemKey));

            var options = new GridstackOptions()
            {
                Column = Column,
                DisableOneColumnMode = DisableOneColumnMode,
                DisableResize = DisableResize,
                Float = Float,
                Margin = Margin,
                MinRow = MinRow,
                Rtl = Rtl
            };

            _gridstackInstance = await Module.Init(options, Ref);

            Module.Resize += GridstackOnResize;

            if (Readonly)
            {
                await SetStatic(true);
            }
        }
    }

    public async ValueTask<List<GridstackWidget>> OnSave()
    {
        if (_gridstackInstance is null) return new List<GridstackWidget>();

        return await Module.Save(_gridstackInstance);
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
        _gridstackInstance = await Module.Reload(_gridstackInstance);
    }

    private async Task SetStatic(bool staticValue)
    {
        if (_gridstackInstance is null) return;
        await Module.SetStatic(_gridstackInstance, staticValue);
    }

    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        try
        {
            Module.Resize -= GridstackOnResize;
            if (_gridstackInstance is not null)
            {
                await _gridstackInstance.DisposeAsync();
            }
        }
        catch
        {
            // ignored
        }
    }
}
