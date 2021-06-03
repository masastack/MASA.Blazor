using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorComponent;
using Microsoft.AspNetCore.Components;

namespace MASA.Blazor
{
    public partial class MLabel : BLabel
    {
        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public StringNumber Left { get; set; }

        [Parameter]
        public StringNumber Right { get; set; } = "auto";

        [Parameter]
        public bool Absolute { get; set; }

        [Parameter]
        public bool IsActive { get; set; }

        [Parameter]
        public bool IsFocused { get; set; }

        [Parameter]
        public string Color { get; set; } = "primary";

        protected override void SetComponentClass()
        {
            var prefix = "m-label";
            CssProvider
                .Apply<BLabel>(cssBuilder =>
                {
                    cssBuilder
                        .Add(prefix)
                        .AddIf($"{prefix}--active", () => IsActive)
                        .AddIf($"{prefix}--is-disabled", () => Disabled)
                        .AddTheme(Dark)
                        .AddTextColor(Color, () => IsFocused);
                }, styleBuilder =>
                {
                    styleBuilder
                        .Add(() => $"left:{Left.ToUnit()}")
                        .Add(() => $"right:{Right.ToUnit()}")
                        .Add(() => $"position:{(Absolute ? "absolute" : "relative")}");
                });
        }
    }
}
