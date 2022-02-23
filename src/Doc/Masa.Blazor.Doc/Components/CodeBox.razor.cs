using BlazorComponent;
using BlazorComponent.I18n;
using Masa.Blazor.Doc.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Masa.Blazor.Doc.Components;

public partial class CodeBox
{
    private static string _githubUrlTemplate =
        "https://github.com/BlazorComponent/Masa.Blazor/blob/main/src/Doc/Masa.Blazor.Doc/{0}.razor";

    private readonly static (string type, string lang) Template = ("template", "html");
    private readonly static (string type, string lang) Code = ("code", "csharp");
    private readonly static (string type, string lang) Style = ("style", "css");

    private readonly Dictionary<(string type, string lang), string> _items = new()
    {
        { Template, null },
        { Code, null },
        { Style, null },
    };

    private StringNumber _activeItem;
    private bool _expend;
    private bool _showComponent;
    private bool _isDark;

    private RenderFragment Component { get; set; }

    [Inject]
    public IJSRuntime Js { get; set; }

    [Parameter]
    public string ComponentName { get; set; }

    [Parameter]
    public DemoItemModel Demo { get; set; }

    [Parameter]
    public int Index { get; set; }

    [Inject]
    public I18n I18n { get; set; }

    private string GithubUrlHref { get; set; }

    protected override void OnInitialized()
    {
        _showComponent = Index < 2;

        if (Demo.Type == null) return;

        Component = Service.GetShowCase(Demo.Type);

        var path = Demo.Type.Replace(".", "/");
        GithubUrlHref = string.Format(_githubUrlTemplate, path);

        if (Demo.Code == null) return;

        var styleFrom = Demo.Code.IndexOf("<style", StringComparison.Ordinal);
        var styleTo = Demo.Code.IndexOf("</style>", StringComparison.Ordinal) + "</style>".Length;

        var code = Demo.Code;
        if (styleFrom > -1 && styleTo > -1)
        {
            var styleContent = Demo.Code.Substring(styleFrom, styleTo - styleFrom);
            _items[Style] = styleContent;

            code = code.Replace(styleContent, "");
        }

        var index = code.IndexOf("@code");
        if (index > -1)
        {
            _items[Template] = code.Substring(0, index).Trim();
            _items[Code] = code.Substring(index).Trim();
        }
        else
        {
            _items[Template] = code.Trim();
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            if (!_showComponent)
            {
                await Task.Delay(Index * 16);
                _showComponent = true;
                StateHasChanged();
            }
        }
    }

    protected string T(string key)
    {
        return I18n.T(key);
    }
}