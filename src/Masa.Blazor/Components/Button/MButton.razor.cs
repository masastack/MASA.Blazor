using System.Diagnostics.CodeAnalysis;
using Masa.Blazor.Components.ItemGroup;
using StyleBuilder = Masa.Blazor.Core.StyleBuilder;

namespace Masa.Blazor
{
    public partial class MButton : MRoutableGroupItem<ItemGroupBase>, IThemeable
    {
        public MButton() : base(GroupType.ButtonGroup, "button")
        {
        }

        [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

        public bool Default { get; set; } = true;

        [Parameter] public bool Absolute { get; set; }

        [Parameter] public bool Bottom { get; set; }

        [Parameter] public bool Depressed { get; set; }

        [Parameter] public StringNumber? Elevation { get; set; }

        [Parameter] public virtual bool Icon { get; set; }

        [Parameter] public bool Fab { get; set; }

        [Parameter] public bool Fixed { get; set; }

        [Parameter] public bool Large { get; set; }

        [Parameter] public bool Left { get; set; }

        [Parameter] public bool Plain { get; set; }

        [Parameter] public bool Right { get; set; }

        [Parameter] public bool Rounded { get; set; }

        [Parameter] public bool Shaped { get; set; }

        [Parameter] public bool Small { get; set; }

        [Parameter] public bool Text { get; set; }

        [Parameter] public bool Tile { get; set; }

        [Parameter]
        public string Type
        {
            get => TypeAttribute;
            set => TypeAttribute = value;
        }

        [Parameter] public bool Top { get; set; }

        [Parameter] public bool XLarge { get; set; }

        [Parameter] public bool XSmall { get; set; }

        [Parameter] public bool Ripple { get; set; } = true;

        [CascadingParameter(Name = "IsDark")] public bool CascadingIsDark { get; set; }

        [Parameter] public bool Block { get; set; }

        [Parameter] public string? Color { get; set; }

        [Parameter] public StringNumber? Height { get; set; }

        [Parameter] public RenderFragment? LoaderContent { get; set; }

        [Parameter] public virtual bool Loading { get; set; }

        [Parameter] public StringNumber? MaxHeight { get; set; }

        [Parameter] public StringNumber? MaxWidth { get; set; }

        [Parameter] public StringNumber? MinHeight { get; set; }

        [Parameter] public StringNumber? MinWidth { get; set; }

        [Parameter] public bool Outlined { get; set; }

        [Parameter] public StringNumber? Width { get; set; }

        [Parameter] public bool OnClickStopPropagation { get; set; }

        [Parameter] public bool OnClickPreventDefault { get; set; }

        [Parameter] public bool Dark { get; set; }

        [Parameter] public bool Light { get; set; }

        [Parameter] public bool? Show { get; set; }

        [Parameter] public string? Key { get; set; }

        [Parameter]
        [MasaApiParameter(ReleasedOn = "v1.5.0")]
        public string? IconName { get; set; }

        [Parameter]
        [MasaApiParameter(ReleasedOn = "v1.5.0")]
        public string? LeftIconName { get; set; }

        [Parameter]
        [MasaApiParameter(ReleasedOn = "v1.5.0")]
        public string? RightIconName { get; set; }

        /// <summary>
        /// Determine whether rendering a loader component
        /// </summary>
        protected bool HasLoader { get; set; }

        /// <summary>
        /// Set the button's type attribute
        /// </summary>
        protected string TypeAttribute { get; set; } = "button";

        [MemberNotNullWhen(true, nameof(IconName))]
        protected bool HasBuiltInIcon => !string.IsNullOrWhiteSpace(IconName);

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

        private bool IsIconBtn => Icon || HasBuiltInIcon;

        private bool HasBackground => !(IsIconBtn || Plain || Outlined || Text);

        private bool IsRound => IsIconBtn || Fab;

        private bool IsElevated => !(IsIconBtn || Text || Outlined || Depressed || Disabled || Plain) &&
                                   (Elevation == null || Elevation.TryGetNumber().number > 0);

        protected override bool AfterHandleEventShouldRender() => false;

        private Block _block = new("m-btn");

        protected override IEnumerable<string> BuildComponentClass()
        {
            return _block.Modifier(Absolute)
                .And(Block)
                .And(Bottom)
                .And(Disabled)
                .And(IsElevated)
                .And(Fab)
                .And(Fixed)
                .And("has-bg", HasBackground)
                .And("icon", IsIconBtn)
                .And(Left)
                .And(Loading)
                .And(Outlined)
                .And(Plain)
                .And(Right)
                .And("round", IsRound)
                .And(Rounded)
                .And(Text)
                .And(Tile)
                .And(Top)
                .And("active", InternalIsActive)
                .AddClass(ComputedActiveClass, InternalIsActive)
                .AddClass(CssClassUtils.GetSize(XSmall, Small, Large, XLarge))
                .AddTheme(IsDark, IndependentTheme)
                .AddBackgroundColor(Color, HasBackground)
                .AddTextColor(Color, !HasBackground)
                .AddElevation(Elevation)
                .GenerateCssClasses();
        }

        protected override IEnumerable<string> BuildComponentStyle()
        {
            return StyleBuilder.Create()
                .AddHeight(Height)
                .AddWidth(Width)
                .AddMinWidth(MinWidth)
                .AddMaxWidth(MaxWidth)
                .AddMinHeight(MinHeight)
                .AddMaxHeight(MaxHeight)
                .AddColor(Color, IsIconBtn || Outlined || Plain || Text)
                .GenerateCssStyles();
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            HasLoader = true;
        }

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

            Attributes["ripple"] = Ripple;
        }

        protected async Task HandleOnClick(MouseEventArgs args)
        {
            if (!Fab && args.Detail > 0)
            {
                await Js.InvokeVoidAsync(JsInteropConstants.Blur, Ref);
            }

            if (OnClick.HasDelegate)
            {
                await OnClick.InvokeAsync(args);
            }

            await ToggleAsync();
        }
    }
}