# 升级指南

## 从 v0.6.x 升级到 v1.0.x

v1.0.x 包含了不兼容的破坏性更改，包括以下变更：

- **Breadcrumbs** 和 **List(包括ListItem)** 的 `Linkage` 参数更改为 `Routable`。
- **CheckBox/Switch**的 `Value` 类型不再是 `bool` 类型，而是泛型类型。可以设置 `TrueValue` 和 `FalseValue`。升级到 **1.0.0** 后，需要添加参数 `TValue=“bool”`。
- **DataTable** header 的 `Align` 类型从 `string` 变为枚举。
- **PConfirm**已被移除，请改用 `PopupService.Confirm`。
- 清理了 **MHover** 的 `Context`：删除了 `Class` 和 `Style` 属性。

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
