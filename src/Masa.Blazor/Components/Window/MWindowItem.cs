using BlazorComponent.Web;

namespace Masa.Blazor
{
    public class MWindowItem : BWindowItem
    {
        [Parameter]
        public string Transition { get; set; }

        [Parameter]
        public string ReverseTransition { get; set; }

        [CascadingParameter]
        public MWindow WindowGroup { get; set; }

        [Inject]
        public Document Document { get; set; }

        protected bool InTransition { get; set; }

        protected override string ComputedTransition
        {
            get
            {
                if (WindowGroup != null && !WindowGroup.InternalReverse)
                {
                    return Transition ?? WindowGroup.ComputedTransition;
                }

                return ReverseTransition ?? WindowGroup?.ComputedTransition;
            }
        }

        protected override async Task OnLeave(BlazorComponent.Element element)
        {
            await WindowGroup.OnLeave.InvokeAsync(element);
        }

        protected override async Task OnEnterTo(BlazorComponent.Element element)
        {
            await WindowGroup.OnEnterTo.InvokeAsync(element);
        }

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-window-item")
                        .AddIf("m-window-item--active", () => InternalIsActive);
                });
        }
    }
}