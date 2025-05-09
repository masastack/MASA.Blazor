# 主题配置 {#theme-configuration}

MASA Blazor 遵循 [Material Design 3](https://m3.material.io/styles/color/roles) 的主题配置规范，允许您自定义应用程序的默认文本颜色、表面等角色。

## 设置 {#setup updated-in=v1.10.0}

MASA Blazor 包含两种预置的主题，**light** 和 **dark**，你可以在 [Github](https://github.com/masastack/MASA.Blazor/blob/main/src/Masa.Blazor/Services/MasaBlazorPreset.cs) 源码里找到它们的定义。

要设置应用程序的默认主题，请使用 `DefaultTheme` 选项。

```csharp Program.cs
builder.Services.AddMasaBlazor(options =>
{
    options.ConfigureTheme(theme =>
    {
        theme.DefaultTheme = "dark";
    });
});
```

## 自定义主题 {#customizing updated-in=v1.10.0}

在注册服务时使用 `ConfigureTheme` 方法修改预置的 **light** 和 **dark** 主题，还可以使用 `Add` 方法添加新的主题。
另外可以通过 `ThemeOptions.Variables` 属性修改供主题使用的 CSS 变量。

以本文档的主题设置为例：
- 修改了内置的 **light**和 **dark** 中某些颜色角色的值，并添加了一个名为 **basil** 的自定义颜色角色（你可以在示例 [Grow](/blazor/components/tabs#grow) 中看到它的使用）。
- 最后添加了一个名为 **camel** 的以骆驼色为主的自定义主题；你可以通过 [Material Theme Builder](https://www.figma.com/community/plugin/1034969338659738588/material-theme-builder) 创建自己的主题。

```csharp
services.AddMasaBlazor(options =>
{
    options.ConfigureTheme(theme =>
    {
        theme.Themes.Light.Primary = "#4f33ff";
        theme.Themes.Light.UserDefined["basil"] = new ColorPairing("#FFFBE6", "#356859");

        theme.Themes.Dark.Primary = "#C5C0FF";
        theme.Themes.Dark.OnPrimary = "#000000";
        theme.Themes.Dark.UserDefined["basil"] = new ColorPairing("#FFFBE6", "#356859");

        // 例：修改暗黑主题下outline变体的透明度，常用用于分割线
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

主题样式的 CSS 会在应用启动时自动生成一个 `id` 为 `masa-blazor-theme-stylesheet` 的 `<style>` 标签，并插入到 `<head>` 标签中。

## 动态更改主题 {#change-theme}

通过 `MasaBlazor` 服务，您可以在运行时更改主题。

<masa-example file="Examples.features.theme.DynamicallyModifyTheme"></masa-example>

## 避免短暂闪烁 {#avoid-flicker released-on=v1.7.0}

自定义主题会通过 JavaScript 动态生成并应用到应用程序中。这可能会导致在加载应用程序时出现短暂地闪烁。为了避免这种情况,您可以在 `head` 标签中添加 **MAppThemeStylesheet** 组件提前应用主题。

:::: code-group
::: code-group-item Blazor Server
```razor _Host.cshtml
<head>
    <!-- 其他内容 -->
    <component type="typeof(MAppThemeStylesheet)" render-mode="ServerPrerendered" />
</head>
```
:::
::: code-group-item Blazor Web App
``` razor App.razor
<head>
    <!-- 其他内容 -->
    <MAppThemeStylesheet />
</head>
```
:::
::::
