# Theme configuration

MASA Blazor follows the theme configuration specification of [Material Design 3](https://m3.material.io/styles/color/roles), allowing you to customize the default text color, surface, and other roles of your application.

## Setup {updated-in=v1.10.0}

MASA Blazor includes two preset themes, **light** and **dark**, which you can find defined in the [Github](https://github.com/masastack/MASA.Blazor/blob/main/src/Masa.Blazor/Services/MasaBlazorPreset.cs) source code.

To set the default theme for your application, use the `DefaultTheme` option.

```csharp Program.cs
builder.Services.AddMasaBlazor(options =>
{
    options.ConfigureTheme(theme =>
    {
        theme.DefaultTheme = "dark";
    });
});
```

## Customizing {updated-in=v1.10.0}

When registering the service, use the `ConfigureTheme` method to modify the preset **light** and **dark** themes, or use the `Add` method to add new themes.
You can also modify the CSS variables used by the theme through the `ThemeOptions.Variables` property.

For example, in this document's theme settings:
- The values of certain color roles in the built-in **light** and **dark** themes have been modified, and a custom color role named **basil** has been added (you can see its usage in the [Grow](/blazor/components/tabs#grow) example).
- Finally, a custom theme named **camel** with a camel color scheme has been added; you can create your own theme using the [Material Theme Builder](https://www.figma.com/community/plugin/1034969338659738588/material-theme-builder).
```csharp
services.AddMasaBlazor(options =>
{
    options.ConfigureTheme(theme =>
    {
        theme.Themes.Light.Primary = "#4f33ff";
        theme.Themes.Light.UserDefined["basil"] = new ColorPairing("#FFFBE6", "#356859");

        theme.Themes.Dark.Primary = "#C5C0FF";
        theme.Themes.Dark.UserDefined["basil"] = new ColorPairing("#FFFBE6", "#356859");
        
        // example: Modify the opacity of the outline variant 
        // in the dark theme, commonly used for dividers
        // theme.Themes.Dark.Variables.BorderOpacity = 0.2f;
        
        // ...

        theme.Themes.Add("camel", true, custom =>
        {
            custom.Primary = "#ffb68a";
            custom.OnPrimary = "#522300";
            custom.Secondary = "#e5bfa9";
            custom.OnSecondary = "#432b1c";
            custom.Accent = "#cbc992";
            custom.OnAccent = "#333209";
            custom.Error = "#ffb4ab";
            custom.OnError = "#690005";
            custom.Surface = "#1a120d";
            custom.OnSurface = "#f0dfd7";
            custom.SurfaceDim = "#1a120d";
            custom.SurfaceBright = "#413732";
            custom.SurfaceContainer = "#261e19";
            custom.SurfaceContainerLow = "#221a15";
            custom.SurfaceContainerLowest = "#140d08";
            custom.SurfaceContainerHigh = "#312823";
            custom.SurfaceContainerHighest = "#3d332d";
            custom.InversePrimary = "#8c4f26";
            custom.InverseSurface = "#f0dfd7";
            custom.InverseOnSurface = "#382e29";
        });
    }
});
```

The CSS of the theme style will automatically generate a `<style>` tag with an `id` of `masa-blazor-theme-stylesheet` and insert it into the `<head>` tag when the application starts.

## Change theme

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
