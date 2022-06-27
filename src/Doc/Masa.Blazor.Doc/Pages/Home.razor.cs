using BlazorComponent;
using BlazorComponent.I18n;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Masa.Blazor.Doc.Pages;

public partial class Home
{
    [Inject]
    public I18n I18n { get; set; } = null!;

    [Inject]
    public IJSRuntime JsRuntime { get; set; } = default!;

    /// <summary>
    /// Not being used, just notifying the component that it needs to be re-rendered
    /// </summary>
    [CascadingParameter(Name = "Culture")]
    public string Culture { get; set; }

    private int _onBoarding = 0;
    private int _length = 1;

    private StringNumber OnBoarding
    {
        get => _onBoarding;
        set => _onBoarding = value.AsT1;
    }

    private async Task Toggle(string url)
    {
        if (!string.IsNullOrWhiteSpace(url))
        {
            await JsRuntime.InvokeVoidAsync("window.open", url);
        }
    }

    private string T(string key) => I18n.T(key);
}