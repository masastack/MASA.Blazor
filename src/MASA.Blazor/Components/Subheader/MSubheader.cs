using BlazorComponent;
using Microsoft.AspNetCore.Components;

namespace MASA.Blazor
{
    public partial class MSubheader : BSubheader
    {
        [Parameter]
        public bool Inset { get; set; }

        [Parameter]
        public bool Dark { get; set; }

        protected override void SetComponentClass()
        {
            CssProvider
                .AsProvider<BSubheader>()
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-subheader")
                        .AddIf("m-subheader--inset", () => Inset)
                        .AddTheme(Dark);
                });
        }
    }
}
