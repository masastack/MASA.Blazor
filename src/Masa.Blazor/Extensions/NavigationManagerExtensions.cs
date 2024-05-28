namespace BlazorComponent;

public static class NavigationManagerExtensions
{
    public static void Replace(this NavigationManager navigationManager, string uri)
    {
        navigationManager.NavigateTo(uri, replace: true);
    }

    /// <summary>
    /// Gets the absolute path of the current URI.
    /// </summary>
    /// <param name="navigationManager"></param>
    /// <returns></returns>
    public static string GetAbsolutePath(this NavigationManager navigationManager)
    {
        return navigationManager.ToUri().AbsolutePath;
    }

    public static string[] GetSegments(this NavigationManager navigationManager)
    {
        return navigationManager.ToUri().Segments;
    }

    /// <summary>
    /// Gets the current URI.
    /// </summary>
    /// <param name="navigationManager"></param>
    /// <returns></returns>
    public static Uri ToUri(this NavigationManager navigationManager)
    {
        return new Uri(navigationManager.Uri);
    }

    public static string? GetHash(this NavigationManager navigationManager)
    {
        var fragment = navigationManager.ToUri().Fragment;
        return fragment == string.Empty ? null : fragment;
    }

    public static void NavigateWithQueryParameter(this NavigationManager navigationManager, string name, bool? value)
    {
        navigationManager.NavigateTo(navigationManager.GetUriWithQueryParameter(name, value));
    }

    public static void NavigateWithQueryParameter(this NavigationManager navigationManager, string name, string? value)
    {
        navigationManager.NavigateTo(navigationManager.GetUriWithQueryParameter(name, value));
    }

    public static void NavigateWithQueryParameter(this NavigationManager navigationManager, string name, int? value)
    {
        navigationManager.NavigateTo(navigationManager.GetUriWithQueryParameter(name, value));
    }

    public static string GetRelativeUriWithQueryParameters(this NavigationManager navigationManager, IReadOnlyDictionary<string, object?> parameters)
    {
        var uriWithQueryParameters = navigationManager.GetUriWithQueryParameters(parameters);
        return navigationManager.ToRelativeUriWithQueryParameters(uriWithQueryParameters);
    }

    public static string GetRelativeUriWithQueryParameters(this NavigationManager navigationManager, string uri, IReadOnlyDictionary<string, object?> parameters)
    {
        var uriWithQueryParameters = navigationManager.GetUriWithQueryParameters(uri, parameters);
        return navigationManager.ToRelativeUriWithQueryParameters(uriWithQueryParameters);
    }

    public static string GetRelativeUriWithQueryParameter(this NavigationManager navigationManager, string name, bool? value)
    {
        var uriWithQueryParameter = navigationManager.GetUriWithQueryParameter(name, value);
        return navigationManager.ToRelativeUriWithQueryParameters(uriWithQueryParameter);
    }

    public static string GetRelativeUriWithQueryParameter(this NavigationManager navigationManager, string name, string? value)
    {
        var uriWithQueryParameter = navigationManager.GetUriWithQueryParameter(name, value);
        return navigationManager.ToRelativeUriWithQueryParameters(uriWithQueryParameter);
    }

    private static string ToRelativeUriWithQueryParameters(this NavigationManager navigationManager, string absoluteUriWithQueryParameters)
    {
        var baseUri = navigationManager.BaseUri;
        var relativeUriWithQueryParameters = absoluteUriWithQueryParameters.Replace(baseUri, string.Empty);
        return relativeUriWithQueryParameters.StartsWith("/") ? relativeUriWithQueryParameters : "/" + relativeUriWithQueryParameters;
    }
}
