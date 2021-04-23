using System.Threading.Tasks;
using BlazorComponent;
using MASA.Blazor.Helpers;
using Microsoft.AspNetCore.Components;

namespace MASA.Blazor
{
    public partial class MCheckbox : BCheckbox
    {
        [Parameter]
        public bool Dark { get; set; }

        protected override Task OnInitializedAsync()
        {
            UncheckIconContent = RenderIcon("mdi-checkbox-blank-outline", Color, Disabled, Dark);
            CheckedIconContent = RenderIcon("mdi-checkbox-marked", Color ?? "primary", Disabled, Dark);
            IndeterminateIconContent = RenderIcon("mdi-minus-box", Color, Disabled, Dark);

            AnimationContent = RenderRipple(Color);

            return base.OnInitializedAsync();
        }

        private RenderFragment RenderIcon(string icon, string color, bool disabled, bool dark) => builder =>
        {
            int sequence = 0;
            builder.OpenComponent(sequence++, typeof(MIcon));

            if (!disabled)
            {
                builder.AddAttribute(sequence++, nameof(MIcon.Color), color);
                builder.AddAttribute(sequence++, nameof(MIcon.Dark), dark);
            }

            builder.AddAttribute(sequence++, nameof(MIcon.ChildContent), (RenderFragment)((builder) => builder.AddContent(sequence++, icon)));
            builder.CloseComponent();
        };

        private RenderFragment RenderRipple(string color) => builder =>
        {
            int sequence = 0;
            builder.OpenElement(sequence++, "div");

            var (color_css, color_style) = ColorHelper.ToCss(color);
            builder.AddAttribute(sequence++, "class", $"m-input--selection-controls__ripple {color_css}");

            builder.AddAttribute(sequence++, "style", color_style);

            builder.CloseElement();
        };

        public override void SetComponentClass()
        {
            CssBuilder
                .Add("m-input m-input--selection-controls m-input--checkbox")
                .AddIf("m-input--is-disabled", () => Disabled)
                .AddTheme(Dark);

            ControlCssBuilder
                .Add("m-input__control");

            SlotCssBuilder
                .Add("m-input__slot");

            InputWrapperCssBuilder
                .Add("m-input--selection-controls__input");

            LabelCssBuilder
                .Add("m-label");
        }
    }
}
