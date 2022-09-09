namespace Masa.Maui.Pages.App.User;

public class UserPage
{
    public List<UserDto> UserDatas { get; set; }

    public string? Role { get; set; }

    public string? Plan { get; set; }

    public string? Status { get; set; }

    public string? Search { get; set; }

    public int PageIndex { get; set; } = 1;

    public int PageSize { get; set; } = 100;

    public int PageCount => (int)Math.Ceiling(CurrentCount / (double)PageSize);

    public int CurrentCount => GetFilterDatas().Count();

    public UserPage(List<UserDto> datas)
    {
        UserDatas = new List<UserDto>();
        UserDatas.AddRange(datas);
    }

    private IEnumerable<UserDto> GetFilterDatas()
    {
        IEnumerable<UserDto> datas = UserDatas;
            
        if(Search is not null)
        {
            datas = datas.Where(d => d.FullName.Contains(Search, StringComparison.OrdinalIgnoreCase) || d.Email?.Contains(Search, StringComparison.OrdinalIgnoreCase) ==true);
        }

        if(Role is not null)
        {
            datas = datas.Where(d => d.Role == Role);
        }

        if (Plan is not null)
        {
            datas = datas.Where(d => d.Plan == Plan);
        }

        if (Status is not null)
        {
            datas = datas.Where(d => d.Status == Status);
        }

        if(datas.Count() < (PageIndex-1) * PageSize) PageIndex = 1;

        return datas;
    }

    public List<UserDto> GetPageDatas()
    {
        return GetFilterDatas().Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
    }
}

