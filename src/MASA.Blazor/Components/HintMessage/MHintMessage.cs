using BlazorComponent;
using Microsoft.AspNetCore.Components;

namespace MASA.Blazor
{
    internal partial class MHintMessage : BHintMessage, IThemeable
    {

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

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            CssProvider
                .AsProvider<BHintMessage>()
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-messages")
                        .AddTheme(IsDark);
                })
                .Apply("wrap", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-messages__wrapper");
                })
                .Apply("message", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-messages__message");
                });
        }
    }
}
