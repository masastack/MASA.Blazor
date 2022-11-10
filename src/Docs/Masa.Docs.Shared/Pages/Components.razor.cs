using System.Net;
using System.Text.RegularExpressions;
using Masa.Docs.Shared.ApiGenerator;
using Microsoft.AspNetCore.Components.Routing;
using YamlDotNet.Serialization;

namespace Masa.Docs.Shared.Pages;

public partial class Components : IDisposable
{
    [Inject]
    private DocService DocService { get; set; } = null!;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    private AppService AppService { get; set; } = null!;

    [Parameter]
    public string Page { get; set; } = null!;

    [Parameter]
    public string? Tab { get; set; }

    private string? _prevPage;

    private string? _md;
    private FrontMatterMeta? _frontMatterMeta;

    private readonly Dictionary<string, Dictionary<string, List<ParameterInfo>>> _apiData = new();
    private readonly Dictionary<string, List<MarkdownItTocContent>> _tocCache = new();

    private bool IsApiTab => Tab is not null && Tab.Equals("api", StringComparison.OrdinalIgnoreCase);
    private string StandardName => FormatName(Page);

    protected override void OnInitialized()
    {
        base.OnInitialized();

        _prevPage = Page;
        NavigationManager.LocationChanged += NavigationManagerOnLocationChanged;
    }

    private async void NavigationManagerOnLocationChanged(object? sender, LocationChangedEventArgs e)
    {
        if (_prevPage != Page)
        {
            _prevPage = Page;
            _tocCache.Clear();
            _apiData.Clear();

            await ReadDocumentAsync();
            await InvokeAsync(StateHasChanged);
            return;
        }

        if (IsApiTab && _apiData.Count == 0)
        {
            await ReadApisAsync();

            foreach (var (key, dict) in _apiData)
            {
                _tocCache.Add(key, dict.Keys.Select(k => new MarkdownItTocContent()
                {
                    Content = k,
                    Anchor = k,
                    Level = 2
                }).ToList());
            }

            AppService.Toc = _tocCache[FormatName(Page)];

            await InvokeAsync(StateHasChanged);
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            await ReadDocumentAsync();

            if (IsApiTab)
            {
                await ReadApisAsync();
            }

            StateHasChanged();
        }
    }

    private void NavigateToTab(string tab)
    {
        NavigationManager.NavigateTo($"/components/{Page}/{tab}");

        if (_tocCache.TryGetValue(tab == "" ? "doc" : StandardName, out var toc))
        {
            AppService.Toc = toc;
        }
    }

    private void OnFrontMatterParsed(string? yaml)
    {
        if (yaml is null)
        {
            return;
        }

        _frontMatterMeta = new DeserializerBuilder().IgnoreUnmatchedProperties().Build().Deserialize<FrontMatterMeta>(yaml);
    }

    private void OnTocParsed(List<MarkdownItTocContent>? contents)
    {
        if (IsApiTab)
        {
            return;
        }

        AppService.Toc = contents;
        _tocCache["doc"] = contents;
    }

    private async Task ReadDocumentAsync()
    {
        try
        {
            _md = await DocService.ReadDocumentAsync("components", Page);
        }
        catch (HttpRequestException e)
        {
            if (e.StatusCode == HttpStatusCode.NotFound)
            {
                NavigationManager.NavigateTo("/404");
            }
        }
    }

    private async Task ReadApisAsync()
    {
        if (_apiData.Count > 0)
        {
            return;
        }

        var name = Page;

        var pageToApi = await DocService.ReadPageToApiAsync();

        if (pageToApi.ContainsKey(Page))
        {
            await pageToApi[Page].ForEachAsync(async componentName => { _apiData[componentName] = await getApiGroupAsync(componentName, true); });
        }
        else
        {
            var apiGroup = await getApiGroupAsync(FormatName(name));
            _apiData[FormatName(name)] = apiGroup;
        }

        async Task<Dictionary<string, List<ParameterInfo>>> getApiGroupAsync(string name, bool isFullname = false)
        {
            var component = isFullname
                ? ApiGenerator.ApiGenerator.parametersCache.Keys.FirstOrDefault(key => key == name)
                : ApiGenerator.ApiGenerator.parametersCache.Keys.FirstOrDefault(key => Regex.IsMatch(key, $"[M|P]{{1}}{name}$"));

            if (component is not null)
            {
                var parametersCacheValue = ApiGenerator.ApiGenerator.parametersCache[component];

                parametersCacheValue = parametersCacheValue.Where(item => item.Value.Count > 0).ToDictionary(item => item.Key, item => item.Value);

                var descriptionGroup = await DocService.ReadApisAsync(Page);

                if (descriptionGroup is not null)
                {
                    foreach (var group in descriptionGroup)
                    {
                        if (!parametersCacheValue.ContainsKey(group.Key))
                        {
                            continue;
                        }

                        var parameters = parametersCacheValue[group.Key];
                        foreach (var (prop, desc) in group.Value)
                        {
                            var parameter = parameters.FirstOrDefault(param => param.Name.Equals(prop, StringComparison.OrdinalIgnoreCase));
                            if (parameter is not null)
                            {
                                parameter.Description = desc;
                            }
                        }
                    }
                }

                return parametersCacheValue;
            }

            return new Dictionary<string, List<ParameterInfo>>();
        }
    }

    private static string KebabToPascal(string name)
    {
        name = name.Trim('-');
        return string.Join("", name.Split('-').Select(item => char.ToUpper(item[0]) + item.Substring(1)));
    }

    private static string FormatName(string name)
    {
        name = name.TrimEnd('s');
        return KebabToPascal(name);
    }

    public void Dispose()
    {
        NavigationManager.LocationChanged -= NavigationManagerOnLocationChanged;
    }
}
