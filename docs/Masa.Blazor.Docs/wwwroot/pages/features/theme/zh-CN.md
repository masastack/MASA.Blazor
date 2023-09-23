# 主题配置

轻松地通过编程的方式改变应用程序的颜色。重新构建默认样式表并根据您的特定需求自定义框架的各个方面。

## 自定义主题

MASA Blazor 会为所有组件应用标准主题，您也可以在注册服务时自定义主题，未定义的属性将继承默认值。

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

## 动态更改主题

您可以通过 `IThemeService` 服务在运行时切换 `浅色` 和 `深色` 主题。

当您将组件指定为浅色或深色时，除非另有说明，否则它的所有子组件都将继承并应用相同的值。

<masa-example file="Examples.features.theme.ChangeThemeMode"></masa-example>

您还可以在运行时动态修改颜色。

<masa-example file="Examples.features.theme.ChangeThemeColor"></masa-example>

## 高级用法

您可以使用 `MThemeProvider` 组件方便地设置主题，例如为每个页面使用不同的配色方案。

<masa-example file="Examples.features.theme.SinglePageCustomStyles"></masa-example>

## 更改样式生成位置

我们默认会生成在 `body` 内生成 `style` 元素。

在某情况下，为了规范与安全性可能需要将样式移动到在 `head` 中。您可以手动在 `head` 中添加 `MThemeProvider` 组件，此时我们将不会在 `body` 中生成默认样式。

```html Pages/_Host.cshtml
    <component type="typeof(MThemeProvider)" render-mode="ServerPrerendered" />
```