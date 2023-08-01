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

    [Inject] private IPopupService PopupService { get; set; } = null!;
    [Inject] public IJSRuntime Js { get; set; } = null!;

    [Parameter, EditorRequired] public string File { get; set; } = null!;

    [Parameter] public int Index { get; set; }

    [Parameter] public bool NoActions { get; set; }

    [Parameter] public bool Dark { get; set; }

    private Sections _sections;

    /// <summary>
    /// 编译器需要使用的程序集
    /// </summary>
    private static readonly List<string> assemblies = new()
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

    private bool _rendered;
    private bool _prevDark;
    private bool _dark;
    private bool _expand;
    private bool _showExpands;
    private bool _load;
    private static bool _initialize;
    private StringNumber _selected = 0;
    private Type? _type;
    private DotNetObjectReference<Example>? _objRef;
    private MMonacoEditor MonacoEditor;
    private List<(string Icon, string Path, Action? OnClick, string? href)> _tooltips = new();

    private object options = new
    {
        language = "html",
        theme = "vs",
        automaticLayout = true,
    };

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
        var sourceCodeUri = $"https://docs.masastack.com/_content/Masa.Blazor.Docs/{sourceCodePath}";

        _tooltips = new()
        {
            new("mdi-play-circle-outline", "run-example", null, $"https://try.masastack.com?path={sourceCodeUri}"),
            new("mdi-invert-colors", "invert-example-colors", () => _dark = !_dark, null),
            new("mdi-github", "view-in-github", null, githubUri),
            new("mdi-code-tags", "view-source", ToggleCode, null)
        };
    }

    private async Task InitCompleteHandle()
    {
        await MonacoEditor.AddCommandAsync(2097, _objRef, nameof(RunCode));
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

                    var sourceCode = await DocService.ReadExampleAsync(category, title, _type.Name);

                    _sections = new Sections(sourceCode, "razor");

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

        if (_type == null)
        {
            StateHasChanged();
            return;
        }

        var sourceCode = await DocService.ReadExampleAsync(category, title, _type.Name);
        await MonacoEditor.SetValueAsync(sourceCode);
    }

    private async Task CopyCode()
    {
        var code = await MonacoEditor.GetValueAsync();

        if (!string.IsNullOrWhiteSpace(code))
        {
            await Js.InvokeVoidAsync(JsInteropConstants.CopyText, code);
            await PopupService.EnqueueSnackbarAsync("Copy success",AlertTypes.Success);
        }
        else
        {
            await PopupService.EnqueueSnackbarAsync("The code is empty", AlertTypes.Success);
        }
    }

    private void ToggleCode()
    {
        if (!_showExpands)
        {
            _showExpands = true;
            StateHasChanged();

            _expand = true;
        }
        else
        {
            _expand = !_expand;
        }
    }

    [JSInvokable(nameof(RunCode))]
    public async Task RunCode()
    {
        _load = true;
        await Task.Delay(50);
        _ = InvokeAsync(StateHasChanged);

        if (!_initialize)
        {
            RazorCompile.Initialized(await GetReference(), GetRazorExtension());
        }

        try
        {

            string code = await MonacoEditor.GetValueAsync();
            _type = RazorCompile.CompileToType(new CompileRazorOptions()
            {
                OptimizationLevel = OptimizationLevel.Release,
                Code = code,
            });
        }
        catch (Exception e)
        {
            await PopupService.EnqueueSnackbarAsync(e.Message, AlertTypes.Error);
        }

        _load = false;
        _ = InvokeAsync(StateHasChanged);
    }


    private async Task<List<PortableExecutableReference>?> GetReference()
    {
        var portableExecutableReferences = new List<PortableExecutableReference>();

        using var http = HttpClientFactory.CreateClient("masa-docs");
        foreach (var asm in assemblies)
        {
            try
            {
                await using var stream = await http!.GetStreamAsync($"_framework/{asm}.dll");
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

        _initialize = true;

        return portableExecutableReferences;
    }

    private static List<RazorExtension> GetRazorExtension()
    {
        return typeof(Example).Assembly.GetReferencedAssemblies()
            .Select(asm => new AssemblyExtension(asm.FullName, AppDomain.CurrentDomain.Load(asm.FullName)))
            .Cast<RazorExtension>().ToList();
    }
}