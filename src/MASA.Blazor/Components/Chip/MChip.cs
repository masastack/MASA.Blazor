using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MChip : BChip, IThemeable, IChip, ISizeable
    {
        private ISizeable _sizer;

        [CascadingParameter]
        public IThemeable Themeable { get; set; }

        [Parameter]
        public string Color { get; set; }

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public bool Draggable { get; set; }

        [Parameter]
        public bool Filter { get; set; }

        [Parameter]
        public string FilterIcon { get; set; } = "mdi-check";

        [Parameter]
        public string Href { get; set; }

        [Parameter]
        public bool Label { get; set; }

        [Parameter]
        public bool Large { get; set; }

        [Parameter]
        public bool Light { get; set; }

        [Parameter]
        public bool Link { get; set; }

        [Parameter]
        public bool Outlined { get; set; }

        [Parameter]
        public bool Pill { get; set; }

        [Parameter]
        public bool Ripple { get; set; } = true;

        [Parameter]
        public bool Small { get; set; }

        [Parameter]
        public string Target { get; set; }

        [Parameter]
        public string TextColor { get; set; }

        [Parameter]
        public bool XLarge { get; set; }

        [Parameter]
        public bool XSmall { get; set; }

        public bool IsClickable => (!Disabled && (IsLink || OnClick.HasDelegate || Tabindex > 0)) || ItemGroup != null;

        public bool IsDark
        {
            get
            {
                if (Dark)
                {
                    return true;
                }

                if (Light)
                {
                    return false;
                }

                return Themeable != null && Themeable.IsDark;
            }
        }

        public bool IsLink => Href != null || Link;

        public int Tabindex => Attributes.TryGetValue("tabindex", out var tabindex) ? Convert.ToInt32(tabindex) : 0;

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            _sizer = new Sizer(this);

            Attributes["ripple"] = Ripple && IsClickable;
            Attributes["draggable"] = Draggable ? "true" : null;
            Attributes["tabindex"] = ItemGroup != null && !Disabled ? 0 : Tabindex;
            Attributes["target"] = Target;

            if (Href != null)
            {
                Tag = "a";
                Attributes["href"] = Href;
            }
        }

        protected override void SetComponentClass()
        {
            var prefix = "m-chip";

            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add(prefix)
                        .Add(_sizer.SizeableClasses())
                        .AddIf($"{prefix}--clickable", () => IsClickable)
                        .AddIf($"{prefix}--disabled", () => Disabled)
                        .AddIf($"{prefix}--draggable", () => Draggable)
                        .AddIf($"{prefix}--label", () => Label)
                        .AddIf($"{prefix}--link", () => IsLink)
                        .AddIf($"{prefix}--no-color", () => string.IsNullOrEmpty(Color))
                        .AddIf($"{prefix}--outlined", () => Outlined)
                        .AddIf($"{prefix}--pill", () => Pill)
                        .AddIf($"{prefix}--removable", () => Close)
                        .AddIf($"{prefix}--active {ComputedActiveClass}", () => IsActive)
                        .AddTheme(IsDark)
                        .AddBackgroundColor(Color)
                        .AddTextColor(Color, () => Outlined)
                        .AddTextColor(TextColor);
                }, styleBuilder => { styleBuilder.AddIf("display:none", () => !Active); })
                .Apply("content", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-chip__content");
                });

            AbstractProvider
                .ApplyChipDefault()
                .Apply<BIcon, MIcon>("close", props =>
                {
                    props[nameof(Class)] = "m-chip__close";
                    props[nameof(MIcon.Right)] = true;
                    props[nameof(MIcon.Size)] = (StringNumber)18;
                    props[nameof(MIcon.OnClick)] = OnCloseClick;
                    props[nameof(MIcon.Attributes)] = new Dictionary<string, object>()
                    {
                        {"aria-label", CloseLabel}
                    };
                })
                .Apply<BIcon, MIcon>("filter", props =>
                {
                    props[nameof(Class)] = "m-chip__filter";
                    props[nameof(MIcon.Left)] = true;
                });

            CloseIcon = "mdi-close-circle";
        }
    }
}