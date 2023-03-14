using Masa.Blazor;
using Masa.Blazor.Extensions.Languages.Razor;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.CodeAnalysis;
using Microsoft.JSInterop;

namespace Masa.Try.Shared.Pages;

public partial class Index : IDisposable
{
    private Type? componentType;

    private MMonacoEditor _monaco;

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

    private bool initialize = false;

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

        var options = new CompileRazorOptions()
        {
            Code = await _monaco.GetValue(),
            ConcurrentBuild = true
        };

        componentType = RazorCompile.CompileToType(options);
        StateHasChanged();
    }

    protected override async Task OnInitializedAsync()
    {

        _objRef = DotNetObjectReference.Create(this);

        RazorCompile.Initialized(await GetReference(), GetRazorExtension());

        await base.OnInitializedAsync();
    }

    private async Task InitMonaco()
    {
        // 监听CTRL+S
        await _monaco.AddCommand(2097, _objRef, nameof(RunCode));
    }

    async Task<List<PortableExecutableReference>?> GetReference()
    {
        var portableExecutableReferences = new List<PortableExecutableReference>();
        foreach (var asm in Assemblys)
        {
            try
            {
                using var stream = await HttpClient!.GetStreamAsync($"_framework/{asm}.dll");
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
        DotNetObject.Dispose();
    }
}
