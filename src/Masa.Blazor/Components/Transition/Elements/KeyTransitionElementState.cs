using Masa.Blazor.Components.Transition;

namespace Masa.Blazor;

#if NET6_0
public class KeyTransitionElementState<TValue>
#else
public class KeyTransitionElementState<TValue> where TValue : notnull
#endif
{
    private TValue? _key;

    public KeyTransitionElementState(KeyTransitionElement<TValue> element)
    {
        Element = element;
    }

    protected object Value => Element.Value;

    protected Transition? Transition => Element.Transition;

    protected string? Class => Element.Class;

    protected string? Style => Element.Style;

    protected bool IsLeaveTransitionState => TransitionState is TransitionState.Leave or TransitionState.LeaveTo;

    protected bool IsEnterTransitionState => TransitionState is TransitionState.Enter or TransitionState.EnterTo;

    protected KeyTransitionElement<TValue> Element { get; }

    /// <summary>
    /// Save transition state for element
    /// </summary>
    public TransitionState TransitionState { get; set; }

    public TValue? Key
    {
        get => _key;
        set
        {
            _key = value;
            IsEmpty = false;
        }
    }

    public bool IsEmpty { get; set; } = true;

    public string? ComputedClass
    {
        get
        {
            var transitionName = Transition?.Name;
            if (transitionName == null || TransitionState == TransitionState.None)
            {
                return Class;
            }

            var transitionClass = TransitionState switch
            {
                TransitionState.Enter   => $"{transitionName}-enter {transitionName}-enter-active",
                TransitionState.EnterTo => $"{transitionName}-enter-active {transitionName}-enter-to",
                TransitionState.Leave   => $"{transitionName}-leave {transitionName}-leave-active",
                TransitionState.LeaveTo => $"{transitionName}-leave-active {transitionName}-leave-to",
                _                       => string.Empty
            };

            return string.Join(" ", Class, transitionClass);
        }
    }

    public string ComputedStyle
    {
        get
        {
            var styles = new List<string>();

            if (Style != null)
            {
                styles.Add(Style);
            }

            if (IsLeaveTransitionState && Transition?.LeaveAbsolute is true && Element.ElementInfo is not null)
            {
                styles.Add("position:absolute");
                styles.Add($"top:{Element.ElementInfo.OffsetTop}px");
                styles.Add($"left:{Element.ElementInfo.OffsetLeft}px");
                styles.Add($"width:{Element.ElementInfo.OffsetWidth}px");
                styles.Add($"height:{Element.ElementInfo.OffsetHeight}px");
            }

            return string.Join(';', styles);
        }
    }

    public void CopyTo(KeyTransitionElementState<TValue> state)
    {
        state.Key = Key;
        state.TransitionState = TransitionState;
    }

    public void Reset()
    {
        IsEmpty = true;
    }
}
