namespace Masa.Blazor;

public partial class MDataTable<TItem> : MDataIterator<TItem>, IMobile
{
    [Inject]
    public MasaBlazor MasaBlazor { get; set; } = null!;

    [Parameter]
    public string? Caption { get; set; }

    [Parameter]
    public RenderFragment? CaptionContent { get; set; }

    [Parameter]
    public IEnumerable<DataTableHeader<TItem>> Headers { get; set; } = new List<DataTableHeader<TItem>>();

    [Parameter]
    public RenderFragment? TopContent { get; set; }

    [Parameter]
    public StringNumber? Height { get; set; }

    [Parameter]
    public bool FixedHeader { get; set; }

    [Parameter]
    public bool Dense { get; set; }

    [Parameter]
    public RenderFragment? FootContent { get; set; }

    [Parameter]
    public OneOf<Breakpoints, double> MobileBreakpoint { get; set; } = 600;

    [Parameter]
    public bool ShowGroupBy { get; set; }

    [Parameter]
    public string? GroupText { get; set; }

    [Parameter]
    public string? CheckboxColor { get; set; }

    [Parameter]
    public Dictionary<string, object> HeaderProps { get; set; } = new();

    [Parameter]
    public int HeadersLength { get; set; }

    [Parameter]
    public RenderFragment? BodyPrependContent { get; set; }

    [Parameter]
    public RenderFragment? BodyAppendContent { get; set; }

    [Parameter]
    public RenderFragment<DataTableGroupContext<TItem>>? GroupContent { get; set; }

    [Parameter]
    public RenderFragment<(IEnumerable<DataTableHeader<TItem>> Headers, TItem Item)>? ExpandedItemContent { get; set; }

    [Parameter]
    public Func<TItem, string?>? ItemClass { get; set; }

    [Parameter]
    public Func<object?, string?, TItem, bool> CustomItemFilter { get; set; } = DefaultFilter;

    [Parameter]
    public bool HideDefaultHeader { get; set; }

    [Parameter]
    public RenderFragment<DataTableHeader>? HeaderColContent { get; set; }

    [Parameter]
    public RenderFragment<ItemColProps<TItem>>? ItemColContent { get; set; }

    [Parameter]
    public bool ShowSelect { get; set; }

    [Parameter]
    public bool ShowExpand { get; set; }

    [Parameter]
    public RenderFragment<DataTableItemExpandOrSelectContext<TItem>>? ItemDataTableExpandContent { get; set; }

    [Parameter]
    [MasaApiParameter("$expand")]
    public string ExpandIcon { get; set; } = "$expand";

    [Parameter]
    public RenderFragment<DataTableItemExpandOrSelectContext<TItem>>? ItemDataTableSelectContent { get; set; }

    [Parameter]
    public RenderFragment<DataTableGroupHeaderContext<TItem>>? GroupHeaderContent { get; set; }

    [Parameter]
    public bool Stripe { get; set; }

    [Parameter]
    public StringNumber? Width { get; set; }

    [Parameter]
    public EventCallback<DataTableRowMouseEventArgs<TItem>> OnRowClick { get; set; }

    [Obsolete("Use OnRowDblClick instead.")]
    [Parameter]
    public EventCallback<DataTableRowMouseEventArgs<TItem>> OnRowDbClick { get; set; }

    [Parameter]
    public EventCallback<DataTableRowMouseEventArgs<TItem>> OnRowDblClick { get; set; }

    [Parameter]
    public EventCallback<DataTableRowMouseEventArgs<TItem>> OnRowContextmenu { get; set; }

    [Parameter]
    public bool OnRowContextmenuPreventDefault { get; set; }

    [Parameter]
    [MasaApiParameter(ReleasedOn = "v1.0.4")]
    public DataTableResizeMode ResizeMode
    {
        get => GetValue(DataTableResizeMode.None);
        set => SetValue(value);
    }

