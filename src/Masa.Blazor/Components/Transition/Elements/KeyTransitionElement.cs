using Masa.Blazor.Components.Transition;

namespace Masa.Blazor;

public class KeyTransitionElement<TValue> : TransitionElementBase<TValue>
{
    private KeyTransitionElementState<TValue>[]? _states;

    private IEnumerable<KeyTransitionElementState<TValue>> ComputedStates =>
        States.Where(state => !state.IsEmpty);

    private KeyTransitionElementState<TValue>[] States =>
        _states ??= new KeyTransitionElementState<TValue>[]
        {
            new(this),
            new(this)
        };

    internal override TransitionState CurrentState
    {
        get
        {
            if (Transition is not null && Transition.Mode == TransitionMode.InOut)
            {
                if (States[1].TransitionState != TransitionState.None)
                {
                    return States[1].TransitionState;
                }

                return States[0].TransitionState;
            }
            else
            {
                if (States[0].TransitionState != TransitionState.None)
                {
                    return States[0].TransitionState;
                }

                return States[1].TransitionState;
            }
        }
        set
        {
            States[0].TransitionState = value;
            States[1].TransitionState = value;
        }
    }

    protected override void StartTransition()
    {
        if (!States[1].IsEmpty)
        {
            //Last transition not complete yet
            States[1].CopyTo(States[0]);
        }

        States[1].Key = Value;

        //First render,don't trigger transition
        if (ComputedStates.Count() < 2)
        {
            return;
        }

        States[0].TransitionState = TransitionState.Leave;
        States[1].TransitionState = TransitionState.Enter;
    }

    protected override async Task NextAsync(TransitionState state)
    {
        if (!Transition!.Mode.HasValue)
        {
            switch (state)
            {
                case TransitionState.Leave:
                case TransitionState.Enter:
                    await RequestNextState(TransitionState.LeaveTo, TransitionState.EnterTo);
                    break;
            }
        }
        else if (Transition.Mode == TransitionMode.OutIn)
        {
            switch (state)
            {
                case TransitionState.Leave:
                    await RequestNextState(TransitionState.LeaveTo, TransitionState.None);
                    break;
                case TransitionState.Enter:
                    await RequestNextState(TransitionState.None, TransitionState.EnterTo);
                    break;
            }
        }
        else if (Transition.Mode == TransitionMode.InOut)
        {
            switch (state)
            {
                case TransitionState.Enter:
                    await RequestNextState(TransitionState.None, TransitionState.EnterTo);
                    break;
                case TransitionState.Leave:
                    await RequestNextState(TransitionState.LeaveTo, TransitionState.None);
                    break;
            }
        }
    }

    public override Task OnTransitionEnd(string referenceId, LeaveEnter transition)
    {
        if (referenceId != Reference.Id)
        {
            return Task.CompletedTask;
        }

        if (Transition!.Mode == TransitionMode.OutIn)
        {
            if (CurrentState == TransitionState.LeaveTo)
            {
                NextState(TransitionState.None, TransitionState.Enter);
            }
            else if (CurrentState == TransitionState.EnterTo)
            {
                NextState(TransitionState.None, TransitionState.None);
            }
        }
        else if (Transition.Mode == TransitionMode.InOut)
        {
            // TODO: InOut mode
        }
        else if (!Transition.Mode.HasValue)
        {
            if (CurrentState == TransitionState.LeaveTo)
            {
                //Remove old element and set new element to first position
                States[0].Reset();

                NextState(TransitionState.None, TransitionState.None);
            }
        }

        return Task.CompletedTask;
    }

    private void NextState(TransitionState oldTransitionState, TransitionState newTransitionState)
    {
        States[0].TransitionState = oldTransitionState;
        States[1].TransitionState = newTransitionState;

        StateHasChanged();
    }

    private async Task RequestNextState(TransitionState first, TransitionState second)
    {
        await RequestAnimationFrameAsync(() =>
        {
            NextState(first, second);
            return Task.CompletedTask;
        });
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        if (NoTransition)
        {
            base.BuildRenderTree(builder);
            return;
        }

        List<KeyTransitionElementState<TValue>> filteredStates = new();

        switch (Transition?.Mode)
        {
            case TransitionMode.OutIn:
            {
                var state = ComputedStates.FirstOrDefault(s => s.TransitionState != TransitionState.None);

                state ??= ComputedStates.LastOrDefault();

                if (state is not null)
                {
                    filteredStates.Add(state);
                }

                break;
            }
            case TransitionMode.InOut:
                // TODO: in-out
                break;
            default:
                filteredStates = ComputedStates
                    .ToList();
                break;
        }

        foreach (var state in filteredStates)
        {
            var sequence = 0;

            builder.OpenElement(sequence++, Tag);

            builder.AddMultipleAttributes(sequence++, AdditionalAttributes);
            builder.AddAttribute(sequence++, "class", state.ComputedClass);
            builder.AddAttribute(sequence++, "style", state.ComputedStyle);
            builder.AddContent(sequence++, RenderChildContent(state.Key));
            builder.AddElementReferenceCapture(sequence, reference =>
            {
                ReferenceCaptureAction?.Invoke(reference);
                Reference = reference;
            });
            builder.SetKey(state.Key);

            builder.CloseComponent();
        }
    }

    private RenderFragment RenderChildContent(TValue? key) => builder =>
    {
        builder.OpenComponent<MShouldRender>(0);
        builder.AddAttribute(1, nameof(MShouldRender.Value), EqualityComparer<TValue>.Default.Equals(key, Value));
        builder.AddAttribute(2, nameof(ChildContent), ChildContent);
        builder.CloseComponent();
    };
}