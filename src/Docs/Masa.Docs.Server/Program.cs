using Microsoft.AspNetCore.Components.WebAssembly.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddScoped<LazyAssemblyLoader>();
builder.Services.AddMasaBlazor(options =>
{
    options.ConfigureTheme(theme =>
    {
        theme.Themes.Light.Secondary = "#5CBBF6";
        theme.Themes.Light.Accent = "#005CAF";
        theme.Themes.Light.UserDefined["Tertiary"] = "#e57373";
    });
});

// TODO: add i18n for server

// TODO: add MasaBlazorDocs

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

// TODO: crawlService

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
