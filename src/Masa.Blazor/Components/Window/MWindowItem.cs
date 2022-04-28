using BlazorComponent.Web;
using Element = BlazorComponent.Web.Element;

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

        protected override async Task HandleOnBefore(ElementReference el)
        {
            if (InTransition)
            {
                return;
            }

            // Initialize transition state here.
            InTransition = true;

            if (WindowGroup.TransitionCount == 0)
            {
                // set initial height for height transition.
                var elementInfo = await Js.InvokeAsync<Element>(JsInteropConstants.GetDomInfo, el);
                WindowGroup.TransitionHeight = elementInfo.ClientHeight;
            }

            WindowGroup.TransitionCount++;

            StateHasChanged();
            Console.WriteLine($"WindowGroup.TransitionCount: {WindowGroup.TransitionCount}");
        }

        protected override Task HandleOnAfter(ElementReference el)
        {
            if (!InTransition)
            {
                return Task.CompletedTask;
            }

            InTransition = false;

            if (WindowGroup.TransitionCount > 0)
            {
                WindowGroup.TransitionCount--;

                // Remove container height if we are out of transition.
                if (WindowGroup.TransitionCount == 0)
                {
                    WindowGroup.TransitionHeight = 0;
                }
            }

            Console.WriteLine($"WindowGroup.TransitionCount: {WindowGroup.TransitionCount}");

            return Task.CompletedTask;
        }

        protected override Task HandleOnEnter(ElementReference el)
        {
            if (!InTransition)
            {
                return Task.CompletedTask;
            }

            NextTick(async () =>
            {
                if (!string.IsNullOrEmpty(ComputedTransition) || !InTransition)
                {
                    return;
                }

                // Set transition target height.
                var elementInfo = await Js.InvokeAsync<Element>(JsInteropConstants.GetDomInfo, el);
                WindowGroup.TransitionHeight = elementInfo.ClientHeight;

                StateHasChanged();
            });

            return Task.CompletedTask;
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
