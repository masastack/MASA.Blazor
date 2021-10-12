using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MChip : BChip, IThemeable, IChip
    {
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

        [Parameter]
        public bool Draggable { get; set; }

        [Parameter]
        public bool Filter { get; set; }

        [Parameter]
        public string FilterIcon { get; set; } = "mdi-check";

        [Parameter]
        public bool Close { get; set; }

        [Parameter]
        public string CloseIcon { get; set; }

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
                        .AddIf($"{prefix}--active {ComputedActiveClass}", () => IsActive)
                        .AddIf($"{prefix}--clickable", () => ItemGroup != null)
                        .AddIf("m-chip--outlined", () => Outlined)
                        .AddIf($"{prefix}--draggable", () => Draggable);
                })
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
                    props[nameof(MIcon.Color)] = CloseIconColor;
                    props[nameof(MIcon.OnClick)] = EventCallback.Factory.Create<MouseEventArgs>(this, HandleOnIconClickAsync);
                })
                .Apply<BIcon, MIcon>("filter", props =>
                {
                    props[nameof(Class)] = "m-chip__filter";
                    props[nameof(MIcon.Left)] = true;
                });

            CloseIcon = "mdi-close-circle";
        }

        protected virtual async Task HandleOnIconClickAsync(MouseEventArgs args)
        {
            if (OnCloseClick.HasDelegate)
            {
                await OnCloseClick.InvokeAsync(args);
            }
            else
            {
                Show = false;
            }
        }
    }
}