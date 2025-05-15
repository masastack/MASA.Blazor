namespace Masa.Blazor.Presets;

public class PageStackGoBackContext
{
    public Func<Task> GoBack { get; init; }

    public PageStackGoBackContext(Func<Task> goBack)
    {
        GoBack = goBack;
    }
}