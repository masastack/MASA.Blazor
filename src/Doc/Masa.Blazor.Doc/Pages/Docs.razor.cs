using System.Globalization;
using BlazorComponent.I18n;
using Masa.Blazor.Doc.Highlight;
using Masa.Blazor.Doc.Models;
using Masa.Blazor.Doc.Services;
using Microsoft.AspNetCore.Components;

namespace Masa.Blazor.Doc.Pages;

public partial class Docs
{
    [Inject]
    private NavigationManager NavigationManager { get; set; }

    [Inject]
    private IPrismHighlighter PrismHighlighter { get; set; }

    [Inject]
    private I18n I18n { get; set; }

    [Inject]
    public DemoService Service { get; set; }

    [CascadingParameter(Name = "Culture")]
    public CultureInfo Culture { get; set; }

    [Parameter]
    public string Category { get; set; }

    [Parameter]
    public string FileName { get; set; }

    private string _previousPath;

    private string Path => $"{Category}/{FileName}.{Culture}";

    private DocFileModel File { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (string.IsNullOrWhiteSpace(FileName))
        {
            var menus = await Service.GetMenuAsync();
            var current = menus.FirstOrDefault(x => x.Url == Category);
            if (current == null)
                throw new HttpRequestException("No page matched", new Exception("No page matched"), System.Net.HttpStatusCode.NotFound);

            NavigationManager.NavigateTo(current.Children[0].Url);
        }
    }

    protected override async Task OnParametersSetAsync()
    {
        if (string.IsNullOrWhiteSpace(FileName)) return;

        if (_previousPath == Path) return;
        _previousPath = Path;

        File = await Service.GetDocFileAsync($"_content/Masa.Blazor.Doc/docs/{Path}.json");
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await PrismHighlighter.HighlightAllAsync();
    }
}
