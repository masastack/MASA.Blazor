using MASA.Blazor.Doc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
await builder.Services.AddMasaI18nForWasm(builder.HostEnvironment.BaseAddress, "_content/MASA.Blazor.Doc/locale/languageConfig.json");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddMasaBlazorDocs(builder.HostEnvironment.BaseAddress);

var host = builder.Build();
builder.RootComponents.Add(typeof(App), "#app", host.Services.GetMasaI18nParameter());
await host.Services.UseMasaI18nForWasm();
await host.RunAsync();