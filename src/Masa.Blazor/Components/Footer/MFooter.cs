using System.ComponentModel;

namespace Masa.Blazor
{
    public partial class MFooter : BFooter, IThemeable
    {
        private readonly string[] _applicationProperties = new string[]
        {
            "Bottom","Left","Right"
        };



        [Parameter]
        public bool Absolute { get; set; }

        [Parameter]
        public bool App { get; set; }

        [Parameter]
        public string Color { get; set; }

        [Parameter]
        public StringNumber Elevation { get; set; }

        [Parameter]
        public bool Fixed { get; set; }

        [Parameter]
        public StringNumber Height { get; set; } = "auto";

        [Parameter]
        public StringNumber Width { get; set; }

        [Parameter]
        public bool Inset { get; set; }

        [Parameter]
        public StringNumber MaxHeight { get; set; }

        [Parameter]
        public StringNumber MinHeight { get; set; }

        [Parameter]
        public StringNumber MaxWidth { get; set; }

        [Parameter]
        public StringNumber MinWidth { get; set; }

        [Parameter]
        public bool Padless { get; set; }

        [Parameter]
        public StringBoolean Rounded { get; set; }

        [Parameter]
        public bool Tile { get; set; }

        [Inject]
        public MasaBlazor MasaBlazor { get; set; }

        protected StringNumber ComputedBottom => ComputeBottom();

        protected StringNumber ComputeBottom()
        {
            if (!IsPositioned) return null;

            return App && Inset ? MasaBlazor.Application.Bottom : 0;
        }

        protected StringNumber ComputedLeft => ComputeLeft();

        protected StringNumber ComputeLeft()
        {
            if (!IsPositioned) return null;

            return App && Inset ? MasaBlazor.Application.Left : 0;
        }

        protected StringNumber ComputedRight => ComputeRight();

        protected StringNumber ComputeRight()
        {
            if (!IsPositioned) return null;

            return App && Inset ? MasaBlazor.Application.Right : 0;
        }

        protected bool IsPositioned => Absolute || Fixed || App;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            MasaBlazor.Application.PropertyChanged += ApplicationPropertyChanged;
        }

        private void ApplicationPropertyChanged(object sender, PropertyChangedEventArgs e)
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
                        .Add("m-footer")
                        .Add("m-sheet")
                        .AddTheme(IsDark)
                        .AddBackgroundColor(Color)
                        .AddIf("m-footer--absolute", () => Absolute)
                        .AddIf("m-footer--fixed", () => !Absolute && (App || Fixed))
                        .AddIf("m-footer--padless", () => Padless)
                        .AddIf("m-footer--inset", () => Inset)
                        .AddElevation(Elevation)
                        .AddRounded(Rounded, Tile);
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddBackgroundColor(Color)
                        .AddHeight(Height)
                        .AddWidth(Width)
                        .AddMaxHeight(MaxHeight)
                        .AddMaxWidth(MaxWidth)
                        .AddMinHeight(MinHeight)
                        .AddMinWidth(MinWidth)
                        .AddIf($"left:{ComputedLeft.ToUnit()}", () => ComputedLeft != null)
                        .AddIf($"right:{ComputedRight.ToUnit()}", () => ComputedRight != null)
                        .AddIf($"bottom:{ComputedBottom.ToUnit()}", () => ComputedBottom != null);
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

            var val = Height.ToDouble() > 0 ? Height.ToDouble() : await GetClientHeightAsync();
            if (Inset)
                MasaBlazor.Application.InsetFooter = val;
            else
                MasaBlazor.Application.Footer = val;
        }

        private async Task<double> GetClientHeightAsync()
        {
            if (Ref.Id == null)
            {
                return 0;
            }

            var element = await JsInvokeAsync<BlazorComponent.Web.Element>(JsInteropConstants.GetDomInfo, Ref);
            return element.ClientHeight;
        }

        protected override void Dispose(bool disposing)
        {
            RemoveApplication();
            MasaBlazor.Application.PropertyChanged -= ApplicationPropertyChanged;
        }

        private void RemoveApplication()
        {
            if (!App)
            {
                return;
            }

            if (Inset)
                MasaBlazor.Application.InsetFooter = 0;
            else
                MasaBlazor.Application.Footer = 0;
        }
    }
}