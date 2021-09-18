using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MTabsBar : MSlideGroup, ITabsBar, IThemeable
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

        [Parameter]
        public string Color { get; set; } = "primary";

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            const string prefix = "m-tabs-bar";
            CssProvider
                .Merge(css => { css.Add(prefix).AddTheme(IsDark).AddTextColor(Color); })
                .Merge("content", css => { css.Add($"{prefix}__content"); });
        }
    }
}