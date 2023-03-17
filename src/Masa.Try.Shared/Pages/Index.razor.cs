using Masa.Blazor;
using Masa.Blazor.Extensions.Languages.Razor;
using Masa.Try.Shared.Pages.Options;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using System.IO;
using System.Runtime.Loader;
using BlazorComponent;

namespace Masa.Try.Shared.Pages;

public partial class Index : NextTickComponentBase
{
    private const string REPOSITORY_URL = "https://github.com/BlazorComponent/Masa.Blazor";

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

    private readonly object _defaultEditorOptions = new
    {
        value = DEFAULT_CODE,
        language = "razor",
        theme = "vs",
        automaticLayout = true,
    };

    public TabMonacoModule CreateMona { get; set; } = new();

    [JSInvokable("RunCode")]
    public async void RunCode()
    {
        if (_initialize == false)
        {
            return;
        }

        _load = true;
        StateHasChanged();

        await Task.Delay(16);

        var monaco = _tabMonacoList[(int)_tabStringNumber];

        var options = new CompileRazorOptions()
        {
            Code = await monaco.MonacoEditor.GetValue(),
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

    protected override async Task OnInitializedAsync()
    {
        _objRef = DotNetObjectReference.Create(this);

        RazorCompile.Initialized(await GetReference(), GetRazorExtension());

        await base.OnInitializedAsync();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            var defaultMonaco = new TabMonacoModule()
            {
                Name = "Masa.razor"
            };

            _tabMonacoList.Add(defaultMonaco);

            await TryJSModule.Init();
        }

        await base.OnAfterRenderAsync(firstRender);
    }

    private async Task InitCompleteHandle(TabMonacoModule module)
    {
        await InitMonaco(module);

        await module.MonacoEditor.DefineTheme("custom", new StandaloneThemeData()
        {
            Base = "vs",
            inherit = true,
            rules = new TokenThemeRule[] { },
            colors = new Dictionary<string, string>()
            {
                { "editor.background", "#f0f3fa" }
            }
        });

        await module.MonacoEditor.SetTheme("custom");

        await NextTickWhile(async () =>
        {
            await Task.Delay(100);
            RunCode();
        }, () =>  _initialize == false);
    }

    private async Task InitMonaco(TabMonacoModule tabMonacoModule)
    {
        // 监听CTRL+S
        await tabMonacoModule.MonacoEditor.AddCommand(2097, _objRef, nameof(RunCode));
    }

    async Task<List<PortableExecutableReference>?> GetReference()
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

    List<RazorExtension> GetRazorExtension()
    {
        var razorExtension = new List<RazorExtension>();

        foreach (var asm in typeof(Index).Assembly.GetReferencedAssemblies())
        {
            razorExtension.Add(new AssemblyExtension(asm.FullName, AppDomain.CurrentDomain.Load(asm.FullName)));
        }

        return razorExtension;
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        _objRef?.Dispose();
    }
}
