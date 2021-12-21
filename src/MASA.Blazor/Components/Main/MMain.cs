using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System.ComponentModel;

namespace MASA.Blazor
{
    public partial class MMain : BMain, IMain
    {
        private readonly string[] _applicationProperties = new string[]
        {
            "Top","Bar","Right","Footer","InsetFooter","Bottom","Left"
        };
        private readonly DelayTask _delayTask = new(100);
        private bool _shouldRender = true;

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Inject]
        public MasaBlazor MasaBlazor { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            MasaBlazor.Application.PropertyChanged += OnApplicationPropertyChanged;
        }

        private void OnApplicationPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //We will change this when really data-driven finished
            _shouldRender = false;
            _ = _delayTask.Run(async () =>
              {
                  _shouldRender = true;
                  if (_applicationProperties.Contains(e.PropertyName))
                  {
                      await InvokeStateHasChangedAsync();
                  }
              });
        }

        protected override bool ShouldRender()
        {
            return _shouldRender;
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
