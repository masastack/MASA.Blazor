using BlazorComponent;
using Microsoft.AspNetCore.Components;

namespace MASA.Blazor
{
    internal partial class MHintMessage : BHintMessage
    {
        [Parameter]
        public bool Dark { get; set; }

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            CssProvider
                .AsProvider<BHintMessage>()
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
