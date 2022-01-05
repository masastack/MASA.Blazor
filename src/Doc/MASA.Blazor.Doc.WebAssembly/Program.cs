using MASA.Blazor.Doc;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
await builder.Services.AddMasaI18nForWasm(builder.HostEnvironment.BaseAddress, "_content/MASA.Blazor.Doc/locale/config/languageConfig.json");
builder.RootComponents.Add(typeof(App), "#app", await builder.Services.GetMasaI18nParameter());
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddMasaBlazorDocs(builder.HostEnvironment.BaseAddress);

await builder.Build().RunAsync();