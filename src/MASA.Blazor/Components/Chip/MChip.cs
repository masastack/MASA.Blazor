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

        [Parameter]
        public bool Draggable { get;set;  }

        [Parameter]
        public EventCallback<bool> ActiveChanged { get; set; }

        [Parameter]
        public bool Filter { get; set; }

        [Parameter]
        public string FilterIcon { get; set; } = "mdi-check";

        [Parameter]
        public bool InputValue { get; set; }

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
                        .AddIf($"{prefix}--active", () => InputValue)
                        .AddIf($"{prefix}--clickable", () => ItemGroup != null)
                        .AddIf($"{SelectColor}--text", () => InputValue)
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
                .Apply<BIcon, MIcon>(props =>
                {
                    props[nameof(Class)] = InputValue ? "m-chip__filter" : "m-chip__close";
                    props[nameof(MIcon.Right)] = !InputValue;
                    props[nameof(MIcon.Left)] = InputValue;
                    props[nameof(MIcon.Size)] = InputValue ? "" : (StringNumber)18;
                    props[nameof(MIcon.Color)] = InputValue ? "" : CloseIconColor;
                    props[nameof(MIcon.OnClick)] = EventCallback.Factory.Create<MouseEventArgs>(this, HandleOnIconClickAsync);
                });

            CloseIcon = "mdi-close-circle";
        }

        protected virtual async Task HandleOnIconClickAsync(MouseEventArgs args)
        {
            if (InputValue)
            {
                InputValue = !InputValue;
                await ActiveChanged.InvokeAsync(InputValue);
            }

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
