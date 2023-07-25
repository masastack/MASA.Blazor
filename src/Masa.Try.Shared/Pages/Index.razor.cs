using Masa.Blazor;
using Masa.Blazor.Extensions.Languages.Razor;
using Masa.Try.Shared.Pages.Options;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using System.Runtime.Loader;
using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System.Text.RegularExpressions;

namespace Masa.Try.Shared.Pages;

public partial class Index : NextTickComponentBase
{
    private const string REPOSITORY_URL = "https://github.com/masastack/Masa.Blazor";

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

    private bool _load;
    private Type? _componentType;
    private bool _settingModalOpened;
    private bool _addScriptModalOpened;

    private readonly List<ScriptNode> _customScriptNodes = new();

    private readonly List<ScriptNodeType> _scriptNodeTypes = Enum.GetValues(typeof(ScriptNodeType)).Cast<ScriptNodeType>().ToList();
    private ScriptNodeType _newScriptNodeType;
    private string _newScriptContent = string.Empty;

    private const string DEFAULT_CODE = """
    <div class="d-flex align-center text-h4">
        <MIcon XLarge Class="mr-2">mdi-arrow-left-bold</MIcon>
        Write code on the left panel!
    </div>
    """;

    private readonly List<TabMonacoModule> _tabMonacoList = new();
    private StringNumber? _tabStringNumber;
    private DotNetObjectReference<Index>? _objRef;
    private bool _initialize;

    [SupplyParameterFromQuery]
    [Parameter]
    public string? Path { get; set; }

    public TabMonacoModule CreateMona { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        _objRef = DotNetObjectReference.Create(this);

        CompileRazorProjectFileSystem.AddGlobalUsing("@using BlazorComponent");
        CompileRazorProjectFileSystem.AddGlobalUsing("@using Masa.Blazor");
        CompileRazorProjectFileSystem.AddGlobalUsing("@using Masa.Blazor.Presets");
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            var code = DEFAULT_CODE;

            if (!string.IsNullOrEmpty(Path))
            {
                try
                {
                    code = await HttpClient.GetStringAsync(Path);
                }
                catch (Exception e)
                {
                    await PopupService.EnqueueSnackbarAsync("Failed to fetch code...", e.Message, AlertTypes.Error);
                }
            }

            RazorCompile.Initialized(await GetReference(), GetRazorExtension());

            _tabMonacoList.Add(new TabMonacoModule()
            {
                Name = "Default.razor",
                Options = GenEditorOptions(code)
            });

            StateHasChanged();

            await TryJSModule.Init();
        }
    }

    [JSInvokable("RunCode")]
    public async void RunCode()
    {
        if (_initialize == false) return;

        _load = true;

        StateHasChanged();

        await Task.Delay(16);

        var monaco = _tabMonacoList[(int)_tabStringNumber];

        var options = new CompileRazorOptions()
        {
            Code = await monaco.MonacoEditor.GetValueAsync(),
            ConcurrentBuild = true
        };

        try
        {
            _componentType = RazorCompile.CompileToType(options);
        }
        catch (Exception e)
        {
            _ = PopupService?.EnqueueSnackbarAsync(e);
        }
        finally
        {
            _load = false;
        }

        StateHasChanged();
    }

    private void Close(TabMonacoModule tabMonacoModule)
    {
        if (_tabMonacoList.Count == 1)
        {
            _ = PopupService.EnqueueSnackbarAsync("Keep at least one", AlertTypes.Error);
            return;
        }

        tabMonacoModule.MonacoEditor.Dispose();
        _tabMonacoList.Remove(tabMonacoModule);
        _tabStringNumber = 0;
    }

    private void CreateFile()
    {
        if (_tabMonacoList.Any(x => x.Name == CreateMona.Name))
        {
            CreateMona = new TabMonacoModule();
            _ = PopupService?.EnqueueSnackbarAsync(@"Duplicate file name", AlertTypes.Error);
            return;
        }

        _tabMonacoList.Add(new TabMonacoModule()
        {
            Name = CreateMona.Name
        });

        CreateMona = new TabMonacoModule();
    }

    private async Task InitCompleteHandle(TabMonacoModule module)
    {
        await InitMonaco(module);

        await module.MonacoEditor.DefineThemeAsync("custom", new StandaloneThemeData()
        {
            Base = "vs",
            inherit = true,
            rules = new TokenThemeRule[] { },
            colors = new Dictionary<string, string>()
            {
                { "editor.background", "#f0f3fa" }
            }
        });

        await module.MonacoEditor.SetThemeAsync("custom");

        await Retry(() =>
        {
            RunCode();
            return Task.CompletedTask;
        }, () =>  _initialize == false);
    }

    private async Task InitMonaco(TabMonacoModule tabMonacoModule)
    {
        // 监听CTRL+S
        await tabMonacoModule.MonacoEditor.AddCommandAsync(2097, _objRef, nameof(RunCode));
    }

    private async Task<List<PortableExecutableReference>?> GetReference()
    {
        var portableExecutableReferences = new List<PortableExecutableReference>();
        if (MasaTrySharedExtension.WebAssembly)
        {
            foreach (var asm in s_assemblies)
            {
                try
                {
                    await using var stream = await HttpClient!.GetStreamAsync($"_framework/{asm}.dll");
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
            foreach (var asm in AssemblyLoadContext.Default.Assemblies)
            {
                try
                {
                    portableExecutableReferences?.Add(MetadataReference.CreateFromFile(asm.Location));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        _initialize = true;

        return portableExecutableReferences;
    }

    private List<RazorExtension> GetRazorExtension()
    {
        var razorExtension = new List<RazorExtension>();

        foreach (var asm in typeof(Index).Assembly.GetReferencedAssemblies())
        {
            razorExtension.Add(new AssemblyExtension(asm.FullName, AppDomain.CurrentDomain.Load(asm.FullName)));
        }

        return razorExtension;
    }

    private object GenEditorOptions(string code)
    {
        return new
        {
            value = code,
            language = "razor",
            theme = "vs",
            automaticLayout = true,
        };
    }

    private async Task AddScriptReferenceAsync()
    {
        var jsScripts = JsNodeRegex().Matches(_newScriptContent);
        foreach (var jsScript in jsScripts.Cast<Match>())
        {
            var newScript = new ScriptNode(jsScript.Value, ScriptNodeType.Js);
            await TryJSModule.AddScript(newScript);
            _customScriptNodes.Add(newScript);
        }

        var cssScripts = CssNodeRegex().Matches(_newScriptContent);
        foreach (var cssScript in cssScripts.Cast<Match>())
        {
            var newScript = new ScriptNode(cssScript.Value, ScriptNodeType.Css);
            await TryJSModule.AddScript(newScript);
            _customScriptNodes.Add(newScript);
        }

        StateHasChanged();
        _addScriptModalOpened = false;
        ClearInputs();
    }

    private void ClearInputs()
    {
        _newScriptContent = string.Empty;
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        _objRef?.Dispose();
    }

    [GeneratedRegex("(<script(.*?)>)(.|\n)*?(</script>)")]
    private static partial Regex JsNodeRegex();

    [GeneratedRegex("(<link(.*?)/>)")]
    private static partial Regex CssNodeRegex();
}
