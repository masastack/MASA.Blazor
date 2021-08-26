using BlazorComponent;
using Microsoft.AspNetCore.Components;

namespace MASA.Blazor
{
    public partial class MSubheader : BSubheader,IThemeable
    {
        [Parameter]
        public bool Inset { get; set; }

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
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-subheader")
                        .AddIf("m-subheader--inset", () => Inset)
                        .AddTheme(IsDark);
                });
        }
    }
}
