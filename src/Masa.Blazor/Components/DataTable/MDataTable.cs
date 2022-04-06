using Microsoft.AspNetCore.Components.Web;

namespace Masa.Blazor
{
    public class MDataTable<TItem> : MDataIterator<TItem>, IDataTable<TItem>, ILoadable, IDataIterator<TItem>
    {
        [Parameter]
        public string Caption { get; set; }

        [Parameter]
        public RenderFragment CaptionContent { get; set; }

        [Parameter]
        public IEnumerable<DataTableHeader<TItem>> Headers { get; set; } = new List<DataTableHeader<TItem>>();

        [Parameter]
        public RenderFragment TopContent { get; set; }

        [Parameter]
        public StringNumber Height { get; set; }

        [Parameter]
        public bool FixedHeader { get; set; }

        [Parameter]
        public bool FixedRight { get; set; }

        [Parameter]
        public bool Dense { get; set; }

        [Parameter]
        public RenderFragment FootContent { get; set; }

        [Parameter]
        public bool ShowGroupBy { get; set; }

        [Parameter]
        public string CheckboxColor { get; set; }

        [Parameter]
        public Dictionary<string, object> HeaderProps { get; set; } = new();

        [Parameter]
        public int HeadersLength { get; set; }

        [Parameter]
        public RenderFragment BodyPrependContent { get; set; }

        [Parameter]
        public RenderFragment BodyAppendContent { get; set; }

        [Parameter]
        public RenderFragment GroupContent { get; set; }

        [Parameter]
        public RenderFragment<(IEnumerable<DataTableHeader<TItem>> Headers, TItem Item)> ExpandedItemContent { get; set; }

        [Parameter]
        public string ItemClass { get; set; }

        [Parameter]
        public Func<object, string, TItem, bool> CustomItemFilter { get; set; } = DefaultFilter;

        [Parameter]
        public bool HideDefaultHeader { get; set; }

        [Parameter]
        public RenderFragment<DataTableHeader> HeaderColContent { get; set; }

        [Parameter]
        public RenderFragment<ItemColProps<TItem>> ItemColContent { get; set; }

        [Parameter]
        public bool ShowSelect { get; set; }

        [Parameter]
        public bool ShowExpand { get; set; }

        [Parameter]
        public RenderFragment ItemDataTableExpandContent { get; set; }

        [Parameter]
        public string ExpandIcon { get; set; } = "mdi-chevron-down";

        [Parameter]
        public RenderFragment ItemDataTableSelectContent { get; set; }

        [Parameter]
        public RenderFragment GroupHeaderContent { get; set; }

        [Parameter]
        public bool Stripe { get; set; }

        [Parameter]
        public StringNumber Width { get; set; }

        public IEnumerable<DataTableHeader<TItem>> ComputedHeaders
        {
            get
            {
                if (Headers == null)
                {
                    return new List<DataTableHeader<TItem>>();
                }

                var headers = Headers.Where(header => header.Value == null || InternalOptions.GroupBy.FirstOrDefault(by => by == header.Value) == null).ToList();

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

        public bool HasTop => TopContent != null;

        public bool HasBottom => FooterContent != null || !HideDefaultFooter;

        public Dictionary<string, object> ColspanAttrs => new()
        {
            { "colspan", HeadersLength > 0 ? HeadersLength : ComputedHeaders.Count() }
        };

        public List<DataTableHeader<TItem>> HeadersWithCustomFilters
        {
            get
            {
                return Headers.Where(header => header.Filter != null && header.Filterable).ToList();
            }
        }

        public List<DataTableHeader<TItem>> HeadersWithoutCustomFilters
        {
            get
            {
                return Headers.Where(header => header.Filter == null && header.Filterable).ToList();
            }
        }

        public Dictionary<string, bool> OpenCache { get; } = new();

        public string GroupMinusIcon { get; } = "mdi-minus";

        public string GroupCloseIcon { get; } = "mdi-close";

        public string GroupPlusIcon { get; } = "mdi-plus";

        //TODO:we will change this
        public DataOptions Options => InternalOptions;

        protected bool IsFixedRight => FixedRight && ComputedItems.Any();

        public Task HandleOnRowClickAsync(MouseEventArgs args)
        {
            return Task.CompletedTask;
        }

        public Task HandleOnRowContextMenuAsync(MouseEventArgs arg)
        {
            return Task.CompletedTask;
        }

        public Task HandleOnRowDbClickAsync(MouseEventArgs arg)
        {
            return Task.CompletedTask;
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            CustomFilter = CustomFilterWithColumns;
            ItemValues = Headers.Select(header => new ItemValue<TItem>(header.Value));
        }

        private IEnumerable<TItem> CustomFilterWithColumns(IEnumerable<TItem> items, IEnumerable<ItemValue<TItem>> filter, string search)
        {
            return SearchTableItems(items, search, HeadersWithCustomFilters, HeadersWithoutCustomFilters, CustomItemFilter);
        }

        private IEnumerable<TItem> SearchTableItems(IEnumerable<TItem> items, string search, List<DataTableHeader<TItem>> headersWithCustomFilters, List<DataTableHeader<TItem>> headersWithoutCustomFilters, Func<object, string, TItem, bool> customItemFilter)
        {
            search = search?.Trim();

            Func<DataTableHeader<TItem>, bool> FilterFunc(TItem item, string search, Func<object, string, TItem, bool> filter)
            {
                return header =>
                {
                    var value = header.ItemValue?.Invoke(item);
                    return header.Filter != null ? header.Filter(value, search, item) : filter(value, search, item);
                };
            }

            return items.Where(item =>
            {
                var matcherColumnFilters = headersWithCustomFilters.All(FilterFunc(item, search, DefaultFilter));
                var matchesSearchTerm = string.IsNullOrWhiteSpace(search) || headersWithoutCustomFilters.Any(FilterFunc(item, search, customItemFilter));

                return matcherColumnFilters && matchesSearchTerm;
            });
        }

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            CssProvider
                .Apply("progress", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-data-table__progress");
                })
                .Apply("column", cssBuilder =>
                {
                    cssBuilder
                        .Add("column");
                })
                .Apply("empty-wrapper", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-data-table__empty-wrapper");
                })
                .Apply("expanded-content", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-data-table__expanded m-data-table__expanded__content");
                })
                .Apply("group-header", cssBuilder =>
                {
                    cssBuilder
                        .Add("text-start");
                });

            AbstractProvider
                .ApplyDataTableDefault<TItem>()
                .Apply(typeof(BDataTableHeader), typeof(MDataTableHeader), attrs =>
                {
                    foreach (var prop in HeaderProps)
                    {
                        attrs[prop.Key] = prop.Value;
                    }

                    attrs[nameof(MDataTableHeader.Headers)] = ComputedHeaders.ToList<DataTableHeader>();
                    attrs[nameof(MDataTableHeader.Options)] = InternalOptions;
                    attrs[nameof(MDataTableHeader.ShowGroupBy)] = ShowGroupBy;
                    attrs[nameof(MDataTableHeader.CheckboxColor)] = CheckboxColor;
                    attrs[nameof(MDataTableHeader.SomeItems)] = SomeItems;
                    attrs[nameof(MDataTableHeader.EveryItem)] = EveryItem;
                    attrs[nameof(MDataTableHeader.SingleSelect)] = SingleSelect;
                    attrs[nameof(MDataTableHeader.DisableSort)] = DisableSort;
                    attrs[nameof(MDataTableHeader.HeaderColContent)] = HeaderColContent;
                    attrs[nameof(MDataTableHeader.OnToggleSelectAll)] = EventCallback.Factory.Create<bool>(this, ToggleSelectAll);
                    attrs[nameof(MDataTableHeader.OnSort)] = EventCallback.Factory.Create<string>(this, Sort);
                    attrs[nameof(MDataTableHeader.OnGroup)] = EventCallback.Factory.Create<string>(this, Group);
                })
                .Apply(typeof(BProgressLinear), typeof(MProgressLinear), attrs =>
                {
                    attrs[nameof(MProgressLinear.Absolute)] = true;
                    attrs[nameof(MProgressLinear.Color)] = (Loading == true || Loading == "") ? "primary" : Loading.AsT0;
                    attrs[nameof(MProgressLinear.Height)] = LoaderHeight;
                    attrs[nameof(MProgressLinear.Indeterminate)] = true;
                })
                .Apply<BSimpleTable, MSimpleTable>(attrs =>
                {
                    attrs[nameof(Height)] = Height;
                    attrs[nameof(FixedHeader)] = FixedHeader;
                    attrs[nameof(Dense)] = Dense;
                    attrs[nameof(Class)] = Class;
                    attrs[nameof(Style)] = Style;
                    attrs[nameof(FixedRight)] = IsFixedRight;
                    attrs[nameof(Width)] = Width;
                })
                .Apply<BSimpleCheckbox, MSimpleCheckbox>(attrs =>
                {
                    var item = (TItem)attrs.Data;
                    attrs[nameof(Class)] = "m-data-table__checkbox";
                    attrs[nameof(MSimpleCheckbox.Disabled)] = !IsSelectable(item);
                    attrs[nameof(MSimpleCheckbox.Value)] = IsSelected(item);
                    attrs[nameof(MSimpleCheckbox.Color)] = CheckboxColor ?? "";
                    attrs[nameof(MSimpleCheckbox.ValueChanged)] = EventCallback.Factory.Create<bool>(this, val =>
                   {
                       Select(item, val);
                   });
                })
                .Apply(typeof(BDataTableRow<>), typeof(MDataTableRow<TItem>), attrs =>
                {
                    attrs[nameof(MDataTableRow<TItem>.Headers)] = ComputedHeaders;
                    attrs[nameof(MDataTableRow<TItem>.IsSelected)] = (Func<TItem, bool>)IsSelected;
                    attrs[nameof(MDataTableRow<TItem>.ItemColContent)] = ItemColContent;
                    attrs[nameof(MDataTableRow<TItem>.IsExpanded)] = (Func<TItem, bool>)IsExpanded;
                    attrs[nameof(MDataTableRow<TItem>.Stripe)] = Stripe;
                })
                .Apply(typeof(BDataTableRowGroup), typeof(MDataTableRowGroup))
                .Apply<BIcon, MIcon>("expand-icon", attrs =>
                {
                    var item = (TItem)attrs.Data;
                    var expanded = IsExpanded(item);
                    var @class = IsExpanded(item) ? "m-data-table__expand-icon m-data-table__expand-icon--active" : "m-data-table__expand-icon";
                    attrs[nameof(Class)] = @class;
                    //TODO:StopPropagation
                    attrs[nameof(MIcon.OnClick)] = EventCallback.Factory.Create<MouseEventArgs>(this, () =>
                    {
                        Expand(item, !expanded);
                    });
                })
                .Apply<BButton, MButton>("group-toggle", attrs =>
                {
                    var group = (string)attrs.Data;
                    attrs[nameof(Class)] = "ma-0";
                    attrs[nameof(MButton.Icon)] = true;
                    attrs[nameof(MButton.Small)] = true;
                    attrs[nameof(MButton.OnClick)] = EventCallback.Factory.Create<MouseEventArgs>(this, args => ToggleGroup(group));
                })
                .Apply<BIcon, MIcon>("group-toggle-icon")
                .Apply<BButton, MButton>("group-remove", attrs =>
                {
                    attrs[nameof(Class)] = "ma-0";
                    attrs[nameof(MButton.Icon)] = true;
                    attrs[nameof(MButton.Small)] = true;
                    attrs[nameof(MButton.OnClick)] = EventCallback.Factory.Create<MouseEventArgs>(this, RemoveGroup);
                })
                .Apply<BIcon, MIcon>("group-remove-icon");
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
    }
}
