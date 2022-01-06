using MASA.Blazor.Doc;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
await builder.Services.AddMasaI18nForWasmAsync($"{builder.HostEnvironment.BaseAddress}/_content/MASA.Blazor.Doc/locale");
builder.RootComponents.Add(typeof(App), "#app", await builder.Services.GetMasaI18nParameterAsync());
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddMasaBlazorDocs(builder.HostEnvironment.BaseAddress);

await builder.Build().RunAsync();