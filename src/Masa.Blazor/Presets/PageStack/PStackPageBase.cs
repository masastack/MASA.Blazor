namespace Masa.Blazor.Presets;

public class PStackPageBase : ComponentBase
{
    [CascadingParameter] private PPageStack? PageStack { get; set; }

    protected string? PageSelector { get; private set; }

    protected override void OnInitialized()
    {
        if (PageStack is null)
        {
            return;
        }

        var peek = PageStack.Pages.Last();
        PageSelector = peek.Selector;
    }
}