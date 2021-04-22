using System.Collections.Generic;
using BlazorComponent;
using Microsoft.AspNetCore.Components;

namespace MASA.Blazor
{
    /// <summary>
    /// Root for application
    /// </summary>
    public partial class MApp : BApp
    {
        /// <summary>
        /// Whether to display from left to right
        /// </summary>
        [Parameter]
        public bool LeftToRight { get; set; } = true;

        /// <summary>
        /// Whether dark theme
        /// </summary>
        [Parameter]
        public bool Dark { get; set; }

        public override void SetComponentClass()
        {
            var prefix = "m-application";

            CssBuilder
                .Clear()
                .Add("m-application")
                .Add(() =>
                {
                    var suffix = LeftToRight ? "ltr" : "rtl";
                    return $"{prefix}--is-{suffix}";
                })
                .AddTheme(Dark);

            WrapCssBuilder.Add("m-application--wrap");

            Attributes.Add("data-app", true);
        }
    }
}
