using BlazorComponent.Components;
using MASA.Blazor.Doc;
using MASA.Blazor.Doc.Utils;
using MASA.Blazor.Doc.WebAssembly;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Globalization;
using System.Net.Http.Json;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var httpClient = new HttpClient()
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
};

builder.Services.AddScoped(sp => httpClient);

#region i18n settings

await I18nHelper.GetLocalesAndAddLang(httpClient);

builder.Services.AddScoped<I18n>();

#endregion

builder.Services.AddMasaBlazorDocs();

await builder.Build().RunAsync();
