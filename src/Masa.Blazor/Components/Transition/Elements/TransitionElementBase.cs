using System.Diagnostics.CodeAnalysis;

namespace Masa.Blazor.Components.Transition;

public abstract class TransitionElementBase : MElement
{
    private ElementReference? _reference;

    protected bool ElementReferenceChanged { get; set; }

    internal abstract TransitionState CurrentState { get; set; }

    /// <summary>
    /// The dom information about the transitional element.
    /// </summary>
    internal BlazorComponent.Web.Element? ElementInfo { get; set; }

    public ElementReference Reference
    {
        get => _reference ?? new ElementReference();
        protected set
        {
            if (_reference.HasValue && _reference.Value.Id != value.Id)
            {
                ElementReferenceChanged = true;
            }

            _reference = value;
        }
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, (Tag ?? "div"));
        builder.AddMultipleAttributes(1, AdditionalAttributes);
        builder.AddAttribute(2, "class", ComputedClass);
        builder.AddAttribute(3, "style", ComputedStyle);
        builder.AddContent(4, ChildContent);
        builder.AddElementReferenceCapture(5, reference =>
        {
            Reference = reference;
            ReferenceCaptureAction?.Invoke(reference);
        });
        builder.CloseElement();
    }
}

#if NET6_0
public abstract class TransitionElementBase<TValue> : TransitionElementBase, ITransitionElement, IAsyncDisposable
#else
public abstract class TransitionElementBase<TValue> : TransitionElementBase, ITransitionElement, IAsyncDisposable
    where TValue : notnull
#endif
{
    [Inject] private TransitionJSModule TransitionJSModule { get; set; } = null!;

    [Inject] [NotNull] protected IJSRuntime? Js { get; set; }

    [CascadingParameter] public Blazor.Transition? Transition { get; set; }

    [Parameter, EditorRequired] public TValue Value { get; set; } = default!;

    private TValue? _preValue;
    private DotNetObjectReference<TransitionJsInteropHandle>? _transitionJsInterop;
    private IJSObjectReference? _transitionJSObjectReference;

    protected bool FirstRender { get; private set; } = true;

    /// <summary>
    /// Whether it is a transitional element.
    /// </summary>
    protected bool HavingTransition =>
        !string.IsNullOrWhiteSpace(Transition?.Name) && Transition?.TransitionElement == this;

    /// <summary>
    /// No transition or is not a transitional element.
    /// </summary>
    protected bool NoTransition => !HavingTransition;

    protected override void OnInitialized()
    {
        _transitionJsInterop = DotNetObjectReference.Create(new TransitionJsInteropHandle(this));

        if (Transition is not null && Transition.TransitionElement is null)
        {
            Transition.TransitionElement = this;
        }
    }

    protected override async Task OnParametersSetAsync()
    {
        if (NoTransition)
        {
            return;
        }

        if (!EqualityComparer<TValue>.Default.Equals(Value, _preValue))
        {
            _preValue = Value;

            if (!FirstRender)
            {
                bool? boolValue = null;
                if (Value is bool @bool)
                {
                    boolValue = @bool;
                }

                if (Transition!.Mode is TransitionMode.InOut || (boolValue.HasValue && boolValue.Value))
                {
                    await Transition!.BeforeEnter(this);
                }
                else
                {
                    await Transition!.BeforeLeave(this);
                }
            }

            StartTransition();

            await Hooks();
        }
    }

    protected async Task Hooks()
    {
        // hooks
        // TODO: but it hasn't been tested yet

        switch (CurrentState)
        {
            case TransitionState.None:
                break;
            case TransitionState.Enter:
                if (!FirstRender)
                {
                    await Transition!.Enter(this);
                }

                break;
            case TransitionState.EnterTo:
                if (!FirstRender)
                {
                    await Transition!.AfterEnter(this);
                }

                break;
            case TransitionState.EnterCancelled:
                if (!FirstRender)
                {
                    await Transition!.EnterCancelled(this);
                }

                break;
            case TransitionState.Leave:
                if (!FirstRender)
                {
                    await Transition!.Leave(this);
                }

                break;
            case TransitionState.LeaveTo:
                if (!FirstRender)
                {
                    await Transition!.AfterLeave(this);
                }

                break;
            case TransitionState.LeaveCancelled:
                if (!FirstRender)
                {
                    await Transition!.LeaveCancelled(this);
                }

                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    protected bool RequestingAnimationFrame;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            FirstRender = false;
        }

        if (!HavingTransition)
        {
            return;
        }

        if (!TransitionJSModule.Initialized)
        {
            if (Reference.Context is null)
            {
                return;
            }

            Transition!.ElementReference = Reference;

            _transitionJSObjectReference = await TransitionJSModule.InitAsync(Reference, _transitionJsInterop!);
        }

        if (!firstRender && ElementReferenceChanged)
        {
            ElementReferenceChanged = false;

            Transition!.ElementReference = Reference;

            _transitionJSObjectReference?.TryInvokeVoidAsync("reset", Reference);
        }

        if (TransitionJSModule.Initialized && CanMoveNext)
        {
            await NextAsync(CurrentState);
        }
    }

    protected virtual bool CanMoveNext => !RequestingAnimationFrame;

    protected bool IsLeaveTransitionState => CurrentState is TransitionState.Leave or TransitionState.LeaveTo;

    protected override string? ComputedStyle
    {
        get
        {
            if (Transition?.LeaveAbsolute is true && IsLeaveTransitionState && ElementInfo is not null)
            {
                var stringBuilder = new StringBuilder();
                stringBuilder.Append(Style);
                stringBuilder.Append(' ');
                stringBuilder.Append("position:absolute; ");
                stringBuilder.Append($"top:{ElementInfo.OffsetTop}px; ");
                stringBuilder.Append($"left:{ElementInfo.OffsetLeft}px; ");
                stringBuilder.Append($"width:{ElementInfo.OffsetWidth}px; ");
                stringBuilder.Append($"height:{ElementInfo.OffsetHeight}px; ");

                return stringBuilder.ToString().TrimEnd();
            }

            return base.ComputedStyle;
        }
    }

    protected abstract void StartTransition();

    /// <summary>
    /// Update to the next transition state.
    /// </summary>
    /// <param name="currentState"></param>
    /// <returns></returns>
    protected abstract Task NextAsync(TransitionState currentState);

    public virtual Task OnTransitionEnd(string referenceId, LeaveEnter transition) => Task.CompletedTask;

    public virtual Task OnTransitionCancel(string referenceId, LeaveEnter transition) => Task.CompletedTask;

    protected async Task RequestAnimationFrameAsync(Func<Task> callback)
    {
        RequestingAnimationFrame = true;
        await Task.Delay(16);
        RequestingAnimationFrame = false;
        await callback();
    }

    public async ValueTask DisposeAsync()
    {
        _transitionJSObjectReference?.TryInvokeVoidAsync("dispose");
    }
}