using BlazorComponent;
using MASA.Blazor.Helpers;
using Microsoft.AspNetCore.Components;

namespace MASA.Blazor
{
    public partial class MListItemGroup : BListItemGroup,IThemeable
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
            CssProvider
                .Apply<BListItemGroup>(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-item-group m-list-item-group")
                        .AddTheme(IsDark)
                        .AddTextColor(Color);
                });
        }
    }
}
