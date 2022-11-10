using System.Globalization;
using System.Net;
using Microsoft.AspNetCore.Components.Routing;

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

    [Parameter]
    public string Category { get; set; } = null!;

    [Parameter] public string Page { get; set; } = null!;

    private string? _md;
    private CultureInfo? _prevCulture;
    private string? _prevAbsolutePath;

    protected override void OnInitialized()
    {
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
            _md = await DocService.ReadDocumentAsync(Category, Page);
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
