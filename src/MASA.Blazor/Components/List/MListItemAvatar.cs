using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MListItemAvatar : MAvatar
    {
        [Parameter]
        public bool Horizontal { get; set; }

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            var prefix = "m-list-item__avatar";
            CssProvider
                .Merge<BAvatar>(cssBuilder =>
                {
                    cssBuilder
                        .Add(prefix)
                        .AddIf($"{prefix}--horizontal", () => Horizontal)
                        .AddIf("m-avatar-tile", () => Tile || Horizontal);
                });
        }
    }
}
