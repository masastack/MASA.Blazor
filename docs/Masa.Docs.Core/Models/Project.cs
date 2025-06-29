namespace Masa.Docs.Core.Models;

public record Project(string? Key, string Name, string? Repo, string IconUrl, string RepoUrl, string? Path = null)
{
    public bool IsValid => Key is not null;
}
