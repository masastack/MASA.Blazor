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

        [Parameter]
        public EventCallback<ColorPickerColor> OnColorUpdate { get; set; }

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
                })
                .Apply("sliders", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__sliders");
                });

            AbstractProvider
                .ApplyColorPickerPreviewDefault()
                .Apply<ISlider<double>, MSlider<double>>(props =>
                {
                    if (props.Index == 0)
                    {
                        props[nameof(Class)] = "m-color-picker__hue";
                        props[nameof(MSlider<double>.ThumbColor)] = "grey lighten-2";
                        props[nameof(MSlider<double>.HideDetails)] = (StringBoolean)true;
                        props[nameof(MSlider<double>.Value)] = Color.Hue;
                        props[nameof(MSlider<double>.Step)] = 0D;
                        props[nameof(MSlider<double>.Min)] = 0D;
                        props[nameof(MSlider<double>.Max)] = 360D;
                        props[nameof(MSlider<double>.ValueChanged)] = CreateEventCallback<double>(val =>
                        {
                            if (Color.Hue != val)
                            {
                                if (OnColorUpdate.HasDelegate)
                                {
                                    var hsva = Color.Hsva;
                                    hsva.H = val;
                                    OnColorUpdate.InvokeAsync(ColorUtils.FromHSVA(hsva));
                                }
                            }
                        });
                    }
                });
        }
    }
}
