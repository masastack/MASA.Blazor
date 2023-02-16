---
title: Application（应用程序）
desc: "在 MASA Blazor 中，**MApp** 组件和 **MNavigationDrawer**、**MAppBar**、**MFooter** 等组件上的 App 属性，可以帮助你的应用围绕 **MMain** 组件进行适当的大小调整。这可以使你创建真正独特的界面，无需因管理布局尺寸而烦恼。 所有应用都需要 **MApp** 组件。 这是许多 MASA Blazor 组件和功能的挂载点，在确保它将默认的应用主题（Dark/Light）传递给子组件的同时还需要保证它在浏览器中对某些点击事件正确的跨浏览器支持。"
related:
  - /blazor/features/theme
  - /blazor/components/app-bars
  - /blazor/components/navigation-drawers
---

<app-alert type="error" content="为了让你的应用正常工作，你必须将其包裹在 **MApp** 组件中。 该组件是确保正确的跨浏览器兼容性的必要条件。 MASA Blazor 不支持在一个页面上有多个孤立的 
Masa.Blazor 实例。 **MApp** 可以存在于你的应用主体的任何地方，但是只能有一个，而且它必须是所有 MASA Blazor 组件的祖先节点。"></app-alert>

<app-alert type="info" content="如果你在应用中使用多个布局，你需要确保每个包含组件的根布局文件在其模板的根部有一个 **MApp**。"></app-alert>

## 默认应用标记

这是一个默认应用标记的例子。 只要设置 **App** 属性，你可以将布局元素放在任何地方。此处的关键组件是 **MMain** 。它将会根据您分配的应用程序
组件结构动态、灵活地调整大小。你可以使用上述所有组件的组合，包括 **MBottomNavigation** 。

```cshtml
<!-- MainLayout.razor -->
@inherits LayoutComponentBase

<MApp>
  <MNavigationDrawer App>
    <!-- -->
  </MNavigationDrawer>

  <MAppBar App>
    <!-- -->
  </MAppBar>

  <!-- 根据应用组件来调整你的内容 -->
  <MMain>
    <!-- 给应用提供合适的间距 -->
    <MContainer Fluid>
        @Body
    </MContainer>
  </MMain>

  <MFooter App>
    <!-- -->
  </MFooter>
</MApp>

```

<app-alert type="info" content="设置 `App` 属性会自动给布局元素设置 `position:fixed`。 如果你的应用程序需要一个绝对定位元素，你可以使用 `Absolute` 属性来覆盖这个功能。"></app-alert>

## 应用组件

以下是所有支持 **App** 属性的组件列表，这些组件可以在你的应用中用作布局元素。 这些组件可以混合和匹配，并且每个特定组件在任何时候都只能存在一个。 不过，你可以把它们换掉，布局也能适应。
有关如何构建各种布局的一些示例，请查看[预置布局](/blazor/getting-started/wireframes)页面。

每一个应用组件都有一个指定的位置和优先级，影响布局系统中的位置。

- [MAppBar](/blazor/components/app-bars)：总是放在应用顶部，优先级低于 **MSystemBar**。
- [MBottomNavigation](/blazor/components/bottom-navigation)：总是放在应用底部，优先级高于 **MFooter**。
- [MFooter](/blazor/components/footers)：总是放在应用底部，优先级低于 `MBottomNavigation`。
- [MNavigationDrawer](/blazor/components/navigation-drawers)：可以放置在应用的左边或右边，并且可以配置在 **MAppBar** 的旁边或下面。
- [MSystemBar](/blazor/components/system-bars)：总是放在应用顶部，优先级高于 **MAppBar** 。

![app](http://cdn.masastack.com/stack/doc/blazor/layouts/app.png)

## 应用服务

应用服务用于配置你的布局。 它与 **MMain** 组件通信，以便它能够正确地调整应用内容。 它有一些可以访问的属性：

```csharp
double Bar { get; }
double Bottom { get; }
double Footer { get; }
double InsetFooter { get; }
double Left { get; }
double Right { get; }
double Top { get; }
```

当你使用 **App** 属性添加和删除组件时，这些值会自动更新。 它们是不可编辑的，并且以只读状态存在。 你可以通过引用 **Application** 对象的应用属性来访问这些值。

```csharp
 [Inject] public MasaBlazor MasaBlazor { get; set; }
 
 Console.WriteLine(MasaBlazor.Application.Footer); // 60
```

<app-alert type="error" content="为了让你的应用正常工作，你必须将其包裹在 **MApp** 组件中。 该组件是确保正确的跨浏览器兼容性的必要条件。 MASA Blazor 不支持在一个页面上有多个孤立的 
Masa.Blazor 实例。 **MApp** 可以存在于你的应用主体的任何地方，但是只能有一个，而且它必须是所有 MASA Blazor 组件的祖先节点。"></app-alert>

## 应用主题

可以自定义 Masa Blazor 的主题，包括各种默认颜色......

在 Program.cs 中修改添加 Masa.Blazor 相关服务的代码，即可设置默认主题
```csharp
builder.Services.AddMasaBlazor(options =>
{
    options.ConfigureTheme(theme =>
    {
        theme.Themes.Light.Primary = "#4318FF";
        theme.Themes.Light.Secondary = "#5CBBF6";
        theme.Themes.Light.Accent = "#005CAF";
        theme.Themes.Light.UserDefined["Tertiary"] = "#e57373";
    });
})
```

### 动态修改主题

你可以通过 **MasaBlazor** 服务的 **ToggleTheme** 方法切换主题

<masa-example file="Examples.components.application.DynamicallyModifyTheme"></masa-example>