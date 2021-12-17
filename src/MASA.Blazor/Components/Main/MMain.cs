using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System.ComponentModel;

namespace MASA.Blazor
{
    public partial class MMain : BMain, IMain
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Inject]
        public MasaBlazor MasaBlazor { get; set; }

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-main");
                }, styleBuilder =>
                {
                    styleBuilder
                        .Add($"padding-top:{MasaBlazor.Application.Top + MasaBlazor.Application.Bar}px")
                        .Add($"padding-right:{MasaBlazor.Application.Right}px")
                        .Add($"padding-bottom:{MasaBlazor.Application.Footer + MasaBlazor.Application.InsetFooter + MasaBlazor.Application.Bottom}px")
                        .Add($"padding-left:{MasaBlazor.Application.Left}px");
                })
                .Apply("wrap", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-main__wrap");
                });

            Attributes.Add("data-booted", true);
            MasaBlazor.Application.IsBooted = true;
            AbstractProvider
                .ApplyMainDefault();
        }
    }
}
