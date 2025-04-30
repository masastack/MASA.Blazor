using StyleBuilder = Masa.Blazor.Core.StyleBuilder;

namespace Masa.Blazor
{
    public partial class MBanner : ThemeComponentBase
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

        private static Block _block = new("m-banner");
        private ModifierBuilder _modifierBuilder = _block.CreateModifierBuilder();

        protected override IEnumerable<string> BuildComponentClass()
        {
            yield return _modifierBuilder
                .Add(HasIcon, SingleLine)
                .Add("is-mobile", Mobile)
                .Add("sticky", IsSticky)
                .AddBackgroundColor(Color)
                .AddTheme(ComputedTheme)
                .AddClass("m-sheet")
                .AddElevation(Elevation)
                .Build();
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