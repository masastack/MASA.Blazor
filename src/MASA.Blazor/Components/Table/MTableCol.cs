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
        public string Align { get; set; } = "start";

        [Parameter]
        public bool Divider { get; set; }

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(typeof(BTableCol), cssBuilder =>
                {
                    cssBuilder
                        .AddIf(() => $"text-{Align}", () => !string.IsNullOrEmpty(Align))
                        .AddIf("m-data-table__divider", () => Divider);
                });
        }
    }
}
