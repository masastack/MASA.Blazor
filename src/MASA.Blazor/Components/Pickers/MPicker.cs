using BlazorComponent;
using Microsoft.AspNetCore.Components;

namespace MASA.Blazor
{
    public partial class MPicker : BPicker, IThemeable, IElevatable, IColorable
    {
        private string _headerColor;

        [Parameter]
        public string HeaderColor
        {
            get
            {
                return _headerColor ?? Color ?? "primary";
            }
            set
            {
                _headerColor = value;
            }
        }

        [Parameter]
        public bool Flat { get; set; }

        [Parameter]
        public bool Landscape { get; set; }

        [Parameter]
        public bool FullWidth { get; set; }

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public StringNumber Elevation { get; set; }

        [Parameter]
        public string Color { get; set; }

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
                        .AddBackgroundColor(HeaderColor);
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddBackgroundColor(HeaderColor);
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
                }, styleBuilder =>
                {
                    styleBuilder.Add(ActionsStyle);
                });
        }
    }
}
