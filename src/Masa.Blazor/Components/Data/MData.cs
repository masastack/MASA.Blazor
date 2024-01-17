namespace Masa.Blazor;

public abstract class MData<TItem> : BDomComponentBase
{
    [Parameter]
    [EditorRequired]
    public IEnumerable<TItem> Items
    {
        get => GetValue<IEnumerable<TItem>>() ?? new List<TItem>();
        set => SetValue(value);
    }

    [Parameter]
    public OneOf<string, IList<string>> SortBy
    {
        get => GetValue<OneOf<string, IList<string>>>(new List<string>());
        set => SetValue(value);
    }

    [Parameter]
    public OneOf<bool, IList<bool>> SortDesc
    {
        get => GetValue<OneOf<bool, IList<bool>>>(new List<bool>());
        set => SetValue(value);
    }

    [Parameter]
    public Func<IEnumerable<TItem>, IEnumerable<ItemValue<TItem>>, IList<string>, List<bool>, string, IEnumerable<TItem>> CustomSort
    {
        get => _customSort ?? DefaultSortItems;
        set => _customSort = value;
    }

    [Parameter]
    public bool MustSort
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    [Parameter]
    public bool MultiSort
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    [Parameter]
    [MasaApiParameter(1)]
    public int Page
    {
        get => GetValue(1);
        set => SetValue(value);
    }

    [Parameter]
    [MasaApiParameter(10)]
    public int ItemsPerPage
    {
        get => GetValue(10);
        set => SetValue(value);
    }

    [Parameter]
    public OneOf<string, IList<string>> GroupBy { get; set; } = new List<string>();

    [Parameter]
    public IList<bool> GroupDesc { get; set; } = new List<bool>();

    [Parameter]
    public Func<IEnumerable<TItem>, IEnumerable<ItemValue<TItem>>, IList<string>, IList<bool>, IEnumerable<IGrouping<string, TItem>>> CustomGroup
    {
        get => _customGroup ?? DefaultGroupItems;
        set => _customGroup = value;
    }

    // TODO: check if this is implemented correctly
    [Parameter]
    [MasaApiParameter("en-US")]
    public string Locale { get; set; } = "en-US";

    [Parameter]
    public bool DisableSort { get; set; }

    [Parameter]
    public bool DisablePagination
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    [Parameter]
    public bool DisableFiltering
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    [Parameter]
    public string? Search
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    [Parameter]
    public Func<IEnumerable<TItem>, IEnumerable<ItemValue<TItem>>, string?, IEnumerable<TItem>> CustomFilter
    {
        get => _customFilter ?? DefaultSearchItems;
        set => _customFilter = value;
    }

    [Parameter]
    public int ServerItemsLength
    {
        get => GetValue(-1);
        set => SetValue(value);
    }

    [Parameter]
    public IEnumerable<ItemValue<TItem>> ItemValues { get; set; } = new List<ItemValue<TItem>>();

    [Parameter]
    public EventCallback<int> OnPageCount { get; set; }

    [Parameter]
    public EventCallback<DataOptions> OnOptionsUpdate { get; set; }

    private Func<IEnumerable<TItem>, IEnumerable<ItemValue<TItem>>, string?, IEnumerable<TItem>>? _customFilter;

    private Func<IEnumerable<TItem>, IEnumerable<ItemValue<TItem>>, IList<string>, List<bool>, string, IEnumerable<TItem>>? _customSort;

    private Func<IEnumerable<TItem>, IEnumerable<ItemValue<TItem>>, IList<string>, IList<bool>, IEnumerable<IGrouping<string, TItem>>>?
        _customGroup;

    protected DataOptions InternalOptions
    {
        get => GetValue(new DataOptions())!;
        set => SetValue(value);
    }

    public DataPagination Pagination => new()
    {
        Page = InternalOptions.Page,
        ItemsPerPage = InternalOptions.ItemsPerPage,
        PageStart = PageStart,
        PageStop = PageStop,
        PageCount = PageCount,
        ItemsLength = ItemsLength
    };

    public int PageStart
    {
        get
        {
            if (InternalOptions.ItemsPerPage == -1 || !Items.Any())
            {
                return 0;
            }

            return (InternalOptions.Page - 1) * InternalOptions.ItemsPerPage;
        }
    }

    public IEnumerable<TItem> FilteredItems
    {
        get
        {
            return GetComputedValue(() =>
            {
                var items = new List<TItem>(Items);

                if (!DisableFiltering && ServerItemsLength <= 0 && ItemValues != null)
                {
                    return CustomFilter(items, ItemValues, Search);
                }

                return items;
            }, new[]
            {
                nameof(DisableFiltering),
                nameof(ServerItemsLength),
                nameof(ItemValues),
                nameof(Items),
                nameof(Search)
            })!;
        }
    }

    public int ItemsLength
    {
        get
        {
            return GetComputedValue(() =>
                ServerItemsLength >= 0 ? ServerItemsLength : FilteredItems.Count());
        }
    }

