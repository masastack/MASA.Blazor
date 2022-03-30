using System.ComponentModel;

namespace Masa.Blazor
{
    public partial class MMain : BMain, IMain
    {
        private readonly string[] _applicationProperties = new string[]
        {
            "Top","Bar","Right","Footer","InsetFooter","Bottom","Left"
        };

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
            if (_applicationProperties.Contains(e.PropertyName))
            {
                InvokeStateHasChanged();
            }
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

            AbstractProvider
                .ApplyMainDefault();
        }

        protected override void OnParametersSet()
        {
            MasaBlazor.Application.IsBooted = true;
        }

        protected override void Dispose(bool disposing)
        {
            MasaBlazor.Application.PropertyChanged -= OnApplicationPropertyChanged;
            base.Dispose(disposing);
        }
    }
}
