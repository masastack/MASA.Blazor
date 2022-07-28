using Microsoft.AspNetCore.Components.Web;

namespace Masa.Blazor;

public class MDataTableHeaderMobile : BDataTableHeaderMobile, IDataTableHeader
{
    [Inject]
    protected I18n I18n { get; set; }

    [Parameter]
    public DataOptions Options { get; set; }

    [Parameter]
    public string CheckboxColor { get; set; }

    [Parameter]
    public bool EveryItem { get; set; }

    [Parameter]
    public bool SomeItems { get; set; }

    [Parameter]
    public bool ShowGroupBy { get; set; }

    [Parameter]
    public RenderFragment DataTableSelectContent { get; set; }

    [Parameter]
    public string SortIcon { get; set; } = "mdi-arrow-up";

    [Parameter]
    public RenderFragment<DataTableHeader> HeaderColContent { get; set; }

    [Parameter]
    public EventCallback<bool> OnToggleSelectAll { get; set; }

    [Parameter]
    public EventCallback<string> OnSort { get; set; }

    [Parameter]
    public EventCallback<string> OnGroup { get; set; }

    public Dictionary<string, object> GetHeaderAttrs(DataTableHeader header)
    {
        var attrs = new Dictionary<string, object>();
        if (!DisableSort && header.Sortable)
        {
            attrs["onclick"] = EventCallback.Factory.Create<MouseEventArgs>(this, async () =>
            {
                if (OnSort.HasDelegate)
                {
                    await OnSort.InvokeAsync(header.Value);
                }
            });
        }

        return attrs;
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
                    .Add("m-data-table-header m-data-table-header-mobile");
            })
            .Apply("header-mobile__wrapper", cssBuilder => { cssBuilder.Add("m-data-table-header-mobile__wrapper"); })
            .Apply("header-mobile__select", cssBuilder => { cssBuilder.Add("m-data-table-header-mobile__select"); });

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
            .Apply(typeof(ISelect<,,>), typeof(MSelect<(string Text, string Value), string, string>), attrs =>
            {
                var sortHeaders = Headers.Where(h => h.Sortable && h.Value == "data-table-select")
                                         .Select(h => (h.Text, h.Value));

                // attrs[nameof(MSelect<(string, string), string, string>.Items)] = sortHeaders;
                attrs[nameof(MSelect<(string, string), string, string>.ItemText)] = (Func<(string Text, string Value), string>)(item => item.Text);
                attrs[nameof(MSelect<(string, string), string, string>.ItemValue)] = (Func<(string Text, string Value), string>)(item => item.Value);
                attrs[nameof(MSelect<(string, string), string, string>.Label)] = I18n.T("$masaBlazor.dataTable.sortBy");
                attrs[nameof(MSelect<(string, string), string, string>.HideDetails)] = true;
                attrs[nameof(MSelect<(string, string), string, string>.Multiple)] = Options.MultiSort;
                attrs[nameof(MSelect<(string, string), string, string>.Value)] = Options.MultiSort ? Options.SortBy : Options.SortBy[0];
                attrs[nameof(MSelect<(string, string), string, string>.ValueChanged)] = EventCallback.Factory.Create<string>(this, OnSort);
                attrs[nameof(MSelect<(string, string), string, string>.MenuProps)] = (Action<BMenuProps>)(props => props.CloseOnContentClick = true);
                // TODO: sort chip slot
            });
    }
}
