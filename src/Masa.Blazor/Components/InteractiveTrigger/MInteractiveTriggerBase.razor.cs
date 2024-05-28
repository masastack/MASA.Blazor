﻿namespace Masa.Blazor;

using BemIt;

/// <summary>
/// An abstract class of interactive trigger component.
/// </summary>
/// <typeparam name="TValue"></typeparam>
/// <typeparam name="TInteractiveValue"></typeparam>

#if NET8_0_OR_GREATER
[StreamRendering]
#endif

public abstract partial class MInteractiveTriggerBase<TValue, TInteractiveValue>
{
    [Parameter] public bool DisableLinkOnInteractive { get; set; }

    /// <summary>
    /// The name of query parameter.
    /// </summary>
    [Parameter] [EditorRequired] public string QueryName { get; set; } = null!;

    /// <summary>
    /// The value of query parameter.
    /// </summary>
    [Parameter] public TValue? QueryValue { get; set; }

    /// <summary>
    /// A value that is used to determine whether the component is interactive.
    /// </summary>
    [Parameter] [EditorRequired] public virtual TInteractiveValue InteractiveValue { get; set; } = default!;

    /// <summary>
    /// The <see cref="Type"/> of interactive component.
    /// </summary>
    [Parameter] [EditorRequired] public Type InteractiveComponentType { get; set; } = null!;

    /// <summary>
    /// The parameters of interactive component.
    /// </summary>
    [Parameter] public IDictionary<string, object?>? InteractiveComponentParameters { get; set; }

    [Parameter] public RenderFragment<TValue?>? ChildContent { get; set; }

    /// <summary>
    /// Determines whether a built-in popup is needed to display the interactive component.
    /// </summary>
    [Parameter] public bool WithPopup { get; set; }

    /// <summary>
    /// The class of popup, but apply only when interacting.
    /// </summary>
    [Parameter] public string? PopupClass { get; set; }

    /// <summary>
    /// The style of popup, but apply only when interacting.
    /// </summary>
    [Parameter] public string? PopupStyle { get; set; }

    /// <summary>
    /// The top position of popup.
    /// </summary>
    [Parameter] public int? Top { get; set; }

    /// <summary>
    /// The right position of popup.
    /// </summary>
    [Parameter] public int? Right { get; set; }

    /// <summary>
    /// The bottom position of popup.
    /// </summary>
    [Parameter] public int? Bottom { get; set; }

    /// <summary>
    /// The left position of popup.
    /// </summary>
    [Parameter] public int? Left { get; set; }

    private bool _active;

    private string ElementId => $"_int_trigger_{QueryName}";

    protected virtual string ComponentName => nameof(MInteractiveTriggerBase<TValue, TInteractiveValue>);

    protected string? Activator => $"#{ElementId} > a";

    public bool IsInteractive { get; private set; }

    private IDictionary<string, object?> ComputedInteractiveComponentParameters
    {
        get
        {
            InteractiveComponentParameters ??= new Dictionary<string, object?>();

            if (InteractiveComponentType.IsAssignableTo(typeof(MInteractivePopup)))
            {
                InteractiveComponentParameters.TryAdd(nameof(QueryName), QueryName);
                InteractiveComponentParameters.TryAdd(nameof(Activator), Activator);
            }

            return InteractiveComponentParameters;
        }
    }

    protected override async Task OnInitializedAsync()
    {
        QueryName.ThrowIfNull(ComponentName);
        InteractiveValue.ThrowIfNull(ComponentName);
        InteractiveComponentType.ThrowIfNull(ComponentName);

        IsInteractive = CheckInteractive();

        if (IsInteractive)
        {
            // The html generated from the server is rendered on the page
            // before a short delay. With delay and [StreamRendering],
            // set active to true, there will be a short transition animation. 

            await Task.Delay(1);

            _active = true;
        }
    }

    protected abstract bool CheckInteractive();

    private static Block _block = new("m-interactive-trigger");
    private ModifierBuilder _linkModifierBuilder = _block.Element("link").CreateModifierBuilder();
    private ModifierBuilder _popupModifierBuilder = _block.Element("popup").CreateModifierBuilder();

    protected override IEnumerable<string> BuildComponentClass()
    {
        yield return _block.Name;
    }
}
