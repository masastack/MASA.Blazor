namespace Masa.Blazor
{
    public class MDataTableHeader : BDataTableHeader, IDataTableHeader
    {
        [Inject]
        protected I18n I18n { get; set; } = null!;

        [Parameter]
        public DataOptions Options { get; set; } = null!;

        [Parameter]
        public string? CheckboxColor { get; set; }

        [Parameter]
        public bool EveryItem { get; set; }

        [Parameter]
        public bool SomeItems { get; set; }

        [Parameter]
        public bool ShowGroupBy { get; set; }

        [Parameter]
        public bool SingleSelect { get; set; }

        [Parameter]
        public RenderFragment? DataTableSelectContent { get; set; }

        [Parameter]
        public bool DisableSort { get; set; }

        [Parameter]
        public bool MultiSort { get; set; }

        [Parameter]
        public string SortIcon { get; set; } = "$sort";

        [Parameter]
        public RenderFragment<DataTableHeader>? HeaderColContent { get; set; }

        [Parameter]
        public EventCallback<bool> OnToggleSelectAll { get; set; }

        [Parameter]
        public EventCallback<OneOf<string?, List<string>>> OnSort { get; set; }

        [Parameter]
        public EventCallback<string> OnGroup { get; set; }

        public async Task HandleOnHeaderColClick(string? value)
        {
            if (OnSort.HasDelegate)
            {
                await OnSort.InvokeAsync(value);
            }
        }

        public async Task HandleOnGroup(string group)
        {
            if (OnGroup.HasDelegate)
            {
                await OnGroup.InvokeAsync(group);
            }
        }

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-data-table-header")
                        .AddIf("m-data-table-header-mobile", () => IsMobile);
                })
                .Apply("header", cssBuilder =>
                {
                    var header = (DataTableHeader?)cssBuilder.Data;
                    if (header is null) return;

                    if (!DisableSort && header.Sortable)
                    {
                        var sortIndex = Options.SortBy.IndexOf(header.Value);
                        var beingSorted = sortIndex >= 0;
                        var isDesc = beingSorted && Options.SortDesc.ElementAtOrDefault(sortIndex);

                        cssBuilder
                            .Add("sortable")
                            .AddIf("active", () => beingSorted)
                            .AddIf(() => isDesc ? "desc" : "asc", () => beingSorted);
                    }

                    cssBuilder
                        .Add($"text-{header.Align.ToString().ToLower()}");
                }, styleBuilder =>
                {
                    var header = (DataTableHeader?)styleBuilder.Data;
                    if (header is null) return;

                    styleBuilder
                        .AddWidth(header.Width)
                        .AddMinWidth(header.Width);
                })
                .Apply("sort-badge", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-data-table-header__sort-badge");
                })
                .Apply("header-mobile__wrapper", cssBuilder => { cssBuilder.Add("m-data-table-header-mobile__wrapper"); })
                .Apply("header-mobile__select", cssBuilder => { cssBuilder.Add("m-data-table-header-mobile__select"); })
                .Apply("header-mobile__select-chips", cssBuilder =>
                {
                    var data = (DataTableHeader?)cssBuilder.Data;
                    if (data is null || (data.Text is null && data.Value is null)) return;

                    var sortIndex = Options.SortBy.IndexOf(data.Value);
                    var beingSorted = sortIndex >= 0;
                    var isDesc = Options.SortDesc.ElementAtOrDefault(sortIndex);

                    cssBuilder.Add("m-chip__close sortable")
                              .AddIf("active", () => beingSorted)
                              .AddIf("asc", () => beingSorted && !isDesc)
                              .AddIf("desc", () => beingSorted && isDesc);
                });

            AbstractProvider
                .ApplyDataTableHeaderDefault()
                .Apply<BSimpleCheckbox, MSimpleCheckbox>(attrs =>
                {
                    attrs[nameof(Class)] = "m-data-table__checkbox";
                    attrs[nameof(MSimpleCheckbox.Value)] = EveryItem;
                    attrs[nameof(MSimpleCheckbox.Indeterminate)] = !EveryItem && SomeItems;
                    attrs[nameof(MSimpleCheckbox.Color)] = CheckboxColor;
                    attrs[nameof(MSimpleCheckbox.ValueChanged)] = OnToggleSelectAll;
                })
                .Apply<BIcon, MIcon>(attrs =>
                {
                    attrs[nameof(Class)] = "m-data-table-header__icon";
                    attrs[nameof(MIcon.Size)] = (StringNumber)18;
                })
                .Apply(typeof(ISelect<,,>), typeof(MSelect<DataTableHeader, string, string>), "sort-select", attrs =>
                {
                    attrs[nameof(MSelect<DataTableHeader, string, string>.ItemText)] =
                        (Func<DataTableHeader, string>)(item => item.Text);
                    attrs[nameof(MSelect<DataTableHeader, string, string>.ItemValue)] =
                        (Func<DataTableHeader, string>)(item => item.Value);
                    attrs[nameof(MSelect<DataTableHeader, string, string>.Label)] = I18n.T("$masaBlazor.dataTable.sortBy");
                    attrs[nameof(MSelect<DataTableHeader, string, string>.HideDetails)] = (StringBoolean)true;
                    attrs[nameof(MSelect<DataTableHeader, string, string>.Multiple)] = Options.MultiSort;
                    attrs[nameof(MSelect<DataTableHeader, string, string>.Value)] = Options.SortBy.Count > 0 ? Options.SortBy[0] : null;
                    attrs[nameof(MSelect<DataTableHeader, string, string>.ValueChanged)] =
                        EventCallback.Factory.Create<string>(this, (s) => OnSort.InvokeAsync(s));

                    attrs[nameof(MSelect<DataTableHeader, string, string>.MenuProps)] =
                        (Action<BMenuProps>)(props => props.CloseOnContentClick = true);
                })
                .Apply(typeof(ISelect<,,>), typeof(MSelect<DataTableHeader, string, List<string>>), "sort-select-multiple", attrs =>
                {
                    attrs[nameof(MSelect<DataTableHeader, string, List<string>>.ItemText)] =
                        (Func<DataTableHeader, string>)(item => item.Text);
                    attrs[nameof(MSelect<DataTableHeader, string, List<string>>.ItemValue)] =
                        (Func<DataTableHeader, string>)(item => item.Value);
                    attrs[nameof(MSelect<DataTableHeader, string, List<string>>.Label)] = I18n.T("$masaBlazor.dataTable.sortBy");
                    attrs[nameof(MSelect<DataTableHeader, string, List<string>>.HideDetails)] = (StringBoolean)true;
                    attrs[nameof(MSelect<DataTableHeader, string, List<string>>.Multiple)] = Options.MultiSort;

                    attrs[nameof(MSelect<List<DataTableHeader>, string, List<string>>.Value)] = Options.SortBy;
                    attrs[nameof(MSelect<List<DataTableHeader>, string, List<string>>.ValueChanged)] =
                        EventCallback.Factory.Create<List<string>>(this, s => OnSort.InvokeAsync(s));

                    attrs[nameof(MSelect<DataTableHeader, string, List<string>>.MenuProps)] =
                        (Action<BMenuProps>)(props => props.CloseOnContentClick = true);
                })
                .Apply<BChip, MChip>(attrs =>
                {
                    attrs[nameof(MChip.Class)] = "sortable";
                    attrs[nameof(MChip.Attributes)] = new Dictionary<string, object>()
                    {
                        { "__internal_stopPropagation_onexclick", true },
                    };
                });
        }
    }
}
