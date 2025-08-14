using Masa.Blazor.Components.ItemGroup;

namespace Masa.Blazor;

public partial class MListItem : MRoutableGroupItem<MItemGroupBase>, IThemeable
{
    public MListItem() : base(GroupType.ListItemGroup)
    {
    }

    /// <summary>
    /// Lowers max height of list tiles
    /// </summary>
    [Parameter]
    public bool Dense { get; set; }

    /// <summary>
    /// If set, the list tile will not be rendered as a link even if it has to/href prop or @click handler
    /// </summary>
    [Parameter]
    public bool Inactive { get; set; }

    /// <summary>
    /// Allow text selection inside v-list-item. This prop uses user-select
    /// </summary>
    [Parameter]
    public bool Selectable { get; set; }

    /// <summary>
    /// Increases list-item height for two lines. This prop uses line-clamp and is not supported in all browsers.
    /// </summary>
    [Parameter]
    public bool TwoLine { get; set; }

    /// <summary>
    /// Increases list-item height for three lines. This prop uses line-clamp and is not supported in all browsers.
    /// </summary>
    [Parameter]
    public bool ThreeLine { get; set; }

    [Parameter] public bool Highlighted { get; set; }

    [Parameter] public bool Ripple { get; set; }

    [CascadingParameter(Name = "IsInGroup")]
    public bool IsInGroup { get; set; }

    [CascadingParameter(Name = "IsInMenu")]
    public bool IsInMenu { get; set; }

    [CascadingParameter(Name = "IsInList")]
    public bool IsInList { get; set; }

    [CascadingParameter(Name = "IsInNav")] public bool IsInNav { get; set; }

    [CascadingParameter] public MList? List { get; set; }

    [Parameter] public string? Color { get; set; }

    [Parameter] public RenderFragment<ItemContext>? ItemContent { get; set; }

    [Parameter] public bool OnClickStopPropagation { get; set; }

    [Parameter] public bool OnClickPreventDefault { get; set; }

    [Parameter]
    [MasaApiParameter(ReleasedIn = "v1.5.0")]
    public string? Title { get; set; }

    [Parameter]
    [MasaApiParameter(ReleasedIn = "v1.5.0")]
    public string? Subtitle { get; set; }

    [Parameter]
    [MasaApiParameter(ReleasedIn = "v1.5.0")]
    public string? PrependIcon { get; set; }

    [Parameter]
    [MasaApiParameter(ReleasedIn = "v1.5.0")]
    public string? PrependAvatar { get; set; }

    [Parameter]
    [MasaApiParameter(ReleasedIn = "v1.5.0")]
    public string? AppendIcon { get; set; }

    [Parameter]
    [MasaApiParameter(ReleasedIn = "v1.5.0")]
    public string? AppendAvatar { get; set; }

    [Parameter]
    [MasaApiParameter(ReleasedIn = "v1.9.0")]
    public bool Slim { get; set; }

    [Parameter]
    [MasaApiParameter(ReleasedIn = "v1.10.0")]
    public string? ActiveColor { get; set; }

    [Parameter]
    [MasaApiParameter(ReleasedIn = "v1.11.0")]
    public StringNumber? MinHeight { get; set; }

    private bool ComputedRipple => !Disabled && IsClickable && (!IsDirtyParameter(nameof(Ripple)) || Ripple);

    protected bool IsClickable => Router?.IsClickable is true || Matched;

    protected override bool IsRoutable => Href != null && List?.Routable is true;

    protected override bool AfterHandleEventShouldRender() => false;

    protected virtual async Task HandleOnClick(MouseEventArgs args)
    {
        if (args.Detail > 0)
        {
            _ = Js.InvokeVoidAsync(JsInteropConstants.Blur, Ref);
        }

        if (OnClick.HasDelegate)
        {
            await OnClick.InvokeAsync(new MouseEventWithRefArgs(args, Ref));
        }

        if (IsRoutable) return;

        await ToggleAsync();
    }

    private void SetAttrs()
    {
        Attributes["aria-disabled"] = Disabled ? true : null;
        Attributes["tabindex"] = IsClickable ? 0 : -1;

        if (Attributes.ContainsKey("role"))
        {
            // do nothing, role already provided
        }
        else if (IsInNav)
        {
            // do nothing, role is inherit (TODO:check)
        }
        else if (IsInGroup)
        {
            Attributes["role"] = "option";
            Attributes["aria-selected"] = InternalIsActive.ToString();
        }
        else if (IsInMenu)
        {
            Attributes["role"] = IsClickable ? "menuitem" : null;
            Attributes["id"] = Id ?? $"list-item-{Guid.NewGuid():N}"; // TODO:check
        }
        else if (IsInList)
        {
            Attributes["role"] = "listitem";
        }
    }

    private ItemContext GenItemContext()
    {
        return new ItemContext(
            InternalIsActive,
            InternalIsActive ? ComputedActiveClass : "",
            ToggleAsync,
            RefBack,
            Value
        );
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        SetAttrs();

        Attributes["ripple"] = ComputedRipple;
    }

    private static Block _block = new("m-list-item");
    private ModifierBuilder _modifierBuilder = _block.CreateModifierBuilder();

    protected override IEnumerable<string> BuildComponentClass()
    {
        yield return _modifierBuilder.Add(Dense)
            .Add(Slim)
            .Add(Disabled)
            .Add(Selectable)
            .Add(TwoLine)
            .Add(ThreeLine)
            .Add("link", IsClickable && !Inactive)
            .Add(Highlighted)
            .Add("active", InternalIsActive)
            .AddClass(ComputedActiveClass, InternalIsActive)
            .AddTextColor(ActiveColor, InternalIsActive)
            .AddTextColor(Color)
            .AddTheme(ComputedTheme)
            .Build();
    }

    protected override IEnumerable<string?> BuildComponentStyle()
    {
        if (MinHeight is not null)
        {
            yield return CssStyleUtils.GetHeight(MinHeight, "min-height");
        }
    }
}