using Masa.Blazor.Presets.PageStack;

namespace Masa.Blazor.Presets;

public class PPageStackItemInit : IComponent
{
    [CascadingParameter] private PPageStackItem PageStackItem { get; set; } = null!;

    [Parameter] public string? AppBarClass { get; set; }

    [Parameter] public string? AppBarStyle { get; set; }

    [Parameter] public string? AppBarColor { get; set; }

    [Parameter] public string? ContentClass { get; set; }

    [Parameter] public string? ContentStyle { get; set; }

    [Parameter] public RenderFragment<PageStackGoBackContext>? AppBarContent { get; set; }

    [Parameter] public string? AppBarTitle { get; set; }

    private bool _init;

    public void Attach(RenderHandle renderHandle)
    {
    }

    public Task SetParametersAsync(ParameterView parameters)
    {
        parameters.SetParameterProperties(this);

        if (_init)
        {
            return Task.CompletedTask;
        }

        _init = true;

        PageStackItem.AppBarContent = AppBarContent;
        PageStackItem.AppBarColor = AppBarColor;
        PageStackItem.AppBarClass = AppBarClass;
        PageStackItem.AppBarStyle = AppBarStyle;
        PageStackItem.AppBarTitle = AppBarTitle;
        PageStackItem.ContentClass = ContentClass;
        PageStackItem.ContentStyle = ContentStyle;

        return Task.CompletedTask;
    }
}