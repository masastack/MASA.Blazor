namespace Masa.Blazor.Docs;

public class SelectParameter
{
    public List<string> Items { get; set; }

    public string? Value { get; set; }

    public SelectParameter(List<string> items, string? value = null)
    {
        Items = items;
        Value = value;
    }
}
