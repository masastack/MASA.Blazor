using BlazorComponent;
using Microsoft.AspNetCore.Components;

namespace MASA.Blazor
{
    public partial class MHitMessage : BHitMessage
    {
        [Parameter]
        public bool Dark { get; set; }

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            CssBuilder
                .Add("m-messages")
                .AddTheme(Dark);

            WrapperCssBuilder
                .Add("m-messages__wrapper");

            MessageCssBuilder
                .Add("m-messages__message");
        }
    }
}
