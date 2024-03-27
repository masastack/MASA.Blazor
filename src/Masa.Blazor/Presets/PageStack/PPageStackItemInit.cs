using Masa.Blazor.Presets.PageStack;

namespace Masa.Blazor.Presets;

public class PPageStackItemInit : IComponent
{
    [CascadingParameter] private PPageStackItem PageStackItem { get; set; } = null!;

    [Parameter] public string? RerenderKey { get; set; }

    [Parameter] public string? BarClass { get; set; }

    [Parameter] public string? BarStyle { get; set; }

    [Parameter] public string? BarColor { get; set; }

    [Parameter] public string? ContentClass { get; set; }

    [Parameter] public string? ContentStyle { get; set; }

    [Parameter] public RenderFragment<PageStackGoBackContext>? BarContent { get; set; }

    [Parameter] public string? Title { get; set; }

    [Parameter] public RenderFragment? ActionContent { get; set; }

    [Parameter] public bool Light { get; set; }

    private bool _init;
    private string? _prevRerenderKey;

    public void Attach(RenderHandle renderHandle)
    {
    }

    public Task SetParametersAsync(ParameterView parameters)
    {
        parameters.SetParameterProperties(this);

        if (_init)
        {
            if (_prevRerenderKey != RerenderKey)
            {
                _prevRerenderKey = RerenderKey;
                Rerender();
            }

            return Task.CompletedTask;
        }

        _init = true;
        _prevRerenderKey = RerenderKey;

        PageStackItem.AppBarContent = BarContent;
        PageStackItem.AppBarColor = BarColor;
        PageStackItem.AppBarClass = BarClass;
        PageStackItem.AppBarStyle = BarStyle;
        PageStackItem.AppBarTitle = Title;
        PageStackItem.Light = Light;

        PageStackItem.ContentClass = ContentClass;
        PageStackItem.ContentStyle = ContentStyle;

        PageStackItem.ActionContent = ActionContent;

        return Task.CompletedTask;
    }

    [MasaApiPublicMethod]
    public void Rerender()
    {
        PageStackItem.InvokeStateHasChanged();
    }
}