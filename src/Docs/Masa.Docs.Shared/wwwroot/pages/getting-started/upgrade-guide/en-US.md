# pgrade Guide

## Upgrading from v0.5.x to v0.6.x

v0.6.x contains non backwards compatible breaking changes, the following changes:

### API changes for configuring themes

The old api in v0.5.x:

```csharp
services.AddMasaBlazor(options =>
{
    options.DarkTheme = true;
    options.UseTheme(theme =>
    {
        theme.Primary = "XXX";
    });
});
```

Change to:

```csharp
services.AddMasaBlazor(options =>
{
   options.ConfigureTheme(theme =>
   {
       theme.Dark = true;
       theme.Themes.Light.Primary= "XXX";
       theme.Themes.Dark.Primary= "XXX"; // support for configure the preset of Dark theme
   });
})
```

### API changes for the Form(MForm) component

- **Validate**, **Reset** and **ResetValidation** are change to synchronous methods.
- Change the context type of `ChildContent` from **EditContext** to **FormContext**.

## Upgrading from v0.4.x to v0.5.x

v0.5.x contains non backwards compatible breaking changes, the following changes:

### CSS

Use `masa-blazor.min.css` instead of `masa-blazor.css` and `masa-extend-blazor.css`.

### API changes for adding custom i18n

The old api in v0.4.x:

```csharp
services.AddMasaBlazor();
services.AddMasaI18nForServer();
services.AddMasaI18nForWasmAsync(); // in WASM
```

Change to：

```csharp
services.AddMasaBlazor().AddI18nForServer();
await services.AddMasaBlazor().AddI18nForWasmAsync(); // in WASM
```

And rename the **languageConfig.json** file to **supportedCultures.json** at the same time.
