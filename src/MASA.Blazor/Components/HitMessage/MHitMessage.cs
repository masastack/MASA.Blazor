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

            CssProvider
                .AsProvider<BHitMessage>()
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-messages")
                        .AddTheme(Dark);
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
