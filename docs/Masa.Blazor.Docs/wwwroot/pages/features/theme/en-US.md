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

## Customizing {updated-in=v1.3.2}

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
        Surface = "#FFFFFF",
        OnPrimary = "#FFFFFF",
        OnSecondary = "#FFFFFF",
        OnAccent = "#FFFFFF",
        OnSurface = "#000000DE",
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
        Surface = "#121212",
        OnPrimary = "#000000",
        OnSecondary = "#000000",
        OnAccent = "#000000",
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
        theme.Themes.Light.Primary = "#4f33ff";
        theme.Themes.Light.Secondary = "#5e5c71";
        theme.Themes.Light.Accent = "#006C4F";
        theme.Themes.Light.Error = "#BA1A1A";
        theme.Themes.Light.OnSurface = "#1C1B1F";

        theme.Themes.Dark.Primary = "#C5C0FF";
        theme.Themes.Dark.Secondary = "#C7C4DC";
        theme.Themes.Dark.Accent = "#67DBAF";
        theme.Themes.Dark.Error = "#FFB4AB";
        theme.Themes.Dark.Surface = "#131316";
        theme.Themes.Dark.OnPrimary = "#2400A2";
        theme.Themes.Dark.OnSecondary = "#302E42";
        theme.Themes.Dark.OnAccent = "#003827";
        theme.Themes.Dark.OnSurface = "#C9C5CA";
    });
});
```

The CSS of the theme style will automatically generate a `<style>` tag with an `id` of `masa-blazor-theme-stylesheet` and insert it into the `<head>` tag when the application starts.

## Toggle theme dynamically

Through the `MasaBlazor` service, you can change the theme at runtime.

<masa-example file="Examples.features.theme.DynamicallyModifyTheme"></masa-example>

## Avoid flicker {released-on=v1.7.0}

When the application starts, the custom theme will be dynamically generated and applied to the application through JavaScript.This may cause a brief flicker when the application is loaded.
To avoid this, you can add the **MAppThemeStylesheet** component in the `head` tag to apply the theme in advance.

:::: code-group
::: code-group-item Blazor Server
```razor _Host.cshtml
<head>
    <!-- Other content -->
    <component type="typeof(MAppThemeStylesheet)" render-mode="ServerPrerendered" />
</head>
```
:::
::: code-group-item Blazor Web App
``` razor App.razor
<head>
    <!-- Other content -->
    <MAppThemeStylesheet />
</head>
```
:::
::::
