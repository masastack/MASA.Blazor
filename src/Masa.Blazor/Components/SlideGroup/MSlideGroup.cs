namespace Masa.Blazor
{
    public partial class MSlideGroup : BSlideGroup
    {
        public MSlideGroup()
        {
        }

        protected MSlideGroup(GroupType groupType) : base(groupType)
        {
        }

        [Inject]
        protected MasaBlazor MasaBlazor { get; set; } = null!;

        protected override bool RTL => MasaBlazor.RTL;

        protected override void OnInitialized()
        {
            base.OnInitialized();

            MasaBlazor.Breakpoint.OnUpdate += BreakpointOnOnUpdate;
            IsMobile = MasaBlazor.Breakpoint.Mobile;
        }

        private async void BreakpointOnOnUpdate(object? sender, BreakpointChangedEventArgs e)
        {
            if (!e.MobileChanged) return;

            IsMobile = MasaBlazor.Breakpoint.Mobile;
            await InvokeStateHasChangedAsync();
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            ActiveClass ??= "m-slide-item--active";
            NextIcon ??= "$next";
            PrevIcon ??= "$prev";
        }

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            CssProvider
                .Apply(css =>
                {
                    css.Add("m-item-group m-slide-group")
                        .AddIf("m-slide-group--is-overflowing", () => IsOverflowing)
                        .AddIf("m-slide-group--has-affixes", () => HasAffixes);
                })
                .Apply("next", css =>
                {
                    css.Add("m-slide-group__next")
                        .AddIf("m-slide-group__next--disabled", () => !HasNext);
                })
                .Apply("prev", css =>
                {
                    css.Add("m-slide-group__prev")
                        .AddIf("m-slide-group__prev--disabled", () => !HasPrev);
                })
                .Apply("wrapper", css => css.Add("m-slide-group__wrapper"))
                .Apply("content", css => css.Add("m-slide-group__content"));

            AbstractProvider
                .Apply(typeof(BSlideGroupPrev<>), typeof(BSlideGroupPrev<MSlideGroup>))
                .Apply(typeof(BSlideGroupNext<>), typeof(BSlideGroupNext<MSlideGroup>))
                .Apply<BIcon, MIcon>();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            MasaBlazor.Breakpoint.OnUpdate -= BreakpointOnOnUpdate;
        }
    }
}