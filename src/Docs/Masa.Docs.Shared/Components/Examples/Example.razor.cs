using System.Reflection;

namespace Masa.Docs.Shared.Components;

public partial class Example : NextTickComponentBase
{
    [Inject]
    private DocService DocService { get; set; } = null!;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    [Parameter, EditorRequired]
    public string File { get; set; } = null!;

    [Parameter]
    public int Index { get; set; }

    private readonly List<(string Code, string Language)> _sections = new();

    private bool _rendered;
    private bool _dark;
    private bool _expand;
    private StringNumber _selected = 0;
    private Type? _type;
    private List<(string Icon, string Path, Action? OnClick, string? href)> _tooltips = new();

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        ArgumentException.ThrowIfNullOrEmpty(File);
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        _tooltips = new()
        {
            new("mdi-invert-colors", "invert-example-colors", () => _dark = !_dark, null),
            new("mdi-github", "view-in-github", null, "https://github.com/BlazorComponent"), // todo:dynamic
            new("mdi-code-tags", "view-source", () => _expand = !_expand, null)
        };
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            NextTick(async () =>
            {
                if (!_rendered)
                {
                    await Task.Delay(1);
                    _rendered = true;

                    var uri = new Uri(NavigationManager.Uri);
                    var category = uri.Segments[1].Replace("/", "");
                    var title = uri.Segments[2].Replace("/", "");

                    var executingAssemblyName = Assembly.GetExecutingAssembly().GetName().Name;
                    _type = Type.GetType($"{executingAssemblyName}.{File}");

                    ArgumentNullException.ThrowIfNull(_type, $"{File} does not exists");

                    var sourceCode = await DocService.ReadExampleAsync(category, title, _type.Name);

                    var (razor, cs, css) = ResolveSourceCode(sourceCode);

                    if (!string.IsNullOrWhiteSpace(razor))
                    {
                        _sections.Add((razor, nameof(razor)));
                    }

                    if (!string.IsNullOrWhiteSpace(cs))
                    {
                        _sections.Add((cs, nameof(cs)));
                    }

                    if (!string.IsNullOrWhiteSpace(css))
                    {
                        _sections.Add((css, nameof(css)));
                    }

                    StateHasChanged();
                }
            });
            
            StateHasChanged();
        }
    }

    private static(string? Razor, string? cs, string? css) ResolveSourceCode(string sourceCode)
    {
        string? razor = null;
        string? cs = null;
        string? css = null;

        var code = sourceCode;
        var cssFrom = sourceCode.IndexOf("<style", StringComparison.Ordinal);
        var cssTo = sourceCode.IndexOf("</style>", StringComparison.Ordinal) + "</style>".Length;

        if (cssFrom > -1 && cssTo > -1)
        {
            var cssContent = sourceCode.Substring(cssFrom, cssTo - cssFrom);
            css = cssContent;

            code = code.Replace(cssContent, "");
        }

        var codeIndex = code.IndexOf("@code");
        if (codeIndex > -1)
        {
            razor = code.Substring(0, codeIndex).Trim();
            cs = code.Substring(codeIndex).Trim();
        }
        else
        {
            razor = code.Trim();
        }

        return (razor, cs, css);
    }
}
