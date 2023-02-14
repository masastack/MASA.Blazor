namespace Microsoft.AspNetCore.Components;

public static class NavigationManagerExtensions
{
    public static void NavigateToHash(this NavigationManager nav, string hash)
    {
        var uri = new Uri(nav.Uri);

        if (uri.Fragment == hash) return;

        nav.NavigateTo(uri.AbsolutePath + hash);
    }
}