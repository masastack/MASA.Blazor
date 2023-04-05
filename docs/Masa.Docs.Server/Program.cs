using Masa.Docs.Core;
using Masa.Docs.Shared;
using Microsoft.AspNetCore.Components.WebAssembly.Services;

var builder = WebApplication.CreateBuilder(args);
// https://github.com/dotnet/aspnetcore/issues/38212
builder.WebHost.UseStaticWebAssets();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor(options =>
{
    options.RootComponents.MaxJSRootComponents = 500;
    options.RootComponents.RegisterCustomElementsUsedJSCustomElementAttribute();
}).AddHubOptions(options => options.MaximumReceiveMessageSize = 64 * 1024);

builder.Services.AddHealthChecks();

builder.Services.AddScoped<LazyAssemblyLoader>();
builder.Services.AddMasaBlazor(options =>
{
    options.ConfigureTheme(theme =>
    {
        theme.Themes.Light.Primary = "#4318FF";
        theme.Themes.Light.Secondary = "#5CBBF6";
        theme.Themes.Light.Accent = "#005CAF";
        theme.Themes.Light.UserDefined?.Add("Tertiary", "#e57373");
    });
}).AddI18nForServer("wwwroot/locale");

var docSeverUrlConfig = builder.Configuration["ASPNETCORE_URLS"];
var docSeverUrls = docSeverUrlConfig?.Split(";", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
docSeverUrls = docSeverUrls?.Select(x => x.Replace("0.0.0.0", "127.0.0.1")).ToArray();
var docSeverUrl = docSeverUrls?.FirstOrDefault(x => x.StartsWith("https://")) ??
                  docSeverUrls?.FirstOrDefault(x => x.StartsWith("http://"));

builder.Services.AddMasaDocs(docSeverUrl ?? "http://localhost:5000");

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.MapHealthChecks("/healthz");

app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
