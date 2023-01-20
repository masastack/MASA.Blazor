using BlazorComponent.I18n;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;
using YamlDotNet.Serialization;

namespace Masa.Blazor.Docs.Pages;

public partial class Components
{
    [Inject]
    private BlazorDocService BlazorDocService { get; set; } = null!;

    [Inject]
    private DocService DocService { get; set; } = null!;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    private AppService AppService { get; set; } = null!;

    [Inject]
    private I18n I18n { get; set; } = null!;

    [CascadingParameter]
    private CultureInfo Culture { get; set; } = null!;

    [Parameter]
    public string Project { get; set; } = null!;

    [Parameter]
    public string Page { get; set; } = null!;

    [Parameter]
    public string? Tab
    {
        get => _tab;
        set
        {
            if (value != _tab)
            {
                _tab = value;
                AppService.Toc = CurrentToc;
            }
        }
    }

    [Parameter]
    public string? Api { get; set; }

    [Parameter]
    public string? CurrentApi
    {
        get => Api ?? _apiData.Keys.FirstOrDefault();
        set
        {
            if (value != Api)
            {
                NavigationManager.NavigateTo($"/blazor/components/{Page}/{Tab}/{value}");
            }
        }
    }

    public List<MarkdownItTocContent> CurrentToc
    {
        get
        {
            if (IsApiTab)
            {
                if (CurrentApi is not null)
                {
                    return _apiData[CurrentApi].Keys.Select(k => new MarkdownItTocContent()
                    {
                        Content = k,
                        Anchor = k,
                        Level = 2
                    }).ToList();
                }
                else return new();
            }
            else
            {
                return _documentToc;
            }
        }
    }

    private string? _tab;
    private string? _md;
    private string? _prevPage;
    private CultureInfo? _prevCulture;
    private string? _prevApi;
    private FrontMatterMeta? _frontMatterMeta;
    private readonly Dictionary<string, Dictionary<string, List<Masa.Blazor.Docs.ParameterInfo>>> _apiData = new();
    private List<MarkdownItTocContent> _documentToc = new();

    private bool IsApiTab => Tab is not null && Tab.Equals("api", StringComparison.OrdinalIgnoreCase);

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        if (!Equals(_prevPage, Page) || !Equals(_prevCulture, Culture))
        {
            _prevPage = Page;
            _prevCulture = Culture;
            _apiData.Clear();
            await ReadDocumentAndApiAsync();
            AppService.Toc = CurrentToc;
        }
        else if ((IsApiTab && _apiData.Any() is false) || (!IsApiTab && _md is null))
        {
            await ReadDocumentAndApiAsync();
            AppService.Toc = CurrentToc;
        }
        else if (!Equals(_prevApi, Api))
        {
            _prevApi = Api;
            if (Api is not null)
            {
                AppService.Toc = CurrentToc;
            }
        }
    }

    private async Task ReadDocumentAndApiAsync()
    {
        await ReadDocumentAsync();
        if (IsApiTab)
        {
            await ReadApisAsync();
        }
    }

    private void NavigateToTab(string tab)
    {
        NavigationManager.NavigateTo($"/blazor/components/{Page}/{tab}");
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
        _documentToc = contents;
        AppService.Toc = CurrentToc;
    }

    private async Task ReadDocumentAsync()
    {
        try
        {
            _md = await DocService.ReadDocumentAsync(Project, "components", Page);
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
        _apiData.Clear();
        var name = Page;

        var pageToApi = await BlazorDocService.ReadPageToApiAsync();
        bool isMultipleApi = false;
        if (pageToApi.ContainsKey(Page))
        {
            var apis = pageToApi[Page];
            isMultipleApi = apis.Count > 1;
            await apis.ForEachAsync(async componentName => { _apiData[componentName] = await getApiGroupAsync(componentName, true); });
        }
        else
        {
            var apiGroup = await getApiGroupAsync(FormatName(name));
            _apiData[FormatName(name)] = apiGroup;
        }

        async Task<Dictionary<string, List<ParameterInfo>>> getApiGroupAsync(string name, bool isFullname = false)
        {
            var component = isFullname
                ? ApiGenerator.ComponentMetas.FirstOrDefault(u => u.Name == name)
                : ApiGenerator.ComponentMetas.FirstOrDefault(u =>
                    Regex.IsMatch(u.Name.ToUpper(), $"[M|P]{{1}}{name}s?$".ToUpper()));

            if (component is not null)
            {
                var parametersCacheValue = component.Parameters;

                parametersCacheValue = parametersCacheValue.Where(item => item.Value.Count > 0).ToDictionary(item => item.Key, item => item.Value);

                var descriptionGroup = await BlazorDocService.ReadApisAsync(Page, isMultipleApi ? name : default);

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

    [return: NotNullIfNotNull("name")]
    private static string? FormatName(string? name)
    {
        if (name is null) return null;
        name = name.TrimEnd('s');
        return KebabToPascal(name);
    }
}
