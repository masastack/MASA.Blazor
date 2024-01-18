namespace Masa.Blazor;

public class DataOptions : ObservableObject
{
    public DataOptions()
    {
    }

    public DataOptions(int page, int itemsPerPage)
    {
        Page = page;
        ItemsPerPage = itemsPerPage;
    }

    public int Page
    {
        get => GetValue<int>();
        set => SetValue(value);
    }

    public int ItemsPerPage
    {
        get => GetValue<int>();
        set => SetValue(value);
    }

    public IList<bool> SortDesc
    {
        get => GetValue<IList<bool>>() ?? new List<bool>();
        set => SetValue(value);
    }

    public IList<bool> GroupDesc
    {
        get => GetValue<IList<bool>>() ?? new List<bool>();
        set => SetValue(value);
    }

    public bool MustSort
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    public bool MultiSort
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    public IList<string> SortBy
    {
        get => GetValue<IList<string>>() ?? new List<string>();
        set => SetValue(value);
    }

    public IList<string> GroupBy
    {
        get => GetValue<IList<string>>() ?? new List<string>();
        set => SetValue(value);
    }
}
