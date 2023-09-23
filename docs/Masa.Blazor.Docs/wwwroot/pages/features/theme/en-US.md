# Theme configuration

Easily change the colors of your application programmatically. Rebuild the default stylesheet and customize various aspects of the framework for your particular needs.

## Custom themes

The standard theme is applied to all components by **MASA Blazor**, or you can customize the theme when you sign up for the service, and undefined properties inherit the default values.

```csharp Program.cs
builder.Services.AddMasaBlazor(options =>
{
    options.Dark = true;
    options.ConfigureTheme(theme =>
    {
        theme.LightPalette.Primary = "#4f33ff";
        theme.LightPalette.Secondary = "#C7C4DC";
        theme.LightPalette.Error = "#ba1a1a";
        theme.LightPalette.UserDefined["Tertiary"] = "#00966f";
        theme.DarkPalette.Primary = "#c5c0ff";
    });
});
```

## Change theme dynamically

You can switch between the `Light` and `Dark` themes at runtime through the `IThemeService` service.

When you specify a component as light or dark, all of its child components inherit and apply the same value unless otherwise noted.

<masa-example file="Examples.features.theme.ChangeThemeMode"></masa-example>

You can also modify colors dynamically at runtime.

<masa-example file="Examples.features.theme.ChangeThemeColor"></masa-example>

## Advanced usage

You can use the `MThemeProvider` component to conveniently set up themes, such as using different color schemes for each page.

<masa-example file="Examples.features.theme.SinglePageCustomStyles"></masa-example>

## Change style position

By default, we generate `style` elements inside `body`

In some cases, it may be necessary to move the `style` to `head` for specification and security. You can manually add the `MThemeProvider` component in `head`, at which point we won't generate a default style in `body`.

```html Pages/_Host.cshtml
    <component type="typeof(MThemeProvider)" render-mode="ServerPrerendered" />
```