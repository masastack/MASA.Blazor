using System.Diagnostics.CodeAnalysis;
using Masa.Blazor.Components.Bootable;

namespace Masa.Blazor.Mixins.Menuable;

public abstract class MMenuable : MBootable
{
    [Inject] protected MasaBlazor MasaBlazor { get; set; } = null!;

    [Parameter] public bool Absolute { get; set; }

    [Parameter] public bool AllowOverflow { get; set; }

    [Parameter] public string? ContentClass { get; set; }

    [Parameter] public StringNumber? MaxWidth { get; set; }

    [Parameter] public StringNumber? MinWidth { get; set; }

    [Parameter] public StringNumber? NudgeBottom { get; set; }

    [Parameter] public StringNumber? NudgeLeft { get; set; }

    [Parameter] public StringNumber? NudgeRight { get; set; }

    [Parameter] public StringNumber? NudgeTop { get; set; }

    [Parameter] public StringNumber? NudgeWidth { get; set; }

    [Parameter] public bool OffsetOverflow { get; set; }

    [Parameter] public double? PositionX { get; set; }

    [Parameter] public double? PositionY { get; set; }

    [Parameter] public StringNumber? ZIndex { get; set; }

    [Parameter] [MasaApiParameter(false)] public StringBoolean? Attach { get; set; } = false;

    [Parameter] public bool Left { get; set; }

    [Parameter] public bool Right { get; set; }

    [Parameter] public bool Top { get; set; }

    [Parameter] public bool Bottom { get; set; }

    [Parameter] public bool OffsetX { get; set; }

    [Parameter] public bool OffsetY { get; set; }

    /// <summary>
    /// The lazy content would be created in a [data-permanent] element.
    /// It's useful when you use this component in a layout.
    /// </summary>
    [Parameter]
    public bool Permanent { get; set; }

    [Parameter] public bool ExternalActivator { get; set; }

    [Inject] [NotNull] public Window? Window { get; set; }

    [Inject] [NotNull] public Document? Document { get; set; }

    protected virtual bool IsRtl => false;

    protected double ComputedLeft
    {
        get
        {
            var activator = Dimensions.Activator;
            var content = Dimensions.Content;
            var activatorLeft = !IsDefaultAttach ? activator.OffsetLeft : activator.Left;
            var minWidth = Math.Max(activator.Width, content?.Width ?? 0);

            double left = 0;

            left += activatorLeft;
            if (Left || (IsRtl && !Right) || IsRtl)
            {
                left -= (minWidth - activator.Width);
            }

            if (OffsetX)
            {
                double maxWidth;

                if (MaxWidth != null)
                {
                    (var isNumber, maxWidth) = MaxWidth.TryGetNumber();
                    maxWidth = isNumber ? Math.Min(activator.Width, maxWidth) : activator.Width;
                }
                else
                {
                    maxWidth = activator.Width;
                }

                left += Left ? -maxWidth : activator.Width;
            }

            if (NudgeLeft != null)
            {
                var (_, nudgeLeft) = NudgeLeft.TryGetNumber();
                left -= nudgeLeft;
            }

            if (NudgeRight != null)
            {
                var (_, nudgeRight) = NudgeRight.TryGetNumber();
                left += nudgeRight;
            }

            return left;
        }
    }

    protected double ComputedTop
    {
        get
        {
            var activator = Dimensions.Activator;
            var content = Dimensions.Content;

            double top = 0;

            if (activator is null || content is null)
            {
                return top;
            }

            if (Top) top += activator.Height - content.Height;

            if (!IsDefaultAttach)
            {
                top += activator.OffsetTop;
            }
            else
            {
                top += activator.Top + PageYOffset;
            }

            if (OffsetY) top += Top ? -activator.Height : activator.Height;

            if (NudgeTop != null)
            {
                var (isNumber, nudgeTop) = NudgeTop.TryGetNumber();
                if (isNumber)
                {
                    top -= nudgeTop;
                }
            }

            if (NudgeBottom != null)
            {
                var (isNumber, nudgeBottom) = NudgeBottom.TryGetNumber();
                if (isNumber)
                {
                    top += nudgeBottom;
                }
            }

            return top;
        }
    }

