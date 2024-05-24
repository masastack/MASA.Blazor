using Masa.Blazor.Components.Transition;

namespace Masa.Blazor;

public class ToggleableTransitionElement : TransitionElementBase<bool>
{
    [Parameter(CaptureUnmatchedValues = true)]
    public override IDictionary<string, object> AdditionalAttributes
    {
        get
        {
            var attributes = base.AdditionalAttributes ?? new Dictionary<string, object>();

            attributes["class"] = ComputedClass;
            attributes["style"] = ComputedStyle;

            return attributes;
        }
        set => base.AdditionalAttributes = value;
    }

    private TransitionState State { get; set; }

    protected bool LazyValue { get; private set; }

    protected override string? ComputedClass
    {
        get
        {
            var transitionClass = Transition?.GetClass(State);
            if (transitionClass != null)
            {
                var stringBuilder = new StringBuilder();
                stringBuilder.Append(Class);
                stringBuilder.Append(' ');
                stringBuilder.Append(transitionClass);
                return stringBuilder.ToString().TrimEnd();
            }

            return Class;
        }
    }

    protected override string? ComputedStyle
    {
        get
        {
            var style = Transition?.GetStyle(State);
            if (style != null)
            {
                var stringBuilder = new StringBuilder();
                stringBuilder.Append(base.ComputedStyle);
                stringBuilder.Append(style);
                stringBuilder.Append("; ");
                return stringBuilder.ToString().TrimEnd();
            }

            return base.ComputedStyle;
        }
    }

    internal override TransitionState CurrentState
    {
        get => State;
        set => State = value;
    }

    protected override void OnParametersSet()
    {
        if (NoTransition)
        {
            if (Value)
            {
                ShowElement();
            }
            else
            {
                HideElement();
            }
        }
    }

    /// <summary>
    /// Value should be true when the status is Enter(To),
    /// Value should be false when the status is Leave(To),
    /// otherwise should continue to move
    /// </summary>
    protected override bool CanMoveNext => !RequestingAnimationFrame || (IsLeaveTransitionState ? !Value : Value);

    protected override void StartTransition()
    {
        //Don't trigger transition in first render
        if (FirstRender)
        {
            ShowElement();
            return;
        }

        if (Value)
        {
            ShowElement();
            State = TransitionState.Enter;
        }
        else
        {
            State = TransitionState.Leave;
        }
    }

    protected override async Task NextAsync(TransitionState state)
    {
        switch (state)
        {
            case TransitionState.Enter:
                await RequestNextStateAsync(TransitionState.EnterTo, true);
                break;
            case TransitionState.Leave:
                await RequestNextStateAsync(TransitionState.LeaveTo, false);
                break;
        }
    }

    public override async Task OnTransitionEnd(string referenceId, LeaveEnter transition)
    {
        if (referenceId != Reference.Id)
        {
            return;
        }

        if (transition == LeaveEnter.Enter && CurrentState == TransitionState.EnterTo)
        {
            await NextState(TransitionState.None);
        }
        else if (transition == LeaveEnter.Leave && CurrentState == TransitionState.LeaveTo)
        {
            HideElement();
            await NextState(TransitionState.None);
        }
    }

    private async Task NextState(TransitionState transitionState)
    {
        State = transitionState;
        StateHasChanged();
        await Hooks();
    }

    private async Task RequestNextStateAsync(TransitionState state, bool checkValue)
    {
        await RequestAnimationFrameAsync(async () =>
        {
            if (checkValue != Value) return;

            await NextState(state);
        });
    }

    private void HideElement()
    {
        LazyValue = false;
    }

    private void ShowElement()
    {
        LazyValue = true;
    }
}