namespace Masa.Docs.Shared;

public static class NavigationManagerExtensions
{
    public static void Replace(this NavigationManager navigationManager, string uri)
    {
        navigationManager.NavigateTo(uri, replace: true);
    }

    public static void ReplaceWithHash(this NavigationManager navigationManager, string hash)
    {
        navigationManager.Replace($"{navigationManager.GetAbsolutePath()}{hash}");
    }

    public static string GetAbsolutePath(this NavigationManager navigationManager)
    {
        return navigationManager.ToUri().AbsolutePath;
    }

    public static string[] GetSegments(this NavigationManager navigationManager)
    {
        return navigationManager.ToUri().Segments;
    }

    private static Uri ToUri(this NavigationManager navigationManager)
    {
        return new Uri(navigationManager.Uri);
    }
}
