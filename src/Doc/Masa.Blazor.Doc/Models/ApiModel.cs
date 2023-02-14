namespace Masa.Blazor.Doc.Models;

public class ApiModel
{
    public string Title { get; set; }
    public List<string> Components { get; set; }
    public List<ApiColumn> Props { get; set; }
    public List<ApiColumn> Contents { get; set; }
    public List<ApiColumn> Events { get; set; }
}