using BlazorComponent.Web;
using Element = BlazorComponent.Element;

namespace Masa.Blazor
{
    public partial class MWindow : BWindow
    {
        [Inject]
        public MasaBlazor MasaBlazor { get; set; }

        public bool InternalReverse => MasaBlazor.RTL ? !Reverse : Reverse;

        public string ComputedTransition
        {
            get
            {
                //TODO:isBooted

                var axis = Vertical ? "y" : "x";
                var reverse = InternalReverse ? !IsReverse : IsReverse;
                var direction = reverse ? "-reverse" : "";

                return $"m-window-{axis}{direction}-transition";
            }
        }

        public EventCallback<Element> OnLeave => EventCallback.Factory.Create<Element>(this, HandleOnLeaveAsync);

        private async Task HandleOnLeaveAsync(Element element)
        {
            TransitionCount++;

            var el = Document.GetElementByReference(element.Reference);
            TransitionHeight = await el.GetClientHeightAsync();
        }

        public EventCallback<Element> OnEnterTo => EventCallback.Factory.Create<Element>(this, HandleOnEnterToAsync);

        private async Task HandleOnEnterToAsync(Element element)
        {
            TransitionCount--;

            var el = Document.GetElementByReference(element.Reference);
            TransitionHeight = await el.GetClientHeightAsync();
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            ActiveClass = "m-window-item--active";
            PrevIcon ??= "mdi-chevron-left";
            NextIcon ??= "mdi-chevron-right";
        }

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-window")
                        .Add("m-item-group")
                        .AddIf("m-window--show-arrows-on-hover", () => ShowArrowsOnHover);
                })
                .Apply("container", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-window__container")
                        .AddIf("m-window__container--is-active", () => IsActive);
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddHeight(TransitionHeight);
                })
                .Apply("prev", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-window__prev");
                })
                .Apply("next", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-window__next");
                });

            AbstractProvider
                .Apply<BButton, MButton>()
                .Apply<BIcon, MIcon>();
        }
    }
}