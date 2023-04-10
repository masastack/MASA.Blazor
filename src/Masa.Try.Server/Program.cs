var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMasaTryShared();
builder.Services.AddMasaBlazor().AddI18nForServer("wwwroot/i18n");

builder.Services.AddScoped(sp => new HttpClient
    { BaseAddress = new Uri(builder.Configuration["ASPNETCORE_URLS"]?.Replace("0.0.0.0", "127.0.0.1") ?? "http://localhost:5250") });

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
