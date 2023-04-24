# Theme configuration

Easily change the colors of your application programmatically. Rebuild the default stylesheet and customize various aspects of the framework for your particular needs.

## Light and dark

MASA Blazor supports **light** and **dark** themes. By default, your application will use the light theme. To switch to the dark theme, set the enable dark theme when registering the service:

```csharp Program.cs
builder.Services.AddMasaBlazor(options =>
{
    options.ConfigureTheme(theme =>
    {
        theme.Dark = true;
    });
});
```

When you specify a component as light or dark, unless otherwise specified, all of its child components will inherit and apply the same theme.

## Customizing

By default MASA Blazor applies the standard theme to all components.

```csharp
public static class MasaBlazorPreset
{
    private static ThemeOptions LightTheme => new()
    {
        CombinePrefix = ".m-application",
        Primary = "#1976D2",
        Secondary = "#424242",
        Accent = "#82B1FF",
        Error = "#FF5252",
        Info = "#2196F3",
        Success = "#4CAF50",
        Warning = "#FB8C00",
        UserDefined = new Dictionary<string, string>()
    };

    private static ThemeOptions DarkTheme => new()
    {
        CombinePrefix = ".m-application",
        Primary = "#2196F3",
        Secondary = "#424242",
        Accent = "#FF4081",
        Error = "#FF5252",
        Info = "#2196F3",
        Success = "#4CAF50",
        Warning = "#FB8C00",
        UserDefined = new Dictionary<string, string>()
    };
}
```

You can easily change this. Just set the theme properties when registering the service. You can choose to modify all or part of the theme properties, and the rest will inherit from the default values.

```csharp Program.cs
builder.Services.AddMasaBlazor(options =>
{
    options.ConfigureTheme(theme =>
    {
        theme.Themes.Light.Primary = "#4318FF";
        theme.Themes.Light.Secondary = "#A18BFF";
        theme.Themes.Light.Accent = "#005CAF";
        theme.Themes.Light.UserDefined["Tertiary"] = "#e57373";
    });
});
```

## Toggle theme dynamically

Through the `MasaBlazor` service, you can change the theme at runtime.

<masa-example file="Examples.features.theme.DynamicallyModifyTheme"></masa-example>
