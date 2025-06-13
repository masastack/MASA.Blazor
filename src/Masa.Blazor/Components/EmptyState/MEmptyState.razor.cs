namespace Masa.Blazor;

public partial class MEmptyState : ThemeComponentBase
{
    [Parameter] public string? ActionText { get; set; }
    [Parameter] public string? BackgroundColor { get; set; }
    [Parameter] public string? Color { get; set; }
    [Parameter] public string? Headline { get; set; }
    [Parameter] public StringNumber? Height { get; set; }
    [Parameter] public string? Href { get; set; }
    [Parameter] public string? Icon { get; set; }
    [Parameter] public string? Image { get; set; }

    [Parameter]
    [MasaApiParameter(EmptyStateJustify.Center)]
    public EmptyStateJustify Justify { get; set; } = EmptyStateJustify.Center;

    [Parameter] public StringNumber? MaxHeight { get; set; }
    [Parameter] public StringNumber? MaxWidth { get; set; }
    [Parameter] public StringNumber? MinHeight { get; set; }
    [Parameter] public StringNumber? MinWidth { get; set; }
    [Parameter] public StringNumber? Size { get; set; }
    [Parameter] public string? Text { get; set; }
    [Parameter] public StringNumber? TextWidth { get; set; }
    [Parameter] public string? Title { get; set; }
    [Parameter] public StringNumber? Width { get; set; }
    [Parameter] public EventCallback<MouseEventArgs> OnActionClick { get; set; }
    [Parameter] public RenderFragment<EmptyStateActionContext>? ActionsContent { get; set; }
    [Parameter] public RenderFragment? HeadlineContent { get; set; }
    [Parameter] public RenderFragment? MediaContent { get; set; }
    [Parameter] public RenderFragment? TextContent { get; set; }
    [Parameter] public RenderFragment? TitleContent { get; set; }
    [Parameter] public RenderFragment? ChildContent { get; set; }

    private static readonly Block _block = new Block("m-empty-state");
    private ModifierBuilder _modifierBuilder = _block.CreateModifierBuilder();
    private EmptyStateActionContext _actionContext = null!;

    private bool HasActions => ActionsContent is not null || !string.IsNullOrWhiteSpace(ActionText);
    private StringNumber ComputedSize => Size ?? (string.IsNullOrEmpty(Image) ? 96 : 200);

    protected override void OnInitialized()
    {
        base.OnInitialized();

        _actionContext = new EmptyStateActionContext(HandleOnActionClick);
    }

    protected override IEnumerable<string?> BuildComponentClass()
    {
        yield return _modifierBuilder
            .Add(Justify.ToString().ToLowerInvariant())
            .AddTheme(ComputedTheme)
            .AddBackgroundColor(BackgroundColor)
            .Build();
    }

    protected override IEnumerable<string?> BuildComponentStyle()
    {
        yield return StyleBuilder.Create()
            .AddBackgroundColor(BackgroundColor)
            .AddHeight(Height)
            .AddMaxHeight(MaxHeight)
            .AddMaxWidth(MaxWidth)
            .AddMinHeight(MinHeight)
            .AddMinWidth(MinWidth)
            .AddWidth(Width)
            .Build();
    }

    private async Task HandleOnActionClick(MouseEventArgs args)
    {
        await OnActionClick.InvokeAsync(args);
    }
}

public enum EmptyStateJustify
{
    End,
    Start,
    Center,
}

public record EmptyStateActionContext(Func<MouseEventArgs, Task> OnClick);