using BlazorComponent;
using Masa.Blazor.Extensions.Languages.Razor;
using Masa.Blazor.Presets;
using Masa.Try.Shared.Pages.Options;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.CodeAnalysis;
using Microsoft.JSInterop;

namespace Masa.Try.Shared.Pages;

public partial class Index : IDisposable
{
    private Type? componentType;

    private bool load;

    private string url = "https://github.com/BlazorComponent/Masa.Blazor";
    
    private static string Code = @"<body>
    <div id='app'>
        <header>
            <h1>Doctor Who&trade; Episode Database</h1>
        </header>

        <nav>
            <a href='main-list'>Main Episode List</a>
            <a href='search'>Search</a>
            <a href='new'>Add Episode</a>
        </nav>

        <h2>Episodes</h2>

    </div>
</body>";

    /// <summary>
    /// 编译器需要使用的程序集
    /// </summary>
    private static List<string> Assemblys = new()
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

    public List<TabMonacoModule> TabMonacoList { get; set; } = new();

    public StringNumber TabStringNumber { get; set; }

    private PEnqueuedSnackbars? _enqueuedSnackbars;

    public bool createPModal { get; set; }
    
    public TabMonacoModule CreateMona { get; set; } = new();
    
    private bool initialize = false;

    private bool drawer = true;
    
    private object Options = new
    {
        value = Code,
        language = "razor",
        theme = "vs-dark",
        automaticLayout = true,
    };

    private DotNetObjectReference<Index> _objRef; 

    [JSInvokable("RunCode")]
    public async void RunCode()
    {
        if (initialize == false)
        {
            return;
        }

        load = true;
        StateHasChanged();
        await Task.Delay(10);

        var _monaco = TabMonacoList[(int)TabStringNumber];
        
        var options = new CompileRazorOptions()
        {
            Code = await _monaco.MonacoEditor.GetValue(),
            ConcurrentBuild = true
        };

        componentType = RazorCompile.CompileToType(options);
        load = false;
        StateHasChanged();
    }

    private void Close(TabMonacoModule tabMonacoModule)
    {
        if (TabMonacoList.Count == 1)
        {
            _enqueuedSnackbars?.EnqueueSnackbar(new SnackbarOptions()
            {
                Content = "Keep at least one",
                Type = AlertTypes.Error,
            });
            return;
        }
        tabMonacoModule.MonacoEditor.Dispose();
        TabMonacoList.Remove(tabMonacoModule);
        TabStringNumber = 0;
    }
    
    private void CreateFile()
    {
        createPModal = false;
        if (TabMonacoList.Any(x => x.Name == CreateMona.Name))
        {
            CreateMona = new TabMonacoModule();
            _enqueuedSnackbars?.EnqueueSnackbar(new SnackbarOptions()
            {
                Content = "Duplicate file name",
                Type = AlertTypes.Error,
            });
            return;
        }
        TabMonacoList.Add(new TabMonacoModule()
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
            TabMonacoList.Add(new TabMonacoModule()
            {
                Name = "Masa.razor"
            });
            await TryJSModule.Init();
        }
        await base.OnAfterRenderAsync(firstRender);
    }

    private async Task InitMonaco(TabMonacoModule tabMonacoModule)
    {
        // 监听CTRL+S
        await tabMonacoModule.MonacoEditor.AddCommand(2097, _objRef, nameof(RunCode));
    }

    async Task<List<PortableExecutableReference>?> GetReference()
    {
        var portableExecutableReferences = new List<PortableExecutableReference>();
        foreach (var asm in Assemblys)
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

        initialize = true;

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

    public void Dispose()
    {
        _objRef.Dispose();
    }
}
