using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    internal partial class MListGroupItemIcon : MListItemIcon
    {
        [Parameter]
        public string Type { get; set; }

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            CssProvider
                .Merge<BListItemIcon>(cssBuilder =>
                {
                    cssBuilder
                        .Add($"m-list-group__header__{Type}-icon");
                });
        }
    }
}
