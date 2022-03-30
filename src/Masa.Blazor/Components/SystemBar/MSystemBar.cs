using System.Text.RegularExpressions;

namespace Masa.Blazor
{
    public partial class MSystemBar : BSystemBar, IThemeable
    {
        [Parameter]
        public string Color { get; set; }


        [Parameter]
        public StringNumber Height { get; set; }

        [Parameter]
        public bool LightsOut { get; set; }

        [Parameter]
        public bool Window { get; set; }

        [Parameter]
        public bool Absolute { get; set; }

        [Parameter]
        public bool App { get; set; }

        [Parameter]
        public bool Fixed { get; set; }

        [Inject]
        public MasaBlazor MasaBlazor { get; set; }

        private StringNumber ComputedHeight => Height != null ?
            (Regex.IsMatch(Height.ToString(), "^[0-9]*$") ? Height.ToInt32() : Height) :
            (Window ? 32 : 24);

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-system-bar")
                        .AddTheme(IsDark)
                        .AddBackgroundColor(Color)
                        .AddIf("m-system-bar--lights-out", () => LightsOut)
                        .AddIf("m-system-bar--absolute", () => Absolute)
                        .AddIf("m-system-bar--fixed", () => !Absolute && (App || Fixed))
                        .AddIf("m-system-bar--window", () => Window);
                }, styleBuilder =>
                {
                    styleBuilder
                        .Add($"height:{ComputedHeight.ToUnit()}");
                });
        }

        protected override async Task OnParametersSetAsync()
        {
            await UpdateApplicationAsync();
        }

        protected async Task UpdateApplicationAsync()
        {
            if (!App)
            {
                return;
            }

            var height = ComputedHeight.ToDouble();
            MasaBlazor.Application.Bar = height > 0 ? height : await GetClientHeightAsync();
        }

        private async Task<double> GetClientHeightAsync()
        {
            if (Ref.Id == null)
            {
                return 0;
            }

            var element = await JsInvokeAsync<BlazorComponent.Web.Element>(JsInteropConstants.GetDomInfo, Ref);
            return element?.ClientHeight ?? 0;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            RemoveApplication();
        }

        private void RemoveApplication()
        {
            if (!App)
            {
                return;
            }

            MasaBlazor.Application.Bar = 0;
        }
    }
}
