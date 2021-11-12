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
        public GlobalConfig GlobalConfig { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            GlobalConfig.Application.PropertyChanged += Application_PropertyChanged;
        }

        private void Application_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            InvokeStateHasChanged();
        }

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
                        .Add($"padding-top:{GlobalConfig.Application.Top + GlobalConfig.Application.Bar}px")
                        .Add($"padding-right:{GlobalConfig.Application.Right}px")
                        .Add($"padding-bottom:{GlobalConfig.Application.Footer + GlobalConfig.Application.InsetFooter + GlobalConfig.Application.Bottom}px")
                        .Add($"padding-left:{GlobalConfig.Application.Left}px");
                })
                .Apply("wrap", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-main__wrap");
                });

            Attributes.Add("data-booted", true);
            GlobalConfig.Application.IsBooted = true;
            AbstractProvider.ApplyMainDefault();
        }
    }
}
