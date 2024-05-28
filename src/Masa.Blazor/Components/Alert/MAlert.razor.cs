﻿using StyleBuilder = Masa.Blazor.Core.StyleBuilder;

namespace Masa.Blazor;

public partial class MAlert : MasaComponentBase, IThemeable
{
    [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

    [Parameter] public string? Transition { get; set; }

    [Parameter] public Borders Border { get; set; }

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter]
    [MasaApiParameter("$cancel")]
    public string CloseIcon { get; set; } = "$cancel";

    [Parameter]
    [MasaApiParameter("Close")]
    public virtual string CloseLabel { get; set; } = "Close";

    [Parameter] public string? Color { get; set; }

    [Parameter] public virtual bool Dismissible { get; set; }

    [Parameter] [MasaApiParameter("div")] public string Tag { get; set; } = "div";

    [Parameter] public string? Title { get; set; }

    [Parameter] public RenderFragment? TitleContent { get; set; }

    [Parameter] public AlertTypes Type { get; set; }

    [Parameter] public bool Value { get; set; } = true;

    [Parameter] public EventCallback<bool> ValueChanged { get; set; }

    [Parameter] public StringBoolean? Icon { get; set; }

    [Parameter] public bool ColoredBorder { get; set; }

    [Parameter] public bool Dense { get; set; }

    [Parameter] public StringNumber? Elevation { get; set; }

    [Parameter] public bool Outlined { get; set; }

    [Parameter] public bool Prominent { get; set; }

    [Parameter] public StringBoolean? Rounded { get; set; }

    [Parameter] public bool Shaped { get; set; }

    [Parameter] public bool Text { get; set; }

    [Parameter] public bool Tile { get; set; }

    [Parameter] public StringNumber? Height { get; set; }

    [Parameter] public StringNumber? MaxHeight { get; set; }

    [Parameter] public StringNumber? MaxWidth { get; set; }

    [Parameter] public StringNumber? MinHeight { get; set; }

    [Parameter] public StringNumber? MinWidth { get; set; }

    [Parameter] public StringNumber? Width { get; set; }

    [Parameter] public bool Dark { get; set; }

    [Parameter] public bool Light { get; set; }

    [CascadingParameter(Name = "IsDark")] public bool CascadingIsDark { get; set; }

    public bool IsDark
    {
        get
        {
            if (Dark)
            {
                return true;
            }

            if (Light)
            {
                return false;
            }

            return CascadingIsDark;
        }
    }

    private string ComputedType => Type != AlertTypes.None ? Type.ToString().ToLower() : "";

    private string ComputedColor => Color ?? ComputedType;

    private (bool, RenderFragment?) ComputedIcon()
    {
        if (Icon != null && Icon.IsT1 && Icon.AsT1 == false) return (false, null);

        if (Icon != null && Icon.IsT0 && Icon.AsT0 != null)
            return (true, builder => builder.AddContent(0, Icon.AsT0));

        var iconText = Type switch
        {
            AlertTypes.Success => "$success",
            AlertTypes.Error => "$error",
            AlertTypes.Info => "$info",
            AlertTypes.Warning => "$warning",
            _ => null
        };

        if (iconText == null) return (false, null);

        return (true, builder => builder.AddContent(0, iconText));
    }

    private bool HasText => Text || Outlined;

    private bool HasColoredBorder => Border != Borders.None && ColoredBorder;

    private bool HasColoredIcon => HasText || HasColoredBorder;

    private bool HasTypedBorder => ColoredBorder && Type != AlertTypes.None;

    private string IconColor => HasColoredIcon ? ComputedColor : "";

    private bool LogicalDark => Type != AlertTypes.None && !ColoredBorder && !Outlined;

    private bool IsDarkTheme => LogicalDark || IsDark;

    private bool IndependentTheme =>
        (IsDirtyParameter(nameof(Dark)) && Dark) || (IsDirtyParameter(nameof(Light)) && Light);

    private bool HasTitle => !string.IsNullOrWhiteSpace(Title) || TitleContent is not null;

    private RenderFragment? IconContent { get; set; }

    private bool IsShowIcon { get; set; }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

#if NET8_0_OR_GREATER
        if (MasaBlazor.IsSsr && !IndependentTheme)
        {
            CascadingIsDark = MasaBlazor.Theme.Dark;
        }
#endif

        (IsShowIcon, IconContent) = ComputedIcon();
    }

    private static Block _block = new("m-alert");
    private ModifierBuilder _modifierBuilder = _block.CreateModifierBuilder();
    private ModifierBuilder _borderModifierBuilder = _block.Element("border").CreateModifierBuilder();

    protected override IEnumerable<string> BuildComponentClass()
    {
        yield return "m-sheet";

        if (Shaped)
        {
            yield return "m-sheet--shaped";
        }

        yield return _modifierBuilder
            .Add("border", Border != Borders.None)
            .Add(Border, Borders.None, "")
            .AddOneOf(Prominent, Dense)
            .Add(Text)
            .Add(Outlined)
            .AddColor(ComputedColor, HasText, !ColoredBorder)
            .AddElevation(Elevation)
            .AddRounded(Rounded, Tile)
            .AddTheme(IsDarkTheme, IndependentTheme)
            .Build();
    }

    protected override IEnumerable<string> BuildComponentStyle()
    {
        return StyleBuilder.Create()
            .AddColor(ComputedColor, HasText, () => !ColoredBorder)
            .AddHeight(Height)
            .AddMaxHeight(MaxHeight)
            .AddMinHeight(MinHeight)
            .AddWidth(Width)
            .AddMaxWidth(MaxWidth)
            .AddMinWidth(MinWidth)
            .AddIf("display", "none", Transition == null && !Value)
            .AddIf("--m-theme-alert-color", $"var(--m-theme-on-{ComputedType})", Type != AlertTypes.None)
            .GenerateCssStyles();
    }

    private async Task HandleOnDismiss(MouseEventArgs args)
    {
        Value = false;
        await ValueChanged.InvokeAsync(false);
    }

    [MasaApiPublicMethod]
    public async Task ToggleAsync()
    {
        Value = !Value;
        await ValueChanged.InvokeAsync(Value);
    }
}