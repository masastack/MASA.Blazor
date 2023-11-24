using Masa.Blazor.Extensions.Languages.Razor;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.CodeAnalysis;
using Microsoft.JSInterop;
using System.Reflection;

namespace Masa.Blazor.Docs.Components;

[JSCustomElement("masa-example")]
public partial class Example : NextTickComponentBase
{
    [Inject] private BlazorDocService DocService { get; set; } = null!;

    [Inject] public IHttpClientFactory HttpClientFactory { get; set; } = null!;

    [Inject] private NavigationManager NavigationManager { get; set; } = null!;

    [Inject] public IJSRuntime Js { get; set; } = null!;

    [Inject] public MasaBlazor MasaBlazor { get; set; } = null!;

    [Parameter, EditorRequired] public string File { get; set; } = null!;

    [Parameter] public int Index { get; set; }

    [Parameter] public bool NoActions { get; set; }

    [Parameter] public bool Dark { get; set; }

    /// <summary>
    /// 编译器需要使用的程序集
    /// </summary>
    private static readonly List<string> s_assemblies = new()
    {
        "BlazorComponent",
        "Masa.Blazor",
        "OneOf",
        "FluentValidation",
        "netstandard",
        "FluentValidation.DependencyInjectionExtensions",
        "System",
        "Microsoft.AspNetCore.Components",
        "System.Linq.Expressions",
        "System.Net.Http.Json",
        "System.Private.CoreLib",
        "Microsoft.AspNetCore.Components.Web",
        "System.Collections",
        "System.Linq",
        "System.Runtime"
    };

    private Dictionary<string, object> _options = new()
    {
        { "language", "html" },
        { "theme", "vs" },
        { "automaticLayout", true }
    };

    private bool _rendered;
    private bool _prevDark;
    private bool _dark;
    private bool _expand;
    private bool _showExpands;
    private string? _sourceCode;
    private static bool _initialize;
    private Type? _type;
    private Guid _typeIdentity;
    private DotNetObjectReference<Example>? _objRef;
    private MMonacoEditor? _monacoEditor;
    private string? _sourceCodeUri;
    private List<(string Icon, string Path, Func<Task> OnClick, string? href)> _tooltips = new();

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        ArgumentException.ThrowIfNullOrEmpty(File);

        if (_prevDark != Dark)
        {
            _prevDark = Dark;
            _dark = Dark;
        }
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        _objRef = DotNetObjectReference.Create(this);

        CompileRazorProjectFileSystem.AddGlobalUsing("@using BlazorComponent");
        CompileRazorProjectFileSystem.AddGlobalUsing("@using Masa.Blazor");
        CompileRazorProjectFileSystem.AddGlobalUsing("@using Masa.Blazor.Presets");

        var githubUri =
            $"https://github.com/masastack/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/{File.Replace(".", "/").Replace("_", "-")}.razor";

        // From File: Examples.components.alerts.Border
        // To Path: pages/Examples/components/alerts/examples/Border.txt
        var sections = File.Replace("Examples", "pages", StringComparison.OrdinalIgnoreCase).Replace("_", "-")
                           .Split(".").ToList();
        sections.Insert(sections.Count - 1, "examples");
        var sourceCodePath = string.Join("/", sections) + ".txt";
        _sourceCodeUri = $"https://docs.masastack.com/_content/Masa.Blazor.Docs/{sourceCodePath}";

        _tooltips = new()
        {
            new("mdi-reload", "code-restore", RestoreCode, null),
            new("mdi-invert-colors", "invert-example-colors", ToggleTheme, null),
            new("mdi-github", "view-in-github", () => Task.CompletedTask, githubUri),
            new("mdi-code-tags", "view-source", ToggleCode, null)
        };

