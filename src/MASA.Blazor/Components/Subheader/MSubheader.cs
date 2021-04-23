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

        public override void SetComponentClass()
        {
            CssBuilder
                .Add("m-subheader")
                .AddIf("m-subheader--inset", () => Inset)
                .AddTheme(Dark);
        }
    }
}
