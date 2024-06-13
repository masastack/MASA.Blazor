﻿using Masa.Blazor.Components.ItemGroup;
using Element = Masa.Blazor.JSInterop.Element;

namespace Masa.Blazor;

public partial class MWindowItem : MGroupItem<MItemGroupBase>
{
    public MWindowItem() : base(GroupType.Window, bootable: true)
    {
    }

    [CascadingParameter] public MWindow? WindowGroup { get; set; }

    [Parameter] public string? Transition { get; set; }

    [Parameter] public string? ReverseTransition { get; set; }

    /// <summary>
    /// just to refresh the component.
    /// </summary>
    [CascadingParameter(Name = "WindowValue")]
    public string? WindowValue { get; set; }

    [Parameter] public bool Eager { get; set; }

    /// <summary>
    /// Internal use
    /// </summary>
    public virtual string Tag { get; set; }

    protected override bool IsEager => Eager;

    private bool InTransition { get; set; }

    protected override bool HasTransition => Transition != "" && ReverseTransition != "";

    protected string? ComputedTransition
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

    protected async Task HandleOnBefore(ElementReference el)
    {
        if (InTransition) return;

        // Initialize transition state here.
        InTransition = true;

        if (WindowGroup == null) return;

        if (WindowGroup.TransitionCount == 0)
        {
            // set initial height for height transition.
            var elementInfo = await Js.InvokeAsync<Element>(JsInteropConstants.GetDomInfo, el);
            var height = elementInfo.ClientHeight;
            if (height != 0)
            {
                WindowGroup.TransitionHeight = height;
            }
        }

        WindowGroup.TransitionCount++;

        WindowGroup.RenderState();
    }

    protected Task HandleOnAfter(ElementReference el)
    {
        if (!InTransition) return Task.CompletedTask;

        InTransition = false;

        if (WindowGroup == null) return Task.CompletedTask;

        if (WindowGroup.TransitionCount > 0)
        {
            WindowGroup.TransitionCount--;

            // Remove container height if we are out of transition.
            if (WindowGroup.TransitionCount == 0)
            {
                WindowGroup.TransitionHeight = null;
            }
        }

        WindowGroup.RenderState();

        return Task.CompletedTask;
    }

    protected Task HandleOnEnter(ElementReference el)
    {
        if (!InTransition)
        {
            return Task.CompletedTask;
        }

        NextTick(async () =>
        {
            if (!string.IsNullOrEmpty(ComputedTransition) || !InTransition) return;

            if (WindowGroup == null) return;

            // Set transition target height.
            var elementInfo = await Js.InvokeAsync<Element>(JsInteropConstants.GetDomInfo, el);
            var height = elementInfo.ClientHeight;
            if (height != 0)
            {
                WindowGroup.TransitionHeight = height;
            }

            WindowGroup.RenderState();
        });

        return Task.CompletedTask;
    }

    private static Block _block = new("m-window-item");
    private ModifierBuilder _modifierBuilder = _block.CreateModifierBuilder();

    protected override IEnumerable<string> BuildComponentClass()
    {
        yield return _modifierBuilder.Add("active", InternalIsActive).Build();
    }
}