    protected MobileProvider? MobileProvider { get; set; }

    public IEnumerable<DataTableHeader<TItem>> ComputedHeaders
    {
        get
        {
            if (Headers == null)
            {
                return new List<DataTableHeader<TItem>>();
            }

            var headers = Headers
                          .Where(header => header.Value == null || InternalOptions.GroupBy.FirstOrDefault(by => by == header.Value) == null)
                          .ToList();

            if (ShowSelect)
            {
                var index = headers.FindIndex(r => r.Value == "data-table-select");
                if (index < 0)
                {
                    headers.Insert(0, new DataTableHeader<TItem>
                    {
                        Width = "1px",
                        Value = "data-table-select"
                    });
                }
                else
                {
                    var header = headers[index];
                    if (header.Width == null)
                    {
                        header.Width = "1px";
                    }
                }
            }

            if (ShowExpand)
            {
                var index = headers.FindIndex(r => r.Value == "data-table-expand");
                if (index < 0)
                {
                    headers.Insert(0, new DataTableHeader<TItem>
                    {
                        Width = "1px",
                        Value = "data-table-expand"
                    });
                }
                else
                {
                    var header = headers[index];
                    if (header.Width == null)
                    {
                        header.Width = "1px";
                    }
                }
            }

            return headers;
        }
    }

    public bool HasBottom => FooterContent != null || !HideDefaultFooter;

    public bool IsMobile => MobileProvider?.IsMobile ?? false;

    public Dictionary<string, object?> ColspanAttrs => new()
    {
        { "colspan", IsMobile ? null : (HeadersLength > 0 ? HeadersLength : ComputedHeaders.Count()) }
    };

    public List<DataTableHeader<TItem>> HeadersWithCustomFilters => Headers.Where(header => header.Filter != null && header.Filterable).ToList();

    public List<DataTableHeader<TItem>> HeadersWithoutCustomFilters
        => Headers.Where(header => header.Filter == null && header.Filterable).ToList();

    public Dictionary<string, bool> OpenCache { get; } = new();

    public DataOptions Options => InternalOptions;

