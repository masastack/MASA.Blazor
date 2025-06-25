---
title: Page stack（页面堆栈）
desc: "**PPageStack** 组件提供了一个类似页面堆栈的容器，主要用于移动端。"
related:
  - /blazor/components/page-tabs
  - /blazor/components/application
  - /blazor/components/bottom-navigation
---

> [Masa.Blazor.MauiDemo](https://github.com/masastack/Masa.Blazor.MauiDemo) 项目中有更多的使用示例。

<app-alert type="warning" content="**PageStack** 组件不会删除旧页面的DOM，新页面直接覆盖在旧页面上；在 MAUI Blazor Hybrid app 上，Webview 的性能会因页面堆栈的增加而下降；具体多少页面后会呈明显下降取决于页面的复杂度，请及时替代或删除不会再访问的页面。"></app-alert>

## 安装 {#installation released-on=v1.10.0}

```shell
dotnet add package Masa.Blazor.MobileComponents
```

```c# Program.cs l:3
builder.Services
    .AddMasaBlazor()
    .AddMobileComponents();
```

## 示例 {#example}

- [PageStackLayout.razor](https://github.com/masastack/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Shared/PageStackLayout.razor)
 
  - 通过 `TabRules` 指定选项卡页面，第一次访问后会缓存页面内容。
  - 使用 **PPageStackTab** 组件包裹类`a`标签，改变导航行为并提供额外的功能。
  
- [PageStackTab1.razor](https://github.com/masastack/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Pages/PageStackTab1.razor), [PageStackTab2.razor](https://github.com/masastack/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Pages/PageStackTab2.razor), [PageStackTab3.razor](https://github.com/masastack/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Pages/PageStackTab3.razor)

  - Tab 1 页面不持久化，不使用路由参数。
  - Tab 2 页面持久化，但使用路由参数。
  - Tab 3 页面持久化，不使用路由参数；激活时再次点击会触发下拉刷新的事件。

- [PageStackPage1.razor](https://github.com/masastack/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Pages/PageStackPage1.razor),
  [PageStackPage2.razor](https://github.com/masastack/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Pages/PageStackPage2.razor),
  [PageStackPage3.razor](https://github.com/masastack/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Pages/PageStackPage3.razor),
  [PageStackPage4.razor](https://github.com/masastack/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Pages/PageStackPage4.razor)

  除了选项卡页面列表，其他页面都会通过从右到左的动画效果进入页面堆栈，移出即销毁。

<masa-example file="Examples.mobiles.page_stack.Usage" no-actions="true"></masa-example>

## 内置组件和类 {#built-in-components-and-classes}

### PageStackNavController

#### 方法 {#methods}

用于控制页面堆栈的导航。

| 方法名                    | 说明                                                      |
|--------------------------|---------------------------------------------------------|
| `Push`                   | 将新页面推入堆栈。                                               |
| `Pop`                    | 将当前页面弹出堆栈，同等于 `GoBack(1)`。                              |
| `GoBack`                 | 返回到指定的前第几个页面。                                           |
| `GoBackAndReplace`       | 返回到指定的前第几个页面后调用`Replace`方法。                             |
| `GoBackToPage`           | 返回到指定页面。                                                |
| `GoBackToPageAndRepalce` | 返回到指定页面后调用`Replace`方法。                                  |
| `Replace`                | 替换当前页面。                                                 |
| `Clear`                  | 清空当前页面堆栈。                                               |
| `GoBackToTab`            | 清空当前堆栈并跳转到指定选项卡。                                        |

```razor
@inject PageStackNavController NavController

<MButton OnClick="@(() => NavController.Push("/stack-page"))">Go to stack page</MButton>
```

#### 事件 {#events}

| 事件名                   | 说明                            | 使用场景                                      |
|-----------------------|-------------------------------|-------------------------------------------|
| `PageClosed`          | 堆栈页面关闭时触发。                    | -                                         |
| `TabChanged`          | 选项卡切换时触发，包括系统级返回时。入栈或出栈时不会触发。 | 切换时重置旧tab页的某些状态，如弹窗。                      |
| `TabRefreshRequested` | 选项卡刷新请求。                      | 配合[下拉刷新组件](/blazor/mobiles/pull-refresh)使用。 |
| `RequestTabBadgeUpdate` | 选项卡徽章更新请求。                    | 新消息通知                                     |
| `RequestTabBadgeClear` | 选项卡徽章清除请求。                    |                                           |

```razor

### data-page-stack-strategy（不推荐） {#data-page-stack-strategy}

<app-alert type="warning" content="建议使用 `PPageStackLink` 组件，以避免连续点击导致多次触发。"></app-alert>

对于 `Push` 操作，可以在`a`标签或组件上添加内置的 `data-page-stack-strategy="push"` 属性，以便在点击时自动将新页面推入堆栈。

> `data-page-stack-strategy` 属性的值可以不填，默认为 `push`。

```razor
<a href="/stack-page" data-page-stack-strategy="push">Stack page</a>

<MButton Href="/stack-page" data-page-stack-strategy>Stack page</MButton>
```

### PPageStackLink {released-on=v1.7.0}

`a` 标签存在一个无法避免的问题：连续点击会导致多次触发。**PPageStackLink** 是一个专门用于页面堆栈的链接组件，可以避免这个问题。

```razor
<PPageStackLink Href="/stack-page">Stack page</PPageStackLink>

<PPageStackLink Href="/stack-page">
    <CustomContent Context="push">
        <MButton OnClick="push">Stack page</MButton>
    </CustomContent>
</PPageStackLink>
```

### PPageStackTab {released-on=v1.10.0}

**PPageStackTab** 组件提供两个功能：

- 默认的 `a` 标签或组件的导航行为会往浏览器历史记录中添加一条记录，标签页切换时会导致历史记录的增加。 
  在 MAUI Blazor Hybrid app 上想实现在标签页上点击返回按钮退出应用程序就比较麻烦。
  使用 **PPageStackTab** 组件包裹 `a` 标签或组件，会将默认导航行为改为 `replace` 方法替换当前页面，从而不会增加历史记录。

  ```razor
  <PPagStackTab href="/tab-1">
      <a @attributes="@context.Attrs">Stack page</a>
  </PPagStackTab>
  ```

- 在激活标签页下再次点击导航链接时，会触发 `TabRefreshRequested` 事件，配合 [下拉刷新组件](/blazor/mobiles/pull-refresh) 使用，可以实现下拉刷新的效果。

  ```razor
  @inject PageStackNavController NavController
  @implements IDisposable
  
  <MPullRefresh @ref="_pullRefresh" OnRefresh="...">
      ...
  </MPullRefresh>
  
  @code {
      private MPullRefresh? _pullRefresh;
  
      protected override void OnInitialized()
      {
          base.OnInitialized();

          NavController.TabRefreshRequested += NavControllerOnTabRefreshRequested;
      }
  
      private async void NavControllerOnTabRefreshRequested(object? sender, PageStackTabRefreshRequestedEventArgs e)
      {
          if (_pullRefresh is null)
          {
              return;
          }

          if (e.TargetHref?.Equals("/tab-1", StringComparison.OrdinalIgnoreCase) is not true)
          {
              return;
          }

          await _pullRefresh.SimulateRefreshAsync();
      }
  
      public void Dispose()
      {
          NavController.TabRefreshRequested -= NavControllerOnTabRefreshRequested;
      }
  }
  ```

### PStackPageBar {released-on=v1.10.0}

<app-alert type="error" content="**PStackPageBarInit** 组件已废弃，请使用 **PStackPageBar** 组件。"></app-alert>

初始化堆栈页面顶部的 AppBar。大部分属性和 [MAppBar](/blazor/components/app-bars)一致。

```razor MyStackPage.razor
<PStackPageBar Title="Order detail"
               CenterTitle 
               Flat
               Color="primary">
</PStackPageBar>
```

以下是 **PStackPageBar** 不同于 **MAppBar** 的参数：

| 参数名称            | 说明                                  |
|-----------------|-------------------------------------|
| `Title`         | 页面标题。                               |
| `CenterTitle`   | 是否将标题居中。                            |
| `Image`         | 背景图片，与 **MAppBar** 的 `Src` 参数一致。    |
| `BarContent`    | 自定义整个内容。                            |
| `GoBackContent` | 自定义返回按钮。                            |
| `ActionContent` | 设置右侧操作区域。                           |

### PStackPageBase

可选的基类，提供了一些方法和属性。

#### PageSelector

堆栈页面本质上还是一个 **MDialog**，其根元素并非 `window`，在某些场景下你需要拿到堆栈页面的跟元素的时候，
可以继承 **PStackPageBase**，并通过 `PageSelector` 属性拿到堆栈页面的根元素。

#### OnPageActivated[Async]

当堆栈页首次加载，以及从其他页面返回时触发。

```razor MyStackPage.razor
@inherits PStackPageBase

@code {
    protected override void OnPageActivated(object? state)
    {
        // `state` 来自上一个页面调用 GoBack 时传递的参数
    }
}
```

#### OnPageDeactivated[Async]

当导航到其他页面时触发。
