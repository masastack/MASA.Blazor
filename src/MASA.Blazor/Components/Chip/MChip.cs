using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MChip : BChip
    {
        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public bool Lable { get; set; }

        [Parameter]
        public string Color { get; set; }

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public bool Large { get; set; }

        [Parameter]
        public bool Small { get; set; }

        [Parameter]
        public bool XLarge { get; set; }

        [Parameter]
        public bool XSmall { get; set; }

        public bool Medium => !XSmall && !Small && !Large && !XLarge;

        [Parameter]
        public EventCallback<MouseEventArgs> Click { get; set; }

        protected override void SetComponentClass()
        {
            var prefix = "m-chip";
            CssProvider
                .AsProvider<BChip>()
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add(prefix)
                        .AddIf($"{prefix}--disabled", () => Disabled)
                        .AddIf($"{prefix}--label", () => Lable)
                        .AddIf($"{prefix}--no-color", () => string.IsNullOrEmpty(Color))
                        .AddIf($"{prefix}--removable", () => Close)
                        .AddTheme(Dark)
                        .AddIf(Color, () => !string.IsNullOrEmpty(Color))
                        .AddIf("m-size--default", () => Medium)
                        .AddIf("m-size--x-small", () => XSmall)
                        .AddIf("m-size--small", () => Small)
                        .AddIf("m-size--large", () => Large)
                        .AddIf("m-size--x-large", () => XLarge);
                })
                .Apply("content", cssBuilder =>
                 {
                     cssBuilder
                         .Add("m-chip__content");
                 });

            SlotProvider
                .Apply<BIcon, MChipCloseIcon>(props =>
                {
                    props[nameof(MChipCloseIcon.Right)] = true;
                    props[nameof(MChipCloseIcon.Size)] = (OneOf<string, int>)18;
                    props[nameof(MChipCloseIcon.Click)] = EventCallback.Factory.Create<MouseEventArgs>(this, async args =>
                    {
                        Show = false;

                        if (Click.HasDelegate)
                        {
                            await Click.InvokeAsync(args);
                        }
                    });
                });

            CloseIcon = "mdi-close-circle";
        }
    }
}
