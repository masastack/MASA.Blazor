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

await builder.Services.AddMasaDocs(builder.HostEnvironment.BaseAddress, BlazorMode.Wasm)
             .AddI18nForWasmAsync($"{builder.HostEnvironment.BaseAddress}/_content/Masa.Docs.Shared/locale");

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.RootComponents.RegisterCustomElementsUsedJSCustomElementAttribute();

await builder.Build().RunAsync();