    protected MenuablePosition AbsolutePosition => new()
    {
        OffsetTop = PositionY ?? AbsoluteY,
        OffsetLeft = PositionX ?? AbsoluteX,
        ScrollHeight = 0,
        Top = PositionY ?? AbsoluteY,
        Bottom = PositionY ?? AbsoluteY,
        Left = PositionX ?? AbsoluteX,
        Right = PositionX ?? AbsoluteX,
        Height = 0,
        Width = 0
    };

    protected double AbsoluteYOffset => PageYOffset - RelativeYOffset;

    protected bool HasActivator => ActivatorContent != null || ExternalActivator;

    protected virtual string? DefaultAttachSelector => default;

    protected string? AttachSelector
    {
        get
        {
            if (IsDefaultAttach)
            {
                return DefaultAttachSelector;
            }

            if (IsAttachSelf)
            {
                return Ref.GetSelector();
            }

            return Attach?.AsT0;
        }
    }

    /// <summary>
    /// Attached to the current element but not to other element.
    /// </summary>
    protected bool IsAttachSelf => Attach is not null && Attach.IsT1 && Attach.AsT1;

    /// <summary>
    /// Determines whether the <see cref="Attach"/> value is false.
    /// </summary>
    protected bool IsDefaultAttach => Attach is null || (Attach is not null && Attach.IsT1 && Attach.AsT1 == false);

    protected int ComputedZIndex => ZIndex != null ? ZIndex.ToInt32() : Math.Max(ActivateZIndex, StackMinZIndex);

    public MenuableDimensions Dimensions { get; } = new();

    protected double AbsoluteX { get; set; }

    protected double AbsoluteY { get; set; }

    protected double PageYOffset { get; set; }

    protected double PageWidth { get; set; }

    protected double RelativeYOffset { get; set; }

    protected bool ActivatorFixed { get; set; }

    public ElementReference ContentElement { get; protected set; }

    protected int ActivateZIndex { get; set; }

    protected int StackMinZIndex { get; set; } = 6;

    public bool Attached { get; protected set; }

    protected StringNumber? CalcLeft(double menuWidth)
        => !IsDefaultAttach ? ComputedLeft : CalcXOverflow(ComputedLeft, menuWidth);

    protected StringNumber? CalcTop()
        => !IsDefaultAttach ? ComputedTop : CalcYOverflow(ComputedTop);

    protected double CalcXOverflow(double left, double menuWidth)
    {
        var xOverflow = left + menuWidth - PageWidth + 12;

        if ((!Left || Right) && xOverflow > 0)
        {
            left = Math.Max(left - xOverflow, 0);
        }
        else
        {
            left = Math.Max(left, 12);
        }

        return left + GetOffsetLeft();
    }

    private double GetOffsetLeft()
    {
        return Window.PageXOffset > 0 ? Window.PageXOffset : Document.DocumentElement.ScrollLeft;
    }

    protected double CalcYOverflow(double top)
    {
        var documentHeight = GetInnerHeight();
        var toTop = PageYOffset + documentHeight;
        var activator = Dimensions.Activator;
        var contentHeight = Dimensions.Content?.Height ?? 0;
        var totalHeight = top + contentHeight;
        var isOverflowing = toTop < totalHeight;

        if (isOverflowing && OffsetOverflow && activator.Top > contentHeight)
        {
            top = PageYOffset + (activator.Top - contentHeight);
        }
        else if (isOverflowing && !AllowOverflow)
        {
            top = toTop - contentHeight - 12;
        }
        else if (top < AbsoluteYOffset && !AllowOverflow)
        {
            top = AbsoluteYOffset + 12;
        }

        return top < 12 ? 12 : top;
    }

    private double GetInnerHeight()
    {
        return Window.InnerHeight > 0 ? Window.InnerHeight : Document.DocumentElement.ClientHeight;
    }

    private void CheckForPageYOffset()
    {
        PageYOffset = ActivatorFixed ? 0 : GetOffsetTop();
    }

    private double GetOffsetTop()
    {
        return Window.PageYOffset > 0 ? Window.PageYOffset : Document.DocumentElement.ScrollTop;
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        MasaBlazor.BreakpointChanged += MasaBlazorOnBreakpointChanged;
    }

