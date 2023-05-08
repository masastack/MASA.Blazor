﻿namespace Masa.Blazor
{
    public partial class MPicker : BPicker, IThemeable, IPicker
    {
        [Parameter]
        public bool Flat { get; set; }

        [Parameter]
        public bool FullWidth { get; set; }

        [Parameter]
        public bool Landscape { get; set; }

        [Parameter]
        public bool NoTitle { get; set; }

        [Parameter]
        public StringNumber? Elevation { get; set; }

        [Parameter]
        public string? Color { get; set; }

        [Parameter]
        [ApiDefaultValue(290)]
        public StringNumber Width { get; set; } = 290;

        [Parameter]
        [ApiDefaultValue("fade-transition")]
        public string Transition { get; set; } = "fade-transition";

        [Parameter]
        public string? HeaderColor { get; set; }

        public string ComputedTitleColor
        {
            get
            {
                var defaultTitleColor = IsDark ? "" : (HeaderColor ?? Color ?? "primary");
                return HeaderColor ?? Color ?? defaultTitleColor;
            }
        }

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
                        .AddTheme(IsDark)
                        .AddElevation(Elevation);
                })
                .Apply("title", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__title")
                        .AddIf($"{prefix}__title--landscape", () => Landscape)
                        .AddBackgroundColor(ComputedTitleColor);
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddBackgroundColor(ComputedTitleColor);
                })
                .Apply("body", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__body")
                        .AddIf($"{prefix}__body--no-title", () => NoTitle)
                        .AddTheme(IsDark);
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

            AbstractProvider
                .ApplyPickerDefault();

        }
    }
}
