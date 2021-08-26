using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using OneOf;
using System;

namespace MASA.Blazor
{
    public partial class MChip : BChip, IThemeable
    {
        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public bool Label { get; set; }

        [Parameter]
        public string Color { get; set; }

        [Parameter]
        public bool Large { get; set; }

        [Parameter]
        public bool Small { get; set; }

        [Parameter]
        public bool XLarge { get; set; }

        [Parameter]
        public bool XSmall { get; set; }

        [Parameter]
        public string CloseIconColor { get; set; }

        [Parameter]
        public string SelectColor { get; set; } = "primary";

        public bool Medium => !XSmall && !Small && !Large && !XLarge;

        [Obsolete("Use OnCloseClick instead.")]
        [Parameter]
        public EventCallback<MouseEventArgs> CloseClick { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnCloseClick { get; set; }

        [Parameter]
        public bool Outlined { get; set; }

        [Parameter]
        public string TextColor { get; set; }

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public bool Light { get; set; }

        [CascadingParameter]
        public IThemeable Themeable { get; set; }

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

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (CloseClick.HasDelegate)
            {
                OnCloseClick = CloseClick;
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
                        .AddIf($"{prefix}--disabled", () => Disabled)
                        .AddIf($"{prefix}--label", () => Label)
                        .AddIf($"{prefix}--no-color", () => string.IsNullOrEmpty(Color))
                        .AddIf($"{prefix}--removable", () => Close)
                        .AddTheme(IsDark)
                        .AddBackgroundColor(Color)
                        .AddTextColor(Color, () => Outlined)
                        .AddTextColor(TextColor)
                        .AddIf("m-size--default", () => Medium)
                        .AddIf("m-size--x-small", () => XSmall)
                        .AddIf("m-size--small", () => Small)
                        .AddIf("m-size--large", () => Large)
                        .AddIf("m-size--x-large", () => XLarge)
                        .AddIf($"{prefix}--active", () => IsActive)
                        .AddIf($"{prefix}--clickable", () => ItemGroup != null)
                        .AddIf($"{SelectColor}--text", () => IsActive)
                        .AddIf("m-chip--outlined", () => Outlined);
                })
                .Apply("content", cssBuilder =>
                 {
                     cssBuilder
                         .Add("m-chip__content");
                 });

            AbstractProvider
                .Apply<BIcon, MChipCloseIcon>(props =>
                {
                    props[nameof(MChipCloseIcon.Right)] = true;
                    props[nameof(MChipCloseIcon.Size)] = (StringNumber)18;
                    props[nameof(MChipCloseIcon.Color)] = CloseIconColor;
                    props[nameof(MChipCloseIcon.OnClick)] = EventCallback.Factory.Create<MouseEventArgs>(this, async args =>
                    {
                        if (OnCloseClick.HasDelegate)
                        {
                            await OnCloseClick.InvokeAsync(args);
                        }
                        else
                        {
                            Show = false;
                        }
                    });
                });

            CloseIcon = "mdi-close-circle";
        }
    }
}
