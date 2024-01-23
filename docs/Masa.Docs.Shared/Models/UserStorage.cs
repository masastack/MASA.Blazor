namespace Masa.Docs.Shared.Models;

public class UserStorage
{
    public Notifications Notifications { get; set; } = new();
}

public class Notifications
{
    public List<string> Read { get; set; } = new();
}
