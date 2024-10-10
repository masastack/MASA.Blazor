using System.Text.Json;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor(opts => opts.DetailedErrors = true);
builder.Services.AddMasaBlazor();
builder.Services.AddHttpClient();

builder.Services.AddSingleton<GraphQLHttpClient>(_ => new GraphQLHttpClient("http://localhost:5297/graphql",
    new SystemTextJsonSerializer(new JsonSerializerOptions(JsonSerializerDefaults.Web))));

builder.WebHost.UseSetting(WebHostDefaults.DetailedErrorsKey, "true");

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