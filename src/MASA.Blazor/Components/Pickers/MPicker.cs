using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MPicker : BPicker, IThemeable, IElevatable, IColorable
    {
        protected string TitleColor => Color ?? (Dark ? null : "primary");

        [Parameter]
        public bool Flat { get; set; }

        [Parameter]
        public bool Landscape { get; set; }

        [Parameter]
        public bool FullWidth { get; set; }

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public int? Elevation { get; set; }

        [Parameter]
        public virtual string Color { get; set; }

        [Parameter]
        public StringNumber Width { get; set; } = 290;

        protected override void SetComponentClass()
        {
            var prefix = "m-picker";
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-card")
                        .Add(prefix)
                        .AddIf($"{prefix}--flat", () => Flat)
                        .AddIf($"{prefix}--landscape", () => Landscape)
                        .AddIf($"{prefix}--full-width", () => FullWidth)
                        .AddTheme(Dark)
                        .AddElevation(Elevation);
                })
                .Apply("title", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__title")
                        .AddIf($"{prefix}__title--landscape", () => Landscape)
                        .AddBackgroundColor(TitleColor);
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddBackgroundColor(TitleColor);
                })
                .Apply("body", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__body")
                        .AddIf($"{prefix}__body--no-title", () => NoTitle)
                        .AddTheme(Dark);
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddIf($"width:{Width.ToUnit()}", () => !FullWidth);
                })
                .Apply("actions", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__actions")
                        .Add("m-card__actions")
                        .AddIf($"{prefix}__actions--no-title", () => NoTitle);
                });
        }
    }
}
