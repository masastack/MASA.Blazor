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

- 可以通过 `TabContent` 插槽自定义标签页的标题。如果通过 `IPageTabsProvider` 参数提供的 `UpdateTabTitle`
  方法更改标题，将会覆盖 `TabContent` 插槽的内容。
- `SelfPatterns` 属性用于模糊匹配路径，所有匹配成功的路径都会显示在同一个标签页中。行为与`<a></a>`标签的 `href` 属性类似。

### PPageContainer

一个的容器组件，包裹 `@Body` 后，将缓存页面的内容。

### PPagePTabsProvider

一个提供器，层级应在 **PPageTabs** 和 **PPageContainer** 之上。

- 子页面可以通过级联下去的 `IPageTabsProvider` 参数提供的 `UpdateTabTitle` 方法更改标签页的标题。

## 使用

以下是一个使用了 `iframe` 展示的例子。底部的 **MBottomNavigation** 组件用于导航到新的页面，不是本示例的重点。

具体使用的源码列表如下：

- [PageTabsLayout.razor](https://github.com/BlazorComponent/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Shared/PageTabsLayout.razor)
  布局：演示使用 **PPageTabs**、**PPageContainer**和**PPageTabsProvider** 组件
- [PageTabs1.razor](https://github.com/BlazorComponent/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Pages/PageTabs1.razor)
  Page1：演示保存页面状态的功能
- [PageTabs2.razor](https://github.com/BlazorComponent/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Pages/PageTabs2.razor)
  Page2：演示保存页面状态的功能
- [PageTabs3.razor](https://github.com/BlazorComponent/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Pages/PageTabs3.razor)
  Page3：演示更改标签页标题的功能
- [PageTabs4.razor](https://github.com/BlazorComponent/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Pages/PageTabs4.razor)
  Page4：演示模糊匹配路径只显示一个标签页的功能

<masa-example file="Examples.components.page_tabs.Usage" no-actions="true"></masa-example>
