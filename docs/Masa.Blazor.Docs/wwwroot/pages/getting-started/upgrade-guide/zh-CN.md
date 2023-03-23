# 升级指南

## 从 v0.6.x 升级到 v1.0.x

v1.0.x 包含了不兼容的破坏性更改，包括以下变更：

- `Linkage` 重命名为 `Routable`，受影响的组件有 **MBreadcrumbs** 和 **MList**：
  ```diff
  - <MBreadcrumbs Linkage></MBreadcrumbs>
  + <MBreadcrumbs Routable></MBreadcrumbs>
  - <MList Linkage></MList>
  + <MList Routable></MList>
  ```
- **MCheckBox/MSwitch** 的 `Value` 类型不再是 `bool` 类型，而是泛型 `TValue`。对于没有使用 `@bind-Value` 的写法需要显式指定泛型类型：
  ```diff
  - <MCheckbox Value="" ValueChanged=""></MCheckbox>
  + <MCheckbox Value="" ValueChanged="" TValue="bool"></MCheckbox>
  ```
- **MDataTable** DataTableHeader 的 `Align` 类型从 `string` 变为枚举：
  ```diff
  - Align = "start"
  + Align = DataTableHeaderAlign.Start
  ```
- **PConfirm** 已被移除，请使用 `IPopupService.ConfirmAsync`。
- **MHover** 移除了 `Context` 的 `Class` 和 `Style` 属性。
- **MErrorHandler** 移除了 `ShowAlert` 参数，使用 `PopupType` 代替；移除了 `OnErrorHandleAsync` 参数，请使用 `OnHandle` 代替。`OnHandle` 会覆盖默认的错误处理程序，如果你只想在处理错误之后做其他操作，请使用 `OnAfterHandle`。
  ```diff
  - <MErrorHandler ShowAlert="false" OnErrorHandleAsync=""></MErrorHandler>
  + <MErrorHandler PopupType="ErrorPopupType.None" OnHandle="" OnAfterHandle=""></MErrorHandler>
  ```
- 移除了 **PToasts** 组件，请使用 **PEnqueuedSnackbars** 组件代替。
- **PopupService** 移除了 `AlertAsync` 和 `ToastAsync`，请使用 `EnqueueSnackbarAsync` 代替。
  ```diff
  - PopupService.AlertAsync()
  - PopupService.ToastAsync()
  + PopupService.EnqueueSnackbarAsync()
  ```

## 从 v0.5.x 升级到 v0.6.x

v0.6.x 包含了不兼容的破坏性更改，包括以下变更：

### 配置主题色的API变更

v0.5.x的API:

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

更改为：

```csharp
services.AddMasaBlazor(options =>
{
   options.ConfigureTheme(theme =>
   {
       theme.Dark = true;
       theme.Themes.Light.Primary= "XXX";
       theme.Themes.Dark.Primary= "XXX"; // 支持配置暗主题的预设颜色
   });
})
```

### 表单（MForm）组件的API变更

- **Validate**，**Reset** 和 **ResetValidation** 更改为同步方法。
- 将 `ChildContent` 的上下文类型从 **EditContext** 改为 **FormContext**。

## 从 v0.4.x 升级到 v0.5.x

v0.5.x 包含了不兼容的破坏性更改，包括以下变更：

### CSS

引入 `masa-blazor.css` 和 `masa-extend-blazor.css` 更改为引入 `masa-blazor.min.css`。

### 注入自定义i18n的API变更

v0.4.x的API：

```csharp
services.AddMasaBlazor();
services.AddMasaI18nForServer();
services.AddMasaI18nForWasmAsync(); // in WASM
```


更改为：

```csharp
services.AddMasaBlazor().AddI18nForServer();
await services.AddMasaBlazor().AddI18nForWasmAsync(); // in WASM
```

同时把 **languageConfig.json** 文件重命名为 **supportedCultures.json**。
