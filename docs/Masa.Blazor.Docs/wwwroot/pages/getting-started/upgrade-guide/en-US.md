# Upgrade Guides

## Upgrading from v0.6.x to v1.0.x

v1.0.x contains non backwards compatible breaking changes, the following changes:

- The Linkage` is renamed to `Routable`, and the affected components are **MBreadcrumbs** and **MList**:
  ```diff
  - <MBreadcrumbs Linkage></MBreadcrumbs>
  + <MBreadcrumbs Routable></MBreadcrumbs>
  - <MList Linkage></MList>
  + <MList Routable></MList>
  ```
- The `Value` type of **MCheckbox/MSwitch** is no longer a `bool`, but generic type `TValue`. For words that do not use `@bind-Value`, you need to specify the generic type explicitly:
  ```diff
  - <MCheckbox Value="" ValueChanged=""></MCheckbox>
  + <MCheckbox Value="" ValueChanged="" TValue="bool"></MCheckbox>
  ```
- The `Align` type of the **MDataTable** DataTableHeader changes from `string` to enum:
  ```diff
  - Align = "start"
  + Align = DataTableHeaderAlign.Start
  ```
- The **PConfirm** has been removed now, use `IPopupService.ConfirmAsync` instead.
- **MHover** removes the `Class` and `Style` properties of `Context`.
- **MErrorHandler** removes the parameter `ShowAlert`, uses `PopupType` instead:
  ```diff
  - <MErrorHandler ShowAlert="false"></MErrorHandler>
  + <MErrorHandler PopupType="ErrorPopupType.None"></MErrorHandler>
  ```

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
