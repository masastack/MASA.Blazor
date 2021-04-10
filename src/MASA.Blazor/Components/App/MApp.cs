using BlazorComponent;
using Microsoft.AspNetCore.Components;

namespace MASA.Blazor.Components.App
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

            ClassMapper
                .Add("m-application")
                .Add("m-application--wrap")
                .Get(() =>
                {
                    var suffix = LeftToRight ? "ltr" : "rtl";
                    return $"{prefix}--is-{suffix}";
                })
                .Get(() =>
                {
                    var suffix = Dark ? "dark" : "light";
                    return $"theme--{suffix}";
                });

        }
    }
}
