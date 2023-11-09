using BlazorComponent.Attributes;

namespace Masa.Docs.Core.Components;

[JSCustomElement]
public class AppHeading : ComponentBase
{
    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    private IJSRuntime JsRuntime { get; set; } = null!;

    [Parameter]
    public int Level { get; set; } = 1;

    [Parameter]
    public string? Href { get; set; }

    [Parameter, EditorRequired]
    public string Content { get; set; } = null!;

    [Parameter]
    public string? ReleasedOn { get; set; }

    private static Dictionary<int, string> s_map = new()
    {
        { 1, "text-h3 text-sm-h3 mb-4 mt-4" },
        { 2, "text-h4 text-sm-h4 mb-3 mt-3" },
        { 3, "text-h5 mb-2" },
        { 4, "text-h6 mb-2" },
        { 5, "text-subtitle-1 font-weight-medium mb-2" },
    };

    private bool IsLink => !string.IsNullOrWhiteSpace(Href);

    private bool IsHash => IsLink && Href!.StartsWith("#");

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        await base.SetParametersAsync(parameters);

        ArgumentNullException.ThrowIfNull(Content);
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, $"h{Level}");
        builder.AddAttribute(1, "class", $"m-heading {s_map[Level]}");

        if (IsLink)
        {
            builder.AddContent(2, (childBuilder) =>
            {
                childBuilder.OpenElement(0, "a");
                childBuilder.AddAttribute(1, "href", Href);
                childBuilder.AddAttribute(2, "class", "text-decoration-none text-right text-md-left");
                childBuilder.AddEventPreventDefaultAttribute(3, "onclick", true);
                childBuilder.AddAttribute(4, "onclick", EventCallback.Factory.Create<MouseEventArgs>(this, () => OnClick(Href!)));
                childBuilder.AddContent(5, "#");
                childBuilder.CloseElement();
            });
        }

        builder.AddContent(3, Content);

        if (!string.IsNullOrWhiteSpace(ReleasedOn))
        {
            builder.AddContent(4, childBuilder =>
            {
                childBuilder.OpenComponent<MChip>(0);
                childBuilder.AddAttribute(1, nameof(MChip.Outlined), true);
                childBuilder.AddAttribute(2, nameof(MChip.Color), AppService.ColorForUpdateState);
                childBuilder.AddAttribute(3, nameof(MChip.Small), true);
                childBuilder.AddAttribute(4, nameof(MChip.Class), "ml-2");
                childBuilder.AddChildContent(5, ReleasedOn);
                childBuilder.CloseComponent();
            });
        }
        
        builder.CloseElement();
    }

    private async Task OnClick(string href)
    {
        if (IsHash)
        {
            await JsRuntime.InvokeVoidAsync("scrollToElement", href, AppService.AppBarHeight + 12);
        }
    }
}
