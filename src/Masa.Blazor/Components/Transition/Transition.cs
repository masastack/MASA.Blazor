using Masa.Blazor.Components.Transition;

namespace Masa.Blazor
{
    public class Transition : ComponentBase
    {
        [Inject] protected IJSRuntime Js { get; set; } = null!;

        [Parameter] public string? Name { get; set; }

        [Parameter] public string? Origin { get; set; }

        [Parameter] public int Duration { get; set; } // TODO: 先实现css的动画时间

        [Parameter] public bool LeaveAbsolute { get; set; }

        [Parameter] public TransitionMode? Mode { get; set; }

        [Parameter] public RenderFragment? ChildContent { get; set; }

        [Parameter] public Func<ElementReference, Task>? OnBeforeEnter { get; set; }

        [Parameter] public Func<ElementReference, Task>? OnEnter { get; set; }

        [Parameter] public Func<ElementReference, Task>? OnAfterEnter { get; set; }

        [Parameter] public Func<ElementReference, Task>? OnEnterCancelled { get; set; }

        [Parameter] public Func<ElementReference, Task>? OnBeforeLeave { get; set; }

        [Parameter] public Func<ElementReference, Task>? OnLeave { get; set; }

        [Parameter] public Func<ElementReference, Task>? OnAfterLeave { get; set; }

        [Parameter] public Func<ElementReference, Task>? OnLeaveCancelled { get; set; }

        /// <summary>
        /// The only child element that running the transition in the Transition's ChildContent.
        /// </summary>
        internal ElementReference? ElementReference { get; set; }

        internal TransitionElementBase? TransitionElement { get; set; }

        public virtual string? GetClass(TransitionState transitionState)
        {
            return transitionState switch
            {
                TransitionState.Enter => $"{Name}-enter {Name}-enter-active",
                TransitionState.EnterTo => $"{Name}-enter-active {Name}-enter-to",
                TransitionState.Leave => $"{Name}-leave {Name}-leave-active",
                TransitionState.LeaveTo => $"{Name}-leave-active {Name}-leave-to",
                _ => null
            };
        }

        public virtual string? GetStyle(TransitionState transitionState)
        {
            if (Origin != null && transitionState != TransitionState.None)
            {
                return $"transform-origin:{Origin}";
            }

            return null;
        }

        public virtual async Task BeforeEnter(TransitionElementBase element)
        {
            if (OnBeforeEnter is not null)
            {
                await OnBeforeEnter.Invoke(element.Reference);
            }
        }

        public virtual async Task Enter(TransitionElementBase element)
        {
            if (OnEnter is not null)
            {
                await OnEnter.Invoke(element.Reference);
            }
        }

        public virtual async Task AfterEnter(TransitionElementBase element)
        {
            if (OnAfterEnter is not null)
            {
                await OnAfterEnter.Invoke(element.Reference);
            }
        }

        public virtual async Task EnterCancelled(TransitionElementBase element)
        {
            if (OnEnterCancelled is not null)
            {
                await OnEnterCancelled.Invoke(element.Reference);
            }
        }

        public virtual async Task BeforeLeave(TransitionElementBase element)
        {
            if (OnBeforeLeave is not null)
            {
                await OnBeforeLeave.Invoke(element.Reference);
            }
        }

        public virtual async Task Leave(TransitionElementBase element)
        {
            if (LeaveAbsolute)
            {
                element.ElementInfo =
                    await Js.InvokeAsync<BlazorComponent.Web.Element>(JsInteropConstants.GetDomInfo, element.Reference);
            }

            if (OnLeave is not null)
            {
                await OnLeave.Invoke(element.Reference);
            }
        }

        public virtual async Task LeaveCancelled(TransitionElementBase element)
        {
            if (OnLeaveCancelled is not null)
            {
                await OnLeaveCancelled.Invoke(element.Reference);
            }
        }

        public virtual async Task AfterLeave(TransitionElementBase element)
        {
            if (OnAfterLeave is not null)
            {
                await OnAfterLeave.Invoke(element.Reference);
            }
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenComponent<CascadingValue<Transition>>(0);
            builder.AddAttribute(1, nameof(CascadingValue<Transition>.Value), this);
            builder.AddAttribute(2, nameof(CascadingValue<Transition>.IsFixed), true);
            builder.AddAttribute(3, nameof(CascadingValue<Transition>.ChildContent), ChildContent);
            builder.CloseComponent();
        }
    }
}