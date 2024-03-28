---
title: Page stack（页面堆栈）
desc: "**PPageStack** 组件提供了一个类似页面堆栈的容器，主要用于移动端。"
related:
  - /blazor/components/page-tabs
  - /blazor/components/application
  - /blazor/components/bottom-navigation
---

> [Masa.Blazor.MauiDemo](https://github.com/masastack/Masa.Blazor.MauiDemo) 项目中有更多的使用示例。

## 使用

- [PageStackLayout.razor](https://github.com/masastack/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Shared/PageStackLayout.razor)
  
  布局：把 **PPageStack** 组件放在 **MMain** 组件内，并 `@Body` 作为子内容。

- [PageStackTab1.razor](https://github.com/masastack/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Pages/PageStackTab1.razor), [PageStackTab2.razor](https://github.com/masastack/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Pages/PageStackTab2.razor), [PageStackTab3.razor](https://github.com/masastack/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Pages/PageStackTab3.razor)
  
  通过 `TabbedPatterns` 指定选项卡页面，第一次访问后会缓存页面内容。

- [PageStackPage1.razor](https://github.com/masastack/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Pages/PageStackPage1.razor), [PageStackPage2.razor](https://github.com/masastack/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Pages/PageStackPage2.razor), [PageStackPage3.razor](https://github.com/masastack/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Pages/PageStackPage3.razor)

  除了选项卡页面列表，其他页面都会通过从右到左的动画效果进入页面堆栈，移出即销毁。

<masa-example file="Examples.labs.page_stack.Usage" no-actions="true"></masa-example>

## 组件

### PPageStackItemInit

用于初始化堆栈页面的顶部 AppBar 的配置和页面容器的样式。

| 主要参数            | 说明                                  |
|-----------------|-------------------------------------|
| `Title`         | 页面标题，与 `BarContent` 冲突。             |
| `BarContent`    | 自定义顶部 AppBar 的内容。                   |
| `ActionContent` | 自定义顶部 AppBar 的右侧内容。                 |
| `RerenderKey`   | 用于强制渲染。当使用`BarContent`后其内容无法刷新时可使用。 |

## 基类

### PStackPageBase

堆栈页面本质上还是一个 **MDialog**，其根元素并非 `window`，在某些场景下你需要拿到堆栈页面的跟元素的时候，可以继承 **PStackPageBase**，
并通过 `PageSelector` 属性拿到堆栈页面的根元素。
