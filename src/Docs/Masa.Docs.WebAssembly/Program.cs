using Masa.Docs.Core;
using Masa.Docs.Shared;
using Masa.Docs.Shared.Models;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.WebAssembly.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped<LazyAssemblyLoader>();

await builder.Services.AddMasaBlazor(options =>
{
    options.ConfigureTheme(theme =>
    {
        theme.Themes.Light.Primary = "#4318FF";
        theme.Themes.Light.Secondary = "#5CBBF6";
        theme.Themes.Light.Accent = "#005CAF";
        theme.Themes.Light.UserDefined["Tertiary"] = "#e57373";
    });
}).AddI18nForWasmAsync($"{builder.HostEnvironment.BaseAddress}/_content/Masa.Docs.Shared/locale");

builder.Services.AddMasaDocs(builder.HostEnvironment.BaseAddress, BlazorMode.Wasm);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.RootComponents.RegisterCustomElementsUsedJSCustomElementAttribute();

await builder.Build().RunAsync();
