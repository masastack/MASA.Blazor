namespace Masa.Blazor.Presets;

public class PageStackGoBackContext
{
    public string AbsolutePath { get; init; }

    public Func<Task> GoBack { get; init; }

    public PageStackGoBackContext(string absolutePath, Func<Task> goBack)
    {
        AbsolutePath = absolutePath;
        GoBack = goBack;
    }
}