using Masa.Blazor.Doc;
using Masa.Blazor.Doc.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

await builder.Services
             .AddMasaBlazor(options =>
             {
                 options.ConfigureTheme(theme =>
                 {
                     theme.Themes.Light.Secondary = "#5CBBF6";
                     theme.Themes.Light.Accent = "#005CAF";
                     theme.Themes.Light.UserDefined["Tertiary"] = "#e57373";
                 });
             })
             .AddI18nForWasmAsync($"{builder.HostEnvironment.BaseAddress}/_content/Masa.Blazor.Doc/locale");

builder.RootComponents.Add(typeof(App), "#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddMasaBlazorDocs(builder.HostEnvironment.BaseAddress, BlazorMode.Wasm);

await builder.Build().RunAsync();
