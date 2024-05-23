using StyleBuilder = Masa.Blazor.Core.StyleBuilder;

namespace Masa.Blazor
{
    public partial class MProgressLinear : MasaComponentBase
    {
        [Inject] public Document Document { get; set; } = null!;

        [Inject] protected MasaBlazor MasaBlazor { get; set; } = null!;

        [Parameter] public bool Absolute { get; set; }

        [Parameter] public bool Fixed { get; set; }

        [Parameter] public bool Query { get; set; }

        [Parameter] public bool Rounded { get; set; }

        [Parameter] public bool Striped { get; set; }

        [Parameter] public bool Bottom { get; set; }

        [Parameter] public bool Top { get; set; }

        [Parameter] [MasaApiParameter(4)] public StringNumber Height { get; set; } = 4;

        [Parameter] [MasaApiParameter(true)] public bool Active { get; set; } = true;

        [Parameter] public string? BackgroundColor { get; set; }

        [Parameter] public double? BackgroundOpacity { get; set; }

        [Parameter] public bool Stream { get; set; }

        [Parameter] [MasaApiParameter(100)] public double BufferValue { get; set; } = 100;

        [Parameter] public bool Reverse { get; set; }

        [Parameter] public RenderFragment<double>? ChildContent { get; set; }

        [Obsolete("Use ValueChanged instead.")]
        [Parameter]
        public EventCallback<double> OnChange { get; set; }

        [Parameter] public EventCallback<double> ValueChanged { get; set; }

        [Parameter] public string? Color { get; set; }

        [Parameter] public bool Indeterminate { get; set; }

        [Parameter] public double Value { get; set; }

        [Parameter] public bool Dark { get; set; }

        [Parameter] public bool Light { get; set; }

        [CascadingParameter(Name = "IsDark")] public bool CascadingIsDark { get; set; }

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

                return CascadingIsDark;
            }
        }

        private bool IsReversed => MasaBlazor.RTL != Reverse;

        protected bool IsVisible { get; set; } = true;

        public async Task HandleOnClickAsync(MouseEventArgs args)
        {
            if (!Reactive)
            {
                return;
            }

            var el = Document.GetElementByReference(Ref);
            if (el is null) return;

            var rect = await el.GetBoundingClientRectAsync();

            var value = args.OffsetX / rect.Width * 100;
            value = Math.Round(value, 0);

            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(value);
            }
            else
            {
                Value = value;
            }

            if (OnChange.HasDelegate)
            {
                await OnChange.InvokeAsync(value);
            }
            else
            {
                Value = value;
            }
        }

        protected bool Reactive => ValueChanged.HasDelegate || OnChange.HasDelegate;

        protected int NormalizedValue => NormalizeValue(Value);

        protected int NormalizedBuffer => NormalizeValue(BufferValue);

        private bool IndependentTheme =>
            (IsDirtyParameter(nameof(Dark)) && Dark) || (IsDirtyParameter(nameof(Light)) && Light);

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

#if NET8_0_OR_GREATER
            if (MasaBlazor.IsSsr && !IndependentTheme)
            {
                CascadingIsDark = MasaBlazor.Theme.Dark;
            }
#endif
            if (string.IsNullOrWhiteSpace(Color))
            {
                Color = "primary";
            }
        }

        private Block _block = new("m-progress-linear");

        protected override IEnumerable<string> BuildComponentClass()
        {
            return _block.Modifier(Absolute)
                .And(Fixed)
                .And(Query)
                .And(Reactive)
                .And("reverse", IsReversed)
                .And(Rounded)
                .And(Striped)
                .And("visible", IsVisible)
                .AddTheme(IsDark, IndependentTheme)
                .GenerateCssClasses();
        }

        protected override IEnumerable<string> BuildComponentStyle()
        {
            return StyleBuilder.Create()
                .AddIf("bottom", "0", Bottom)
                .AddIf("top", "0", Top)
                .AddIf("height", Height.ToUnit(), Active)
                .AddTextColor(Color)
                .GenerateCssStyles();
        }

        private static int NormalizeValue(StringNumber value)
        {
            int value1 = value.ToInt32();

            if (value1 < 0) return 0;
            if (value1 > 100) return 100;
            return value1;
        }
    }
}