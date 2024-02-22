using Microsoft.AspNetCore.Components.Web;
using StyleBuilder = Masa.Blazor.Core.StyleBuilder;

namespace Masa.Blazor
{
    public partial class MBanner : MasaComponentBase
    {
        protected bool IsSticky => Sticky || App;

        [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

        [Parameter] public bool App { get; set; }

        [Parameter] public string? Icon { get; set; }

        [Parameter] public string? IconColor { get; set; }

        [Parameter] public string? Color { get; set; }

        [Parameter] [MasaApiParameter(0)] public StringNumber Elevation { get; set; } = "0";

        [Parameter] public bool SingleLine { get; set; }

        [Parameter] public bool Sticky { get; set; }

        [Parameter] public RenderFragment? IconContent { get; set; }

        [Parameter] public RenderFragment<Action>? ActionsContent { get; set; }

        [Parameter] public EventCallback<bool> ValueChanged { get; set; }

        [Parameter] public EventCallback<MouseEventArgs> OnIconClick { get; set; }

        [Parameter] public RenderFragment? ChildContent { get; set; }

        [Parameter] [MasaApiParameter(true)] public bool Value { get; set; } = true;

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

        /// <summary>
        /// This should be down in next version
        /// </summary>
        public bool Mobile { get; }

        public bool HasIcon => !string.IsNullOrWhiteSpace(Icon) || IconContent != null;

        public RenderFragment? ComputedActionsContent => ActionsContent?.Invoke(() =>
        {
            Value = false;
            ValueChanged.InvokeAsync(Value);
        });

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
        }

        private Block _block = new("m-banner");

        protected override IEnumerable<string> BuildComponentClass()
        {
            return _block.Modifier(HasIcon)
                .And("is-mobile", Mobile)
                .And(SingleLine)
                .And("sticky", IsSticky)
                .AddBackgroundColor(Color)
                .AddTheme(IsDark, IndependentTheme)
                .AddClass("m-sheet")
                .AddElevation(Elevation)
                .GenerateCssClasses();
        }

        protected override IEnumerable<string> BuildComponentStyle()
        {
            return StyleBuilder.Create()
                .Add("top", "0")
                .AddIf("position", "sticky", IsSticky)
                .AddIf("z-index", "1", IsSticky)
                .GenerateCssStyles();
        }

        protected virtual async Task HandleOnIconClickAsync(MouseEventArgs args)
        {
            if (OnIconClick.HasDelegate)
            {
                await OnIconClick.InvokeAsync(args);
            }
        }
    }
}