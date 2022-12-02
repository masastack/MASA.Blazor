# 升级指南

## 从 v0.5.x 升级到 v0.6.x

v0.6.x 包含了不兼容的破坏性更改，包括以下变更：

### 配置主题色的API表更

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

引入 `masa-blazor.css` 和 `masa-extend-blazor.css `更改为引入 `masa-blazor.min.css`。

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
