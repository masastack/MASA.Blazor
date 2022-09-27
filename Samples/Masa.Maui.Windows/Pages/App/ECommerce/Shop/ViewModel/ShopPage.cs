namespace Masa.Maui.Pages.App.ECommerce.Shop;

public class ShopPage
{
    public List<GoodsDto> Datas { get; set; }

    private IEnumerable<GoodsDto> GetFilterDatas()
    {
        IEnumerable<GoodsDto> datas = Datas;

        if (MultiRange is not null)
        {
            datas = MultiRange.RangeType switch
            {
                RangeType.All => datas,
                RangeType.Range => datas.Where(d => d.Price >= MultiRange.LeftNumber && d.Price <= MultiRange.RightNumber),
                RangeType.Less => datas.Where(d => d.Price < MultiRange.LeftNumber),
                RangeType.LessEqual => datas.Where(d => d.Price <= MultiRange.LeftNumber),
                RangeType.More => datas.Where(d => d.Price > MultiRange.LeftNumber),
                RangeType.MoreEqual => datas.Where(d => d.Price >= MultiRange.LeftNumber),
                _ => datas
            };
        }
        if (Category is not null)
        {
            datas = datas.Where(d => d.Category == Category);
        }
        if (Brand is not null)
        {
            datas = datas.Where(d => d.Brand == Brand);
        }

        if (SortType == SortType.Lowest)
        {
            datas = datas.OrderBy(d => d.Price);
        }
        else if (SortType == SortType.Highest)
        {
            datas = datas.OrderByDescending(d => d.Price);
        }

        if (Search is not null)
        {
            datas = datas.Where(d => d.Name.ToUpper().Contains(Search.ToUpper()));
        }

        if (datas.Count() < (PageIndex - 1) * PageSize) PageIndex = 1;

        return datas;
    }

    public List<GoodsDto> GetPageDatas()
    {
        return GetFilterDatas().Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
    }

    public int CurrentCount => GetFilterDatas().Count();

    public int PageIndex { get; set; } = 1;

    public int PageSize { get; set; } = 6;

    public int PageCount => (int)Math.Ceiling(CurrentCount / (double)PageSize);

    public MultiRangeDto? MultiRange { get; set; }

    public string? Category { get; set; }

    public string? Brand { get; set; }

    SortType SortType { get; set; }

    public StringNumber SortTypeLable
    {
        get => SortType.ToString();
        set { SortType = (SortType)Enum.Parse(typeof(SortType), value.ToString()); }
    }

    public string? Search { get; set; }

    public ShopPage(List<GoodsDto> datas)
    {
        Datas = datas;
    }

    public GoodsDto GetGoods(string? id)
    {
        return Datas.FirstOrDefault(a => a.Id.ToString() == id) ?? Datas.First();
    }
}

public enum SortType
{
    Featured,
    Lowest,
    Highest
}




