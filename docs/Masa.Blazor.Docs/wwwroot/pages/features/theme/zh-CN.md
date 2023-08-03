# 主题配置

轻松地通过编程的方式改变应用程序的颜色。重新构建默认样式表并根据您的特定需求自定义框架的各个方面。

## 浅色和深色

MASA Blazor 支持**浅色**和**深色**主题。默认情况下，您的应用程序将使用浅色主题。要切换到深色主题，请在注册服务时设置启用深色主题：

```csharp Program.cs
builder.Services.AddMasaBlazor(options =>
{
    options.ConfigureTheme(theme =>
    {
        theme.Dark = true;
    });
});
```

当您将组件指定为浅色或深色时，除非另有说明，否则它的所有子组件都将继承并应用相同的主题。

## 自定义主题

默认情况下，MASA Blazor 为所有组件应用标准主题。

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
        UserDefined = new Dictionary<string, string>()
    };
}
```

这可以轻松更改。只需在注册服务时设置主题属性即可。您可以选择修改所有或部分主题属性，其余的将从默认值继承。

```csharp Program.cs
builder.Services.AddMasaBlazor(options =>
{
    options.ConfigureTheme(theme =>
    {
        theme.Themes.Light.Primary = "#4318FF";
        theme.Themes.Light.Secondary = "#A18BFF";
        theme.Themes.Light.Accent = "#005CAF";
        theme.Themes.Light.UserDefined["Tertiary"] = "#e57373";
    });
});
```

## 动态更改主题

通过 `MasaBlazor` 服务，您可以在运行时更改主题。

<masa-example file="Examples.features.theme.DynamicallyModifyTheme"></masa-example>
