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
    public class MDataTableHeader : BDataTableHeader, IDataTableHeader
    {
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
        public bool SingleSelect { get; set; }

        [Parameter]
        public RenderFragment DataTableSelectContent { get; set; }

        [Parameter]
        public bool DisableSort { get; set; }

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
                        .Add("m-data-table-header");
                })
                .Apply("header", cssBuilder =>
                {
                    var header = (DataTableHeader)cssBuilder.Data;

                    if (!DisableSort && header.Sortable)
                    {
                        var sortIndex = Options.SortBy.IndexOf(header.Value);
                        var beingSorted = sortIndex >= 0;
                        var isDesc = beingSorted ? Options.SortDesc[sortIndex] : false;

                        cssBuilder
                            .Add("sortable")
                            .AddIf("active", () => beingSorted)
                            .AddIf(() => isDesc ? "desc" : "asc", () => beingSorted);
                    }

                    cssBuilder
                        .Add($"text-{header.Align ?? "start"}");
                }, styleBuilder =>
                {
                    var header = (DataTableHeader)styleBuilder.Data;

                    styleBuilder
                        .AddWidth(header.Width)
                        .AddMinWidth(header.Width);
                })
                .Apply("sort-badge", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-data-table-header__sort-badge");
                });

            AbstractProvider
                .ApplyDataTableHeaderDefault()
                .Apply<BSimpleCheckbox, MSimpleCheckbox>(props =>
                {
                    props[nameof(Class)] = "m-data-table__checkbox";
                    props[nameof(MSimpleCheckbox.Value)] = EveryItem;
                    props[nameof(MSimpleCheckbox.Indeterminate)] = !EveryItem && SomeItems;
                    props[nameof(MSimpleCheckbox.Color)] = CheckboxColor;
                    props[nameof(MSimpleCheckbox.OnInput)] = OnToggleSelectAll;
                })
                .Apply<BIcon, MIcon>(props =>
                {
                    props[nameof(Class)] = "m-data-table-header__icon";
                    props[nameof(MIcon.Size)] = (StringNumber)18;
                });
        }
    }
}
