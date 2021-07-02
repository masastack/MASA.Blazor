using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorComponent;
using Microsoft.AspNetCore.Components;

namespace MASA.Blazor
{
    public partial class MTableCol : BTableCol
    {
        [Parameter]
        public string Align { get; set; }

        [Parameter]
        public bool Divider { get; set; }

        [CascadingParameter(Name = "TableAlign")]
        public string TableAlign { get; set; }

        public string ComputedAlign => Align ?? TableAlign;

        [Parameter]
        public StringNumber Width { get; set; }

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .AddIf(() => $"text-{ComputedAlign}", () => !string.IsNullOrEmpty(ComputedAlign))
                        .AddIf("m-data-table__divider", () => Divider);
                },styleBuilder=> {
                    styleBuilder
                        .AddWidth(Width)
                        .AddMinWidth(Width);
                });
        }
    }
}
