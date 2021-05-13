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
            CssProvider
                .Apply<BListItemGroup>(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-item-group m-list-item-group")
                        .AddTheme(Dark)
                        .AddTextColor(Color);
                });
        }
    }
}
