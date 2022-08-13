namespace Masa.Maui.Data.App.User.Dto;

public class UserDto : IComparable
{
    public string Id { get; set; }

    public string? HeadImg { get; set; }

    [Required]
    public string? UserName { get; set; }

    [Required]
    public string FullName { get; set; } = "";

    public string? SampleName
    {
        get
        {
            return string.Join("", FullName.Split(' ').Select(n => n[0].ToString().ToUpper()));
        }
    }

    public string Status { get; set; }

    [Required]
    public string Role { get; set; }

    [Required]
    public string? Plan { get; set; }

    [Required]
    public string? Country { get; set; }

    [Required]
    public string? Contact { get; set; }

    [Required]
    public string? Company { get; set; }

    public string? Sales { get; set; }

    public string? Profit { get; set; }

    public string? Email { get; set; }

    public DateOnly BirthDate { get; set; }

    public string Mobile { get; set; } 

    public string? Website { get; set; }

    public string? Language { get; set; }

    public string Gender { get; set; }

    public string? ContactOptions { get; set; }

    public string? Address1 { get; set; }

    public string? Address2 { get; set; }

    public string? Address3 { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? Twitter { get; set; }

    public string? Facebook { get; set; }

    public string? Instagram { get; set; }

    public string? Github { get; set; }

    public string? Codepen { get; set; }

    public string? Slack { get; set; }

    public List<PermissionDto> Permissions { get; set; }

    internal string Color { get; }
    public UserDto() { }
    public UserDto(string status,string role,DateOnly birthDate, string mobile,string gender,List<PermissionDto> permissions)
    {
        Id = Guid.NewGuid().ToString();
        Status = status;
        Role = role;
        BirthDate = birthDate;
        Mobile = mobile;
        Gender = gender;
        Permissions=permissions;

        List<string> _colors = new List<string>
        {
            "error", "pry", "remind", "info", "sample-green"
        };
        Random _ran = new Random();
        int index = _ran.Next(0, 5);
        Color = _colors[index];
    }

    public int CompareTo(object? other)
    {
        if (other is UserDto user)
        {
            return FullName.CompareTo(user.FullName);
        }
        else return 1;   
    }

    public string GetFullNameInitials()
    {
        var result = "";
        foreach (var item in FullName.Split(' ', '.'))
        {
            result += item.Substring(0, 1);
        }
        return result;
    }
}