    public int PageStop
    {
        get
        {
            if (InternalOptions.ItemsPerPage == -1)
            {
                return ItemsLength;
            }

            if (!Items.Any())
            {
                return 0;
            }

            return Math.Min(ItemsLength, InternalOptions.Page * InternalOptions.ItemsPerPage);
        }
    }

    public int PageCount
    {
        get
        {
            return GetComputedValue(() =>
                InternalOptions.ItemsPerPage <= 0 ? 1 : (int)Math.Ceiling(ItemsLength / (InternalOptions.ItemsPerPage * 1.0)));
        }
    }

    public static IEnumerable<TItem> DefaultSearchItems(IEnumerable<TItem> items, IEnumerable<ItemValue<TItem>> itemValues, string? search)
    {
        if (string.IsNullOrWhiteSpace(search))
        {
            return items;
        }

        search = search.ToLowerInvariant();
        return items.Where(item => itemValues.Any(itemValue => DefaultFilter(itemValue.Invoke(item), search, item)));
    }

    public static bool DefaultFilter(object? value, string? search, TItem item)
    {
        return value?.ToString() != null && search != null &&
               value.ToString()!.ToLowerInvariant().IndexOf(search.ToLowerInvariant(), StringComparison.Ordinal) != -1;
    }

    public static IEnumerable<TItem> DefaultSortItems(IEnumerable<TItem> items, IEnumerable<ItemValue<TItem>> itemValues, IList<string> sortBy,
        List<bool> sortDesc, string locale)
    {
        var sortedItems = default(IOrderedEnumerable<TItem>);

        for (var i = 0; i < sortBy.Count; i++)
        {
            var itemValue = itemValues.FirstOrDefault(itemValue => itemValue == sortBy[i]);
            if (itemValue?.Factory == null)
            {
                continue;
            }

            var selector = itemValue.Factory;
            var desc = sortDesc.ElementAtOrDefault(i);

            if (i == 0)
            {
                sortedItems = !desc ? items.OrderBy(selector) : items.OrderByDescending(selector);
            }
            else
            {
                sortedItems = !desc ? sortedItems!.ThenBy(selector) : sortedItems!.ThenByDescending(selector);
            }
        }

        return sortedItems ?? items;
    }

    private static IEnumerable<IGrouping<string, TItem>> DefaultGroupItems(IEnumerable<TItem> items, IEnumerable<ItemValue<TItem>> itemValues,
        IList<string> groupBy, IList<bool> groupDesc)
    {
        var key = groupBy.FirstOrDefault();
        var itemValue = itemValues.FirstOrDefault(itemValue => itemValue == key);
        if (itemValue == null || key == null)
        {
            return new List<IGrouping<string, TItem>>();
        }

        return items.GroupBy(item => itemValue.Invoke(item).ToString());
    }

    public bool IsGrouped => InternalOptions.GroupBy.Count > 0;

    public IEnumerable<IGrouping<string, TItem>> GroupedItems
        => IsGrouped ? GroupItems(ComputedItems) : Enumerable.Empty<IGrouping<string, TItem>>();

    public IEnumerable<TItem> ComputedItems
    {
        get
        {
            return GetComputedValue(() =>
            {
                IEnumerable<TItem> items = new List<TItem>(FilteredItems);

                if ((!DisableSort || InternalOptions.GroupBy.Count > 0) && ServerItemsLength <= 0)
                {
                    items = SortItems(items);
                }

                if (!DisablePagination && ServerItemsLength <= 0)
                {
                    items = PaginateItems(items);
                }

                return items;
            }, new[]
            {
                nameof(FilteredItems),
                nameof(DisableSort),
                nameof(InternalOptions),
                nameof(ServerItemsLength),
                nameof(DisablePagination)
            })!;
        }
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        UpdateOptions(options =>
        {
            options.Page = Page;
            options.ItemsPerPage = ItemsPerPage;
            options.SortBy = WrapperInArray(SortBy);
            options.SortDesc = WrapperInArray(SortDesc);
            options.GroupBy = WrapperInArray(GroupBy);
            options.GroupDesc = GroupDesc;
            options.MustSort = MustSort;
            options.MultiSort = MultiSort;

            var sortDiff = InternalOptions.SortBy.Count - InternalOptions.SortDesc.Count;
            var groupDiff = InternalOptions.GroupBy.Count - InternalOptions.GroupDesc.Count;

            if (sortDiff > 0)
            {
                for (int i = 0; i < sortDiff; i++)
                {
                    InternalOptions.SortDesc.Add(false);
                }
            }

            if (groupDiff > 0)
            {
                for (int i = 0; i < groupDiff; i++)
                {
                    InternalOptions.GroupDesc.Add(false);
                }
            }
        }, false);
    }

