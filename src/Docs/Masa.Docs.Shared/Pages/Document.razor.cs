using Masa.Docs.Shared.Shared;
using Microsoft.AspNetCore.Components.Routing;
using System.Globalization;
using System.Net;

namespace Masa.Docs.Shared.Pages;

public partial class Document : IDisposable
{
    [Inject]
    private DocService DocService { get; set; } = null!;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    private AppService AppService { get; set; } = null!;

    [CascadingParameter]
    private CultureInfo Culture { get; set; } = null!;

    [CascadingParameter]
    private BaseLayout BaseLayout { get; set; } = null!;

    [Parameter]
    public string Project { get; set; } = null!;

    [Parameter]
    public string Category { get; set; } = null!;

    [Parameter] public string Page { get; set; } = null!;

    [Parameter] public string? SubPage { get; set; }

    private string? _md;
    private CultureInfo? _prevCulture;
    private string? _prevAbsolutePath;

    private Dictionary<string, string[]> _projectMap = new()
    {
        { "stack", new string[] { "MASA Stack", "https://cdn.masastack.com/stack/images/logo/MASAStack/logo.png" } },
        { "framework", new string[] { "MASA Framework", "https://cdn.masastack.com/images/framework_logo.png" } },
        { "blazor", new string[] { "MASA Blazor", "https://cdn.masastack.com/stack/images/website/masa-blazor/miniLogo.png" } }
    };

    private string GithubUri
    {
        get
        {
            return Project switch
            {
                "blazor" =>
                    $"https://github.com/BlazorComponent/MASA.Blazor/blob/main/src/Docs/Masa.Blazor.Docs/wwwroot/pages/{Category}/{Page}/{Culture.Name}.md",
                "framework" or "stack" =>
                    $"https://github.com/masastack/MASA.Docs/blob/main/src/Masa.{Project.ToUpperFirst()}.Docs/wwwroot/pages/{Category}/{Page}/{(string.IsNullOrWhiteSpace(SubPage) ? "" : $"{SubPage}/")}{Culture.Name}.md",
                _ => string.Empty
            };
        }
    }

    protected override void OnInitialized()
    {
        BaseLayout.PageInfo.PageTitle = _projectMap.GetValueOrDefault(Project)?[0];
        BaseLayout.PageInfo.PageIcon = _projectMap.GetValueOrDefault(Project)?[1];

        base.OnInitialized();

        NavigationManager.LocationChanged += NavigationManagerOnLocationChanged;
    }

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();
        if (_prevCulture is not null && !Equals(_prevCulture, Culture))
        {
            _prevCulture = Culture;
            await ReadDocumentAsync();
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            _prevCulture = Culture;
            _prevAbsolutePath = NavigationManager.GetAbsolutePath();

            await ReadDocumentAsync();
            StateHasChanged();
        }
    }

    private async void NavigationManagerOnLocationChanged(object? sender, LocationChangedEventArgs e)
    {
        var absolutePath = NavigationManager.GetAbsolutePath();
        if (_prevAbsolutePath != absolutePath)
        {
            _prevAbsolutePath = absolutePath;
            await ReadDocumentAsync();
        }

        await InvokeAsync(StateHasChanged);
    }

    private void OnTocParsed(List<MarkdownItTocContent>? contents)
    {
        AppService.Toc = contents;
    }

    private async Task ReadDocumentAsync()
    {
        try
        {
            _md = await DocService.ReadDocumentAsync(Project, Category, Page, SubPage);
        }
        catch (HttpRequestException e)
        {
            if (e.StatusCode == HttpStatusCode.NotFound)
            {
                NavigationManager.NavigateTo("/404");
            }
        }
    }

    public void Dispose()
    {
        NavigationManager.LocationChanged -= NavigationManagerOnLocationChanged;
    }
}
