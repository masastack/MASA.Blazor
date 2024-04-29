---
title: Page stack（页面堆栈）
desc: "**PPageStack** 组件提供了一个类似页面堆栈的容器，主要用于移动端。"
related:
  - /blazor/components/page-tabs
  - /blazor/components/application
  - /blazor/components/bottom-navigation
---

> [Masa.Blazor.MauiDemo](https://github.com/masastack/Masa.Blazor.MauiDemo) 项目中有更多的使用示例。

## 示例

- [PageStackLayout.razor](https://github.com/masastack/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Shared/PageStackLayout.razor)

  布局：把 **PPageStack** 组件放在 **MMain** 组件内，并 `@Body` 作为子内容。

- [PageStackTab1.razor](https://github.com/masastack/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Pages/PageStackTab1.razor), [PageStackTab2.razor](https://github.com/masastack/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Pages/PageStackTab2.razor), [PageStackTab3.razor](https://github.com/masastack/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Pages/PageStackTab3.razor)

  通过 `TabbedPatterns` 指定选项卡页面，第一次访问后会缓存页面内容。

- [PageStackPage1.razor](https://github.com/masastack/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Pages/PageStackPage1.razor),
  [PageStackPage2.razor](https://github.com/masastack/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Pages/PageStackPage2.razor),
  [PageStackPage3.razor](https://github.com/masastack/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Pages/PageStackPage3.razor),
  [PageStackPage4.razor](https://github.com/masastack/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Pages/PageStackPage4.razor)

  除了选项卡页面列表，其他页面都会通过从右到左的动画效果进入页面堆栈，移出即销毁。

<masa-example file="Examples.labs.page_stack.Usage" no-actions="true"></masa-example>

## 内置组件和类

### PageStackNavController

用于控制页面堆栈的导航。

| 方法名       | 说明                         |
|-----------|----------------------------|
| `Push`    | 将新页面推入堆栈。                  |
| `Pop`     | 将当前页面弹出堆栈，同等于 `GoBack(1)`。 |
| `GoBack`  | 返回到指定的前几个页面。               |
| `Replace` | 替换当前页面。                    |
| `Clear`   | 清空当前页面堆栈。                  |
| `GoToTab` | 清空当前堆栈并跳转到指定选项卡。           |

```razor
@inject PageStackNavController NavController

<MButton OnClick="@(() => NavController.Push("/stack-page"))">Go to stack page</MButton>
```

### data-page-stack-strategy

对于 `Push` 操作，可以在`a`标签或组件上添加内置的 `data-page-stack-strategy="push"` 属性，以便在点击时自动推入堆栈。

> `data-page-stack-strategy` 属性的值可以不填，默认为 `push`。

```razor
<a href="/stack-page" data-page-stack-strategy="push">Stack page</a>

<MButton Href="/stack-page" data-page-stack-strategy>Stack page</MButton>
```

### PStackPageBarInit

初始化堆栈页面顶部的 AppBar。大部分属性和 [MAppBar](/blazor/components/app-bars)一致。

```razor MyStackPage.razor
<PStackPageBarInit Title="Order detail"
                   CenterTitle 
                   Flat
                   Color="primary">
</PStackPageBarInit>
```

以下是 **PStackPageBarInit** 不同于 **MAppBar** 的参数：

| 参数名称            | 说明                                  |
|-----------------|-------------------------------------|
| `Title`         | 页面标题。                               |
| `CenterTitle`   | 是否将标题居中。                            |
| `Image`         | 背景图片，与 **MAppBar** 的 `Src` 参数一致。    |
| `BarContent`    | 自定义整个内容。                            |
| `GoBackContent` | 自定义返回按钮。                            |
| `ActionContent` | 设置右侧操作区域。                           |
| `RerenderKey`   | 用于强制渲染。当使用`BarContent`后其内容无法刷新时可使用。 |

### PStackPageBase

可选的基类，提供了一些方法和属性。

#### PageSelector

堆栈页面本质上还是一个 **MDialog**，其根元素并非 `window`，在某些场景下你需要拿到堆栈页面的跟元素的时候，
可以继承 **PStackPageBase**，并通过 `PageSelector` 属性拿到堆栈页面的根元素。

#### OnPageActivated[Async]

当堆栈页首次加载，以及从其他页面返回时触发。

```razor MyStackPage.razor
@inherit PStackPageBase

@code {
    protected override void OnPageActivated(object? state)
    {
        // `state` 来自上一个页面调用 GoBack 时传递的参数
    }
}
```

#### OnPageDeactivated[Async]

当导航到其他页面时触发。