        MasaBlazor.OnThemeChange += OnMasaBlazorOnOnThemeChange;
    }

    private void InitCompleteHandle()
    {
        // add ctrl+s command
        _ = _monacoEditor?.AddCommandAsync(2097, _objRef!, nameof(RunCode));
    }

    private async void OnMasaBlazorOnOnThemeChange(Theme theme)
    {
        if (_monacoEditor is null)
        {
            return;
        }

        await _monacoEditor.SetThemeAsync(theme.Dark ? "vs-dark" : "vs");
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            NextTick(async () =>
            {
                _dark = Dark;
                _prevDark = Dark;

                if (!_rendered)
                {
                    await Task.Delay(1);
                    _rendered = true;

                    var segments = NavigationManager.GetSegments();

                    var category = segments[2].TrimEnd('/');
                    var title = segments[3].TrimEnd('/');

                    var executingAssemblyName = Assembly.GetExecutingAssembly().GetName().Name;
                    _type = Type.GetType($"{executingAssemblyName}.{File}");

                    if (_type == null)
                    {
                        StateHasChanged();
                        return;
                    }

                    _sourceCode = await DocService.ReadExampleAsync(category, title, _type.Name);

                    StateHasChanged();
                }
            });

            StateHasChanged();
        }
    }

    private async Task RestoreCode()
    {
        var segments = NavigationManager.GetSegments();

        var category = segments[2].TrimEnd('/');
        var title = segments[3].TrimEnd('/');

        var executingAssemblyName = Assembly.GetExecutingAssembly().GetName().Name;
        _type = Type.GetType($"{executingAssemblyName}.{File}");
        _typeIdentity = Guid.NewGuid();

        if (_type == null ||  _monacoEditor is null)
        {
            return;
        }

        var sourceCode = await DocService.ReadExampleAsync(category, title, _type.Name);
        await _monacoEditor!.SetValueAsync(sourceCode);
    }

    private async Task CopyCode()
    {
        var code = await _monacoEditor!.GetValueAsync();

        if (!string.IsNullOrWhiteSpace(code))
        {
            await Js.InvokeVoidAsync(JsInteropConstants.CopyText, code);
        }
    }

    private async Task ToggleTheme()
    {
        _dark = !_dark;
        await Task.CompletedTask;
    }

    private async Task ToggleCode()
    {
        if (!_showExpands)
        {
            _showExpands = true;
            StateHasChanged();

            _options["theme"] = MasaBlazor.Theme.Dark ? "vs-dark" : "vs";
            _expand = true;
        }
        else
        {
            _expand = !_expand;
        }

        await Task.CompletedTask;
    }

    [JSInvokable(nameof(RunCode))]
    public async Task RunCode()
    {
        if (!_initialize)
        {
            RazorCompile.Initialized(await GetReference(), GetRazorExtension());
            _initialize = true;
        }

        var code = await _monacoEditor!.GetValueAsync();
        _type = RazorCompile.CompileToType(new CompileRazorOptions()
        {
            OptimizationLevel = OptimizationLevel.Release,
            Code = code,
        });

        _ = InvokeAsync(StateHasChanged);
    }

    private async Task<List<PortableExecutableReference>?> GetReference()
    {
        var portableExecutableReferences = new List<PortableExecutableReference>();
        if (Js is IJSInProcessRuntime)
        {
            using var http = HttpClientFactory.CreateClient("masa-docs");
            foreach (var asm in s_assemblies)
            {
                try
                {
                    await using var stream = await http.GetStreamAsync($"_framework/{asm}.dll");
                    if (stream.Length > 0)
                    {
                        portableExecutableReferences?.Add(MetadataReference.CreateFromStream(stream));
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
        else
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies().Where(u => !string.IsNullOrWhiteSpace(u.Location)))
            {
                try
                {
                    portableExecutableReferences?.Add(MetadataReference.CreateFromFile(assembly.Location));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        return portableExecutableReferences;
    }

    private static List<RazorExtension> GetRazorExtension()
    {
        return typeof(Example).Assembly.GetReferencedAssemblies()
            .Select(asm =>
            {
                try
                {
                    return new AssemblyExtension(asm.FullName,
                        AppDomain.CurrentDomain.Load(asm.FullName));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message, asm.FullName);
                    return null;
                }
            }).Where(x => x != null)
            .Cast<RazorExtension>().ToList();
    }

    protected override void Dispose(bool disposing)
    {
        _objRef?.Dispose();

        base.Dispose(disposing);
    }
}