    public override Task SetParametersAsync(ParameterView parameters)
    {
        GroupText ??= I18n.T("$masaBlazor.dataTable.groupText", defaultValue: "group");
        return base.SetParametersAsync(parameters);
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        CustomFilter = CustomFilterWithColumns;
        ItemValues = Headers.Select(header => new ItemValue<TItem>(header.Value));

        MobileProvider = new MobileProvider(this);
        MasaBlazor.Breakpoint.OnUpdate += BreakpointOnOnUpdate;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            if (ResizeMode != DataTableResizeMode.None)
            {
                await NextTickIf(async () =>
                {
                    var @ref = RefBack.Current;

                    await JsInvokeAsync(JsInteropConstants.ResizableDataTable, @ref);
                }, () => RefBack.Current.Context is null);
            }
        }
    }

    protected override void RegisterWatchers(PropertyWatcher watcher)
    {
        base.RegisterWatchers(watcher);

        watcher
            .Watch<DataOptions>(nameof(InternalOptions), (value, prevValue) =>
            {
                if (ResizeMode != DataTableResizeMode.None && value?.ItemsPerPage != prevValue?.ItemsPerPage)
                {
                    NextTick(() => { _ = JsInvokeAsync(JsInteropConstants.UpdateDataTableResizeHeight, RefBack.Current); });
                }
            })
            .Watch<DataTableResizeMode>(nameof(ResizeMode), (value, prevValue) =>
            {
                if (prevValue == DataTableResizeMode.None)
                {
                    NextTick(() => { _ = JsInvokeAsync(JsInteropConstants.ResizableDataTable, RefBack.Current); });
                }
            });
    }

    private string GetText(string value)
    {
        return Headers.FirstOrDefault(h => h.Value == value)?.Text ?? value;
    }

    private async void BreakpointOnOnUpdate(object? sender, BreakpointChangedEventArgs e)
    {
        MobileProvider = new MobileProvider(this);
        await InvokeStateHasChangedAsync();
    }

    public Task HandleOnRowClickAsync(MouseEventArgs args, TItem item)
    {
        var rowMouseEventArgs = new DataTableRowMouseEventArgs<TItem>(item, IsMobile, IsSelected(item), IsExpanded(item), args);
        return OnRowClick.InvokeAsync(rowMouseEventArgs);
    }

    public Task HandleOnRowContextmenuAsync(MouseEventArgs args, TItem item)
    {
        var rowMouseEventArgs = new DataTableRowMouseEventArgs<TItem>(item, IsMobile, IsSelected(item), IsExpanded(item), args);
        return OnRowContextmenu.InvokeAsync(rowMouseEventArgs);
    }

    public Task HandleOnRowDblClickAsync(MouseEventArgs args, TItem item)
    {
        var rowMouseEventArgs = new DataTableRowMouseEventArgs<TItem>(item, IsMobile, IsSelected(item), IsExpanded(item), args);
        var e = OnRowDblClick.HasDelegate ? OnRowDblClick : OnRowDbClick;
        return e.InvokeAsync(rowMouseEventArgs); }

    private IEnumerable<TItem> CustomFilterWithColumns(IEnumerable<TItem> items, IEnumerable<ItemValue<TItem>> filter, string? search)
    {
        return SearchTableItems(items, search, HeadersWithCustomFilters, HeadersWithoutCustomFilters, CustomItemFilter);
    }

    private IEnumerable<TItem> SearchTableItems(IEnumerable<TItem> items, string? search, List<DataTableHeader<TItem>> headersWithCustomFilters,
        List<DataTableHeader<TItem>> headersWithoutCustomFilters, Func<object?, string?, TItem, bool> customItemFilter)
    {
        search = search?.Trim();

        Func<DataTableHeader<TItem>, bool> FilterFunc(TItem item, string? s, Func<object?, string?, TItem, bool> filter)
        {
            return header =>
            {
                var value = header.ItemValue?.Invoke(item);
                return header.Filter?.Invoke(value, s, item) ?? filter(value, s, item);
            };
        }

        return items.Where(item =>
        {
            var matcherColumnFilters = headersWithCustomFilters.All(FilterFunc(item, search, DefaultFilter));
            var matchesSearchTerm = string.IsNullOrWhiteSpace(search) ||
                                    headersWithoutCustomFilters.Any(FilterFunc(item, search, customItemFilter));

            return matcherColumnFilters && matchesSearchTerm;
        });
    }

    protected override void SetComponentCss()
    {
        CssProvider.UseBem("m-data-table",
                       css => css.Modifiers(m => m.Modifier("mobile", IsMobile)
                                                  .Add("resizable", ResizeMode != DataTableResizeMode.None)
                                                  .Add("resizable-overflow", ResizeMode == DataTableResizeMode.Overflow)
                                                  .Add("resizable-independent", ResizeMode == DataTableResizeMode.Independent)
                                                  .Add("rtl", MasaBlazor.RTL)))
                   .Element("progress")
                   .Apply("column", css => css.Add("column"))
                   .Element("empty-wrapper")
                   .Element("expanded", css => css.Add("m-data-table__expanded__content"))
                   .Apply("group-header", css => css.Add("text-start"));
    }

    public void ToggleGroup(string group)
    {
        var open = OpenCache.GetValueOrDefault(group);
        OpenCache[group] = !open;
    }

    public void RemoveGroup()
    {
        UpdateOptions(options =>
        {
            options.GroupBy = new List<string>();
            options.GroupDesc = new List<bool>();
        });
    }

    protected override ValueTask DisposeAsyncCore()
    {
        MasaBlazor.Breakpoint.OnUpdate -= BreakpointOnOnUpdate;

        return base.DisposeAsyncCore();
    }
}
