using BlazorComponent;
using BlazorComponent.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Element = BlazorComponent.Web.Element;

namespace MASA.Blazor
{
    public partial class MTooltip : BTooltip, ITooltip
    {
        protected override void SetComponentClass()
        {
            var prefix = "m-tooltip";
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add(prefix)
                        .AddIf($"{prefix}--top", () => Top)
                        .AddIf($"{prefix}--right", () => Right)
                        .AddIf($"{prefix}--bottom", () => Bottom)
                        .AddIf($"{prefix}--left", () => Left)
                        .AddIf($"{prefix}--attached", () => Attach != null);
                })
                .Apply("content", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__content")
                        .AddIf("menuable__content__active", () => InternalValue)
                        .AddIf($"{prefix}__content--fixed", () => Fixed)
                        .AddBackgroundColor(Color);
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddMaxWidth(MaxWidth)
                        .AddMinWidth(MinWidth)
                        .Add($"left:{CalculatedLeft}px")
                        .Add($"top:{CalculatedTop}px")
                        .Add($"opacity:{(InternalValue ? 0.9 : 0)}")
                        .Add($"z-index:{InternalZIndex}")
                        .AddBackgroundColor(Color);
                });

            AbstractProvider
                .ApplyTooltipDefault();
        }
    }
}