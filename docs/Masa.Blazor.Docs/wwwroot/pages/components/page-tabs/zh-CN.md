---
title: Page tabs（页面选项卡）
desc: "页面选项卡用于记录访问过的页面。"
tag: 预置
related:
  - /blazor/components/tabs
  - /blazor/components/menus
---

## 组件列表

### PPageTabs

一组访问过的标签页组件，可以通过点击标签页切换到对应的页面。

| 主要参数           | 说明                                                                                                                   | 
|----------------|----------------------------------------------------------------------------------------------------------------------|
| `TabOptions`   | 可以通过 `TabOptions` 属性自定义标签页的标题和图标。如果通过 **PPageTabsProvider** 组件提供的 `UpdateTabTitle` 方法更改标题，将会覆盖 `TabOptions` 属性设置的标题。 |
| `SelfPatterns` | 用于模糊匹配路径，所有匹配成功的路径都会显示在同一个标签页中。行为与`<a></a>`标签的 `href` 属性的 `_self` 类似。                                                |
| `OnClose`      | 自定义关闭标签页的行为。返回 `true` 表示关闭标签页，返回 `false` 表示不关闭标签页。                                                                   |

> 需配合 **PPageContainer** 组件一起使用。

### PPageContainer

一个的容器组件，包裹 `@Body` 后，将缓存页面的内容。行为类似 Vue 的 `<keep-alive></keep-alive>` 组件。

| 主要参数              | 说明                                                                  |
|-------------------|---------------------------------------------------------------------|
| `SelfPatterns`    | 用于模糊匹配路径，所有匹配成功的路径都会显示在同一个内容中。行为与`<a></a>`标签的 `href` 属性的 `self` 类似。 |
| `ExcludePattners` | 接受一个匹配路径的正则表达式列表，匹配成功的路径对应的页面的内容将不会在离开页面后被缓存在DOM中。                  |
| `IncludePatterns` | 不设置此参数时，默认所有页面都会缓存。                                                 |
| `Max`             | 最大缓存页面数量。                                                           |

> 该组件可以单独使用。

### PPageTabsProvider

一个提供器，层级应在 **PPageTabs** 和 **PPageContainer** 之上。子页面可以通过级联下去的 `IPageTabsProvider`
参数提供的 `UpdateTabTitle` 方法更改标签页的标题。

## 使用

底部的 **MBottomNavigation** 组件用于导航到新的页面，不是本示例的重点。

具体使用的源码列表如下：

- [PageTabsLayout.razor](https://github.com/masastack/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Shared/PageTabsLayout.razor)
  布局：演示使用 **PPageTabs**、**PPageContainer**和**PPageTabsProvider** 组件，以及如何使用 `TabOptions` 属性自定义标签页的标题和图标。
- [PageTabs1.razor](https://github.com/masastack/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Pages/PageTabs1.razor)
  Page1：演示保存页面状态的功能
- [PageTabs2.razor](https://github.com/masastack/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Pages/PageTabs2.razor)
  Page2：演示使用`ExcludedPatterns`属性后不会保存页面状态的功能
- [PageTabs3.razor](https://github.com/masastack/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Pages/PageTabs3.razor)
  Page3：演示更改标签页标题的功能
- [PageTabs4.razor](https://github.com/masastack/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Pages/PageTabs4.razor)
  Page4：演示模糊匹配路径只显示一个标签页的功能

<masa-example file="Examples.components.page_tabs.Usage" no-actions="true"></masa-example>