    private async void MasaBlazorOnBreakpointChanged(object? sender, BreakpointChangedEventArgs e)
    {
        if (!IsActive)
        {
            return;
        }

        await InvokeAsync(async () =>
        {
            await UpdateDimensionsAsync();
            StateHasChanged();
        });
    }

    protected virtual async Task UpdateDimensionsAsync()
    {
        if (!Attached && Attach is not null &&
            ((Attach.IsT0 && string.IsNullOrWhiteSpace(Attach.AsT0)) || Attach.IsT1 && Attach.AsT1))
        {
            Attached = true;
        }

        //Invoke multiple method
        //1、Attach
        //2、Window,Document
        //3、Dimensions
        //4、z-index
        var windowProps = new string[] { "innerHeight", "innerWidth", "pageXOffset", "pageYOffset" };
        var documentProps = new string[] { "clientHeight", "clientWidth", "scrollLeft", "scrollTop" };

        var hasActivator = HasActivator && !Absolute;
        var multipleResult = await Js.InvokeAsync<MultipleResult>(JsInteropConstants.InvokeMultipleMethod, windowProps,
            documentProps,
            hasActivator, ActivatorSelector, IsDefaultAttach, ContentElement, Attached, AttachSelector, Ref);
        var windowAndDocument = multipleResult.WindowAndDocument;

        //We want to reduce js interop
        //And we attach content-element in last step
        if (!Attached)
        {
            Attached = true;
        }

        ActivateZIndex = multipleResult.ZIndex;

        //Window props
        Window.InnerHeight = windowAndDocument.InnerHeight;
        Window.InnerWidth = windowAndDocument.InnerWidth;
        Window.PageXOffset = windowAndDocument.PageXOffset;
        Window.PageYOffset = windowAndDocument.PageYOffset;

        //Document props
        Document.DocumentElement.ClientHeight = windowAndDocument.ClientHeight;
        Document.DocumentElement.ClientWidth = windowAndDocument.ClientWidth;
        Document.DocumentElement.ScrollLeft = windowAndDocument.ScrollLeft;
        Document.DocumentElement.ScrollTop = windowAndDocument.ScrollTop;

        //TODO:CheckActivatorFixed
        CheckForPageYOffset();
        PageWidth = Document.DocumentElement.ClientWidth;

        var dimensions = multipleResult.Dimensions;
        if (hasActivator)
        {
            Dimensions.Activator = dimensions.Activator;
        }
        else
        {
            Dimensions.Activator = AbsolutePosition;

            //Since no activator,we should re-computed it's top and left
            Dimensions.Activator.Top -= dimensions.RelativeYOffset;
            Dimensions.Activator.Left -= Window.PageXOffset + dimensions.OffsetParentLeft;
        }

        Dimensions.Content = dimensions.Content;
        RelativeYOffset = dimensions.RelativeYOffset;
    }

    public override async Task HandleOnClickAsync(MouseEventArgs args)
    {
        AbsoluteX = args.ClientX;
        AbsoluteY = args.ClientY;

        if (OpenOnClick)
        {
            await base.HandleOnClickAsync(args);
        }
    }

    protected override async Task WhenIsActiveUpdating(bool value)
    {
        if (value)
        {
            await CallActivateAsync();
        }
        else
        {
            await CallDeactivateAsync();
        }

        await base.WhenIsActiveUpdating(value);
    }

    private async Task CallActivateAsync()
    {
        await ActivateAsync();
    }

    protected virtual async Task ActivateAsync()
    {
        await UpdateDimensionsAsync();

        //Wait for left and top update
        //Otherwise,we may get a flash
        StateHasChanged();
        await Task.Delay(16);
    }

    private Task CallDeactivateAsync()
    {
        //TODO:isContentActive
        return DeactivateAsync();
    }

    protected virtual Task DeactivateAsync()
    {
        return Task.CompletedTask;
    }

    protected override async ValueTask DisposeAsyncCore()
    {
        MasaBlazor.BreakpointChanged -= MasaBlazorOnBreakpointChanged;

        if (ContentElement.Context is not null)
        {
            _ = Js.InvokeVoidAsync(JsInteropConstants.DelElementFrom, ContentElement, AttachSelector);
        }

        await base.DisposeAsyncCore();
    }
}