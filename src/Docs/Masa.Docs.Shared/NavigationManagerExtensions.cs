namespace Masa.Docs.Shared;

public static class NavigationManagerExtensions
{
    public static void Replace(this NavigationManager navigationManager, string uri)
    {
        navigationManager.NavigateTo(uri, replace: true);
    }

    public static void ReplaceWithHash(this NavigationManager navigationManager, string hash)
    {
        var uri = new Uri(navigationManager.Uri);
        navigationManager.Replace($"{uri.AbsolutePath}{hash}");
    }

    public static string GetAbsolutePath(this NavigationManager navigationManager)
    {
        var uri = new Uri(navigationManager.Uri);
        return uri.AbsolutePath;
    }
}
