using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MColorPickerPreview : BColorPickerPreview, IColorPickerPreview
    {
        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public bool HideAlpha { get; set; }

        [Parameter]
        public ColorPickerColor Color { get; set; }

        protected override void SetComponentClass()
        {
            var prefix = "m-color-picker";

            CssProvider
                .Apply("preview", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__preview")
                        .AddIf($"{prefix}__preview--hide-alpha", () => HideAlpha);
                })
                .Apply("dot", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__dot");
                }, styleBuilder =>
                {
                    styleBuilder
                        .Add(() => $"background:{ColorUtils.RGBAtoCSS(Color.Rgba)}");
                });

            AbstractProvider
                .ApplyColorPickerPreviewDefault();
        }
    }
}
