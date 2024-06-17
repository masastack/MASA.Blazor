namespace Masa.Blazor;

public class DataOptions
{
    public int Page { get; set; }

    public int ItemsPerPage { get; set; }

    public IList<bool> SortDesc { get; set; } = new List<bool>();

    public IList<bool> GroupDesc { get; set; } = new List<bool>();

    public bool MustSort { get; set; }

    public bool MultiSort { get; set; }

    public IList<string> SortBy { get; set; } = new List<string>();

    public IList<string> GroupBy { get; set; } = new List<string>();

    public DataOptions()
    {
    }

    public DataOptions(int page, int itemsPerPage)
    {
        Page = page;
        ItemsPerPage = itemsPerPage;
    }
}