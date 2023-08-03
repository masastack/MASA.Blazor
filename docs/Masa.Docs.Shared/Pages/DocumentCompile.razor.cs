namespace Masa.Docs.Shared.Pages;

public partial class DocumentCompile
{
    [Inject]
    private AppService AppService { get; set; } = null!;

    [Parameter]
    [SupplyParameterFromQuery]
    public string? Path { get; set; }

    private string Code;

    private void OnTocParsed(List<MarkdownItTocContent>? contents)
    {
        AppService.Toc = contents;
    }

}
