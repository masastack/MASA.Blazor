using BlazorComponent.JSInterop;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace Masa.Docs.Shared.Components;

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

    private static Dictionary<int, string> _map = new()
    {
        { 1, "text-h3 text-sm-h3 mb-4 mt-4" },
        { 2, "text-h4 text-sm-h4 mb-3 mt-3" },
        { 3, "text-h5 mb-2" },
        { 4, "text-h6 mb-2" },
        { 5, "text-subtitle-1 font-weight-medium mb-2" },
    };

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        await base.SetParametersAsync(parameters);

        ArgumentNullException.ThrowIfNull(Content);
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, $"h{Level}");
        builder.AddAttribute(1, "class", $"m-heading {_map[Level]}");
        builder.AddContent(2, (childBuilder) =>
        {
            childBuilder.OpenElement(0, "a");
            childBuilder.AddAttribute(1, "href", Href);
            childBuilder.AddAttribute(2, "class", "text-decoration-none text-right text-md-left");
            childBuilder.AddEventPreventDefaultAttribute(3, "onclick", true);
            childBuilder.AddAttribute(4, "onclick", EventCallback.Factory.Create<MouseEventArgs>(this, () => OnClick(Href)));
            childBuilder.AddContent(5, "#");
            childBuilder.CloseElement();
        });
        builder.AddContent(3, Content);
        builder.CloseElement();
    }

    private async Task OnClick(string href)
    {
        if (href.StartsWith("#"))
        {
            // TODO: remove the following lines when #40190 of aspnetcore resolved.
            // TODO: Blazor now does not support automatic scrolling of anchor points.
            // Check this when .NET 8 released.

            NavigationManager.ReplaceWithHash(href);
            await JsRuntime.ScrollToElement(href, AppService.AppBarHeight);
        }
    }
}
