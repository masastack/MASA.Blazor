using BlazorComponent;
using MASA.Blazor.Helpers;
using Microsoft.AspNetCore.Components;

namespace MASA.Blazor
{
    public partial class MListItemGroup : BListItemGroup
    {
        [Parameter]
        public bool Dark { get; set; }

        protected override void SetComponentClass()
        {
            CssBuilder
                .Add("m-item-group m-list-item-group")
                .AddTheme(Dark);

            var (css, style) = ColorHelper.ToCss(Color);
            CssBuilder.Add(css);
        }
    }
}
