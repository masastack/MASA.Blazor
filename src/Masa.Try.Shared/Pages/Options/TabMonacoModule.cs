using Masa.Blazor;

namespace Masa.Try.Shared.Pages.Options;

public class TabMonacoModule
{
    public MMonacoEditor MonacoEditor { get; set; }

    public string Name { get; set; }

    public object Options { get; set; }
}
