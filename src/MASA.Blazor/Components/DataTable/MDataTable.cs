using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
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
                .Apply(typeof(BDataTableHeader), typeof(MDataTableHeader), props =>
                {
                    foreach (var prop in HeaderProps)
                    {
                        props[prop.Key] = prop.Value;
                    }

                    props[nameof(MDataTableHeader.Headers)] = ComputedHeaders.ToList<DataTableHeader>();
                    props[nameof(MDataTableHeader.Options)] = InternalOptions;
                    props[nameof(MDataTableHeader.ShowGroupBy)] = ShowGroupBy;
                    props[nameof(MDataTableHeader.CheckboxColor)] = CheckboxColor;
                    props[nameof(MDataTableHeader.SomeItems)] = SomeItems;
                    props[nameof(MDataTableHeader.EveryItem)] = EveryItem;
                    props[nameof(MDataTableHeader.SingleSelect)] = SingleSelect;
                    props[nameof(MDataTableHeader.DisableSort)] = DisableSort;
                    props[nameof(MDataTableHeader.HeaderColContent)] = HeaderColContent;
                    props[nameof(MDataTableHeader.OnToggleSelectAll)] = EventCallback.Factory.Create<bool>(this, ToggleSelectAll);
                    props[nameof(MDataTableHeader.OnSort)] = EventCallback.Factory.Create<string>(this, Sort);
                    props[nameof(MDataTableHeader.OnGroup)] = EventCallback.Factory.Create<string>(this, Group);
                })
                .Apply(typeof(BProgressLinear), typeof(MProgressLinear), props =>
                {
                    props[nameof(MProgressLinear.Absolute)] = true;
                    props[nameof(MProgressLinear.Color)] = (Loading == true || Loading == "") ? "primary" : Loading.AsT0;
                    props[nameof(MProgressLinear.Height)] = LoaderHeight;
                    props[nameof(MProgressLinear.Indeterminate)] = true;
                })
                .Apply<BSimpleTable, MSimpleTable>(props =>
                {
                    props[nameof(Height)] = Height;
                    props[nameof(FixedHeader)] = FixedHeader;
                    props[nameof(Dense)] = Dense;
                    props[nameof(Class)] = Class;
                    props[nameof(Style)] = Style;
                    props[nameof(FixedRight)] = FixedRight;
                    props[nameof(Width)] = Width;
                })
                .Apply<BSimpleCheckbox, MSimpleCheckbox>(props =>
                {
                    var item = (TItem)props.Data;
                    props[nameof(Class)] = "m-data-table__checkbox";
                    props[nameof(MSimpleCheckbox.Disabled)] = !IsSelectable(item);
                    props[nameof(MSimpleCheckbox.Value)] = IsSelected(item);
                    props[nameof(MSimpleCheckbox.Color)] = CheckboxColor ?? "";
                    props[nameof(MSimpleCheckbox.OnInput)] = EventCallback.Factory.Create<bool>(this, val =>
                   {
                       Select(item, val);
                   });
                })
                .Apply(typeof(BDataTableRow<>), typeof(MDataTableRow<TItem>), props =>
                {
                    props[nameof(MDataTableRow<TItem>.Headers)] = ComputedHeaders;
                    props[nameof(MDataTableRow<TItem>.IsSelected)] = (Func<TItem, bool>)IsSelected;
                    props[nameof(MDataTableRow<TItem>.ItemColContent)] = ItemColContent;
                    props[nameof(MDataTableRow<TItem>.IsExpanded)] = (Func<TItem, bool>)IsExpanded;
                    props[nameof(MDataTableRow<TItem>.Stripe)] = Stripe;
                })
                .Apply(typeof(BDataTableRowGroup), typeof(MDataTableRowGroup))
                .Apply<BIcon, MIcon>("expand-icon", props =>
                {
                    var item = (TItem)props.Data;
                    var expanded = IsExpanded(item);
                    var @class = IsExpanded(item) ? "m-data-table__expand-icon m-data-table__expand-icon--active" : "m-data-table__expand-icon";
                    props[nameof(Class)] = @class;
                    //TODO:StopPropagation
                    props[nameof(MIcon.OnClick)] = EventCallback.Factory.Create<MouseEventArgs>(this, () =>
                    {
                        Expand(item, !expanded);
                    });
                })
                .Apply<BButton, MButton>("group-toggle", props =>
                {
                    var group = (string)props.Data;
                    props[nameof(Class)] = "ma-0";
                    props[nameof(MButton.Icon)] = true;
                    props[nameof(MButton.Small)] = true;
                    props[nameof(MButton.OnClick)] = EventCallback.Factory.Create<MouseEventArgs>(this, args => ToggleGroup(group));
                })
                .Apply<BIcon, MIcon>("group-toggle-icon")
                .Apply<BButton, MButton>("group-remove", props =>
                {
                    props[nameof(Class)] = "ma-0";
                    props[nameof(MButton.Icon)] = true;
                    props[nameof(MButton.Small)] = true;
                    props[nameof(MButton.OnClick)] = EventCallback.Factory.Create<MouseEventArgs>(this, RemoveGroup);
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