    protected override void RegisterWatchers(PropertyWatcher watcher)
    {
        base.RegisterWatchers(watcher);

        watcher
            .Watch<int>(nameof(Page), value => { InternalOptions.Page = value; })
            .Watch<int>(nameof(ItemsPerPage), value => { InternalOptions.ItemsPerPage = value; })
            .Watch<bool>(nameof(MultiSort), value => { InternalOptions.MultiSort = value; })
            .Watch<bool>(nameof(MustSort), value => { InternalOptions.MustSort = value; })
            .Watch<int>(nameof(PageCount), value => { OnPageCount.InvokeAsync(value); })
            .Watch<OneOf<string, IList<string>>>(nameof(SortBy), val => { InternalOptions.SortBy = WrapperInArray(val); })
            .Watch<OneOf<bool, IList<bool>>>(nameof(SortDesc), val => { InternalOptions.SortDesc = WrapperInArray(val); });
    }

    protected IList<TValue> WrapperInArray<TValue>(OneOf<TValue, IList<TValue>> val)
    {
        if (val.Value == null)
        {
            return new List<TValue>();
        }

        return val.Match(
            t1 => new List<TValue> { t1 },
            t2 => t2);
    }

    public(IList<string> by, IList<bool> desc, int page) Toggle(string key, IList<string> oldBy, IList<bool> oldDesc, int page, bool mustSort,
        bool multiSort)
    {
        var by = oldBy;
        var desc = oldDesc;
        var byIndex = oldBy.IndexOf(key);

        if (byIndex < 0)
        {
            if (!multiSort)
            {
                by = new List<string>();
                desc = new List<bool>();
            }

            by.Add(key);
            desc.Add(false);
        }
        else if (!desc[byIndex])
        {
            desc[byIndex] = true;
        }
        else if (!mustSort)
        {
            by.RemoveAt(byIndex);
            desc.RemoveAt(byIndex);
        }
        else
        {
            desc[byIndex] = false;
        }

        //TODO:reset page to 1

        return (by, desc, page);
    }

    public void Group(string key)
    {
        var (groupBy, groupDesc, page) = Toggle(key, InternalOptions.GroupBy, InternalOptions.GroupDesc, InternalOptions.Page, true, false);

        UpdateOptions(options =>
        {
            options.GroupDesc = groupDesc;
            options.GroupBy = groupBy;
            options.Page = page;
        });
    }

    public void Sort(OneOf<string, List<string>> key)
    {
        if (key.IsT1)
        {
            SortArray(key.AsT1);
            return;
        }

        var (sortBy, sortDesc, page) = Toggle(key.AsT0, InternalOptions.SortBy, InternalOptions.SortDesc, InternalOptions.Page,
            InternalOptions.MustSort, InternalOptions.MultiSort);

        UpdateOptions(options =>
        {
            options.SortDesc = sortDesc;
            options.SortBy = sortBy;
            options.Page = page;
        });
    }

    public void SortArray(List<string> sortBy)
    {
        var sortDesc = sortBy.Select(s =>
        {
            var i = InternalOptions.SortBy.ToList().FindIndex(k => k == s);
            return i > -1 ? InternalOptions.SortDesc[i] : false;
        }).ToList();

        UpdateOptions(options =>
        {
            options.SortBy = sortBy;
            options.SortDesc = sortDesc;
        });
    }

    public void UpdateOptions(Action<DataOptions> options, bool emit = true)
    {
        options?.Invoke(InternalOptions);

        InternalOptions.Page = ServerItemsLength < 0
            ? Math.Max(1, Math.Min(InternalOptions.Page, PageCount))
            : InternalOptions.Page;

        if (OnOptionsUpdate.HasDelegate && emit)
        {
            OnOptionsUpdate.InvokeAsync(InternalOptions);
        }
    }

    public IEnumerable<IGrouping<string, TItem>> GroupItems(IEnumerable<TItem> items)
    {
        return CustomGroup(items, ItemValues, InternalOptions.GroupBy, InternalOptions.GroupDesc);
    }

    public IEnumerable<TItem> SortItems(IEnumerable<TItem> items)
    {
        var sortBy = new List<string>();
        var sortDesc = new List<bool>();

        if (!DisableSort)
        {
            sortBy = InternalOptions.SortBy.ToList();
            sortDesc = InternalOptions.SortDesc.ToList();
        }

        if (InternalOptions.GroupBy.Count > 0)
        {
            sortBy.InsertRange(0, InternalOptions.GroupBy);
            sortDesc.InsertRange(0, InternalOptions.GroupDesc);
        }

        return CustomSort(items, ItemValues, sortBy, sortDesc, Locale);
    }

    public IEnumerable<TItem> PaginateItems(IEnumerable<TItem> items)
    {
        var cacheItems = items.ToList();

        if (ServerItemsLength == -1 && cacheItems.Count <= PageStart)
        {
            InternalOptions.Page = Math.Max(1, (int)Math.Ceiling(cacheItems.Count / (InternalOptions.ItemsPerPage * 1.0)));
        }

        return cacheItems.Skip(PageStart).Take(PageStop - PageStart);
    }
}
