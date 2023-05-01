using BlazorComponent;
using Masa.Blazor;
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
        theme.Themes.Light.Secondary = "#A18BFF";
        theme.Themes.Light.Accent = "#005CAF";
        theme.Themes.Light.UserDefined["Tertiary"] = "#e57373";
    });
    options.ConfigureIcons(IconSet.MaterialDesignIcons, aliases =>
    {
        aliases.UserDefined["masaBlazor"] = new SvgPath[]
        {
            new("M3 6.375H6.375V9.75H3V6.375Z"),
            new("M3 13.125H6.375V16.5H3V13.125Z"),
            new("M15.9375 3H6.375V6.375H15.9375C16.8695 6.375 17.625 7.13052 17.625 8.0625C17.625 8.99448 16.8695 9.75 15.9375 9.75H6.375V13.125H15.9375C18.7334 13.125 21 10.8584 21 8.0625C21 5.26656 18.7334 3 15.9375 3Z"),
            new("M15.9375 9.75H6.375V13.125H15.9375C16.8695 13.125 17.625 13.8805 17.625 14.8125C17.625 15.7445 16.8695 16.5 15.9375 16.5H6.375V19.875H15.9375C18.7334 19.875 21 17.6084 21 14.8125C21 12.0166 18.7334 9.75 15.9375 9.75Z")
        };
    });
}).AddI18nForWasmAsync($"{builder.HostEnvironment.BaseAddress}/_content/Masa.Docs.Shared/locale");

builder.Services.AddMasaDocs(builder.HostEnvironment.BaseAddress, BlazorMode.Wasm);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.RootComponents.RegisterCustomElementsUsedJSCustomElementAttribute();

await builder.Build().RunAsync();
