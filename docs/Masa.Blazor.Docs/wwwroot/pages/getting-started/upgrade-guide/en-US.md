# Upgrade Guides

## Upgrading from v0.6.x to v1.0.x

v1.0.x contains non backwards compatible breaking changes, the following changes:

### Features

#### I18n

Removed the way of setting the default locale through `$DefaultCulture`. Instead, supply the `Locale` option when calling `AddMasaBlazor` in _Program.cs_.

```csharp
services.AddMasaBlazor(options =>
{
    options.Locale = new Locale("zh-CN", "en-US");
});
```

### Components

#### MBreadcrumbs

The `Linkage` is renamed to `Routable`.

```diff
- <MBreadcrumbs Linkage></MBreadcrumbs>
+ <MBreadcrumbs Routable></MBreadcrumbs>
```

#### MButton

The parameter `StopPropgation` is renamed to `OnClickStopPropagation`.

```diff
- <MButton StopPropagation></MButton>
+ <MButton OnClickStopPropagation></MButton>
 ```

#### MCheckbox/MSwitch

The `Value` is no longer a `bool`, but generic type `TValue`. For words that do not use `@bind-Value`, you need to specify the generic type explicitly.

```diff
- <MCheckbox Value="" ValueChanged=""></MCheckbox>
+ <MCheckbox Value="" ValueChanged="" TValue="bool"></MCheckbox>
```

#### MDataTable

- The `Align` type of DataTableHeader changes from `string` to enum.

  ```diff
  - Align = "start"
  + Align = DataTableHeaderAlign.Start
  ```

- The type of `ItemClass` was changed from `string` to `Func<TItem, string>`.

#### MErrorHandler

- The `ShowAlert` parameter is removed, uses `PopupType` instead
- The `OnErrorHandleAsync` parameter is removed, use `OnHandle` instead. `OnHandle` would overrides the default error handler. Use `OnAfterHandle` if you only want to do something else after handling an error.

```diff
- <MErrorHandler ShowAlert="false"></MErrorHandler>
+ <MErrorHandler PopupType="ErrorPopupType.None" OnHandle="" OnAfterHandle=""></MErrorHandler>
```

#### MHover

Removed the `Class` and `Style` properties of `Context`.

#### MIcon

Introduces the concept of the default icon set. Now if the icon is not the default icon set, you need to specify the prefix of icon set.

```diff
- <MIcon>home</MIcon>
+ <MIcon>md:home</MIcon>
- <MIcon>fas fa-home</MIcon>
+ <MIcon>fa:fas fa-home</MIcon>
```

#### MInfiniteScroll

Now no longer needs to set the loading state through the additional `HasMore` parameter, but through the `Status` of the `OnLoad` event parameter to control it. Also, the component will automatically trigger the `OnLoad` event when it is first rendered.

```diff
 <MInfiniteScroll
-    HasMore="hasMore"
-    OnLoadMore="OnLoad"
+    OnLoad="OnLoad"
 >
 </MInfiniteScroll>
 @code {
-    private bool hasMore;
-    private async Task OnLoad() {
-        var items = await Request();
-        hasMore = items.Count > 0;
-    }
+    private async Task OnLoad(InfiniteScrollLoadEventArgs args) {
+        var items = await Request();
+        args.Status = items.Count > 0 ? InfiniteScrollStatus.HasMore : InfiniteScrollStatus.NoMore;
+    }
 }
```

#### MList

The `Linkage` is renamed to `Routable`.

```diff
- <MList Linkage></MList>
+ <MList Routable></MList>
```

#### MNavigationDrawer

The type of `Value` is changed to `bool?`, and the type of `ValueChanged` is changed to `EventCallback<bool?>`. If you don't have special requirements, it is recommended to set the `Value` value to `null`. When the `Value` value is `null`, the component will automatically determine whether to display the drawer according to the screen width.

#### MOverlay

A new parameter `Contained` is used to replace the previous parameter `Absolute`.

```diff
- <MOverlay Absolute></MOverlay>
+ <MOverlay Contained></MOverlay>
```

#### MPageTabs

Refactored to the preset component **PPageTabs**. There are many design and API changes, please refer to the [document](/blazor/components/page-tabs) for details.

#### PopupService

Removed `AlertAsync` and `ToastAsync`, use `EnqueueSnackbarAsync` instead.

```diff
- PopupService.AlertAsync()
- PopupService.ToastAsync()
+ PopupService.EnqueueSnackbarAsync()
```

#### PConfirm

The component has been removed, use the `IPopupService` service's `ConfirmAsync` method instead.

#### PToasts

The component has been removed, use the **PEnqueuedSnackbars** component or `IPopupService` service's `EnqueueSnackbarAsync` method instead.

## Upgrading from v0.5.x to v0.6.x

v0.6.x contains non backwards compatible breaking changes, the following changes:

### Themes

```diff
 services.AddMasaBlazor(options =>
 {
-    options.DarkTheme = true;
-    options.UseTheme(theme =>
-    {
-        theme.Primary = "XXX";
-    });
+    options.ConfigureTheme(theme =>
+    {
+        theme.Dark = true;
+        theme.Themes.Light.Primary= "XXX";
+        theme.Themes.Dark.Primary= "XXX"; // support for configure the preset of Dark theme
+    });
 })
```

### Components

- **MForm**: `Validate`, `Reset` and `ResetValidation` are change to synchronous methods. Change the context type of `ChildContent` from `EditContext` to `FormContext`.

## Upgrading from v0.4.x to v0.5.x

v0.5.x contains non backwards compatible breaking changes, the following changes:

### CSS

Use `masa-blazor.min.css` instead of `masa-blazor.css` and `masa-extend-blazor.css`.

### I18n

```diff
- services.AddMasaBlazor();
- services.AddMasaI18nForServer();
- services.AddMasaI18nForWasmAsync(); // in WASM
+ services.AddMasaBlazor().AddI18nForServer();
+ await services.AddMasaBlazor().AddI18nForWasmAsync(); // in WASM
```

And rename the **languageConfig.json** file to **supportedCultures.json** at the same time.
