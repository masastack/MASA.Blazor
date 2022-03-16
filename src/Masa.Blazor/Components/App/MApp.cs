using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorComponent;
using BlazorComponent.Web;
using Microsoft.AspNetCore.Components;

namespace Masa.Blazor
{
    /// <summary>
    /// Root for application
    /// </summary>
    public partial class MApp : BApp, IThemeable
    {
        /// <summary>
        /// Whether to display from left to right
        /// </summary>
        [Parameter]
        public bool LeftToRight { get; set; } = true;

        [Inject]
        public HeadJsInterop HeadJsInterop { get; set; }

        [Inject]
        public MasaBlazor MasaBlazor { get; set; }

        [Inject]
        public Window Window { get; set; }

        protected ThemeCssBuilder ThemeCssBuilder { get; } = new ThemeCssBuilder();

        protected override Task OnInitializedAsync()
        {
            if (Variables.Theme != null)
                HeadJsInterop.InsertAdjacentHTML("beforeend", ThemeCssBuilder.Build());

            return base.OnInitializedAsync();
        }

        protected override void SetComponentClass()
        {
            var prefix = "m-application";

            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-application")
                        .Add(() =>
                        {
                            var suffix = LeftToRight ? "ltr" : "rtl";
                            return $"{prefix}--is-{suffix}";
                        })
                        .AddTheme(IsDark);
                })
                .Apply("wrap", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-application--wrap");
                });

            Attributes.Add("data-app", true);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await MasaBlazor.Breakpoint.InitAsync();
                await Window.InitializeAsync();
            }
        }
    }
}
