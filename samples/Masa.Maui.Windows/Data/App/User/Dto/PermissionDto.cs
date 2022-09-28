namespace Masa.Maui.Data.App.User.Dto;

public class PermissionDto
{
    public string Module { get; set; } = default!;

    public bool Read { get; set; }

    public bool Write { get; set; }

    public bool Create { get; set; }

    public bool Delete { get; set; }
}