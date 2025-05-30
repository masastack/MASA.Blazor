---
title: Page Stack
desc: "**PPageStack** component provides a container similar to a page stack, mainly for mobile."
related:
  - /blazor/components/page-tabs
  - /blazor/components/application
  - /blazor/components/bottom-navigation
---

> [Masa.Blazor.MauiDemo](https://github.com/masastack/Masa.Blazor.MauiDemo) has more usage examples.

<app-alert type="warning" content="The **PageStack** component does not remove the DOM of old pages; instead, new pages are directly overlaid on top of the old ones. In a MAUI Blazor Hybrid app, the performance of the WebView degrades as the page stack grows due to the accumulation of DOM elements in memory. The exact number of pages after which the performance drop becomes noticeable depends on the complexity of the pages. To maintain optimal performance, it is recommended to replace or remove pages that will no longer be accessed."></app-alert>

## Installation {released-on=v1.10.0}

```shell
dotnet add package Masa.Blazor.MobileComponents
```

```c# Program.cs l:3
builder.Services
    .AddMasaBlazor()
    .AddMobileComponents();
```

## Example

- [PageStackLayout.razor](https://github.com/masastack/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Shared/PageStackLayout.razor)

  - Through `TabRules` to specify the tabbed pages, the content of the page will be cached after the first visit.
  - Wrap the `a` tag with the **PPageStackTab** component to change the navigation behavior and provide additional functionality.

- [PageStackTab1.razor](https://github.com/masastack/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Pages/PageStackTab1.razor), [PageStackTab2.razor](https://github.com/masastack/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Pages/PageStackTab2.razor), [PageStackTab3.razor](https://github.com/masastack/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Pages/PageStackTab3.razor)

  - Tab 1 page is not persistent and does not use route parameters.
  - Tab 2 page is persistent but uses route parameters.
  - Tab 3 page is persistent and does not use route parameters; clicking again when activated will trigger the refresh-requested event.

- [PageStackPage1.razor](https://github.com/masastack/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Pages/PageStackPage1.razor),
  [PageStackPage2.razor](https://github.com/masastack/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Pages/PageStackPage2.razor),
  [PageStackPage3.razor](https://github.com/masastack/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Pages/PageStackPage3.razor),
  [PageStackPage4.razor](https://github.com/masastack/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Pages/PageStackPage4.razor)

  Except for the tabbed page list, other pages will enter the page stack with an animation effect from right to left,
  and exit will destroy the page.

<masa-example file="Examples.mobiles.page_stack.Usage" no-actions="true"></masa-example>

## Built-in components and classes

### PageStackNavController

#### Methods

Used to control the navigation of the page stack.

| Method name              | Description                                                              |
|--------------------------|--------------------------------------------------------------------------|
| `Push`                   | Push a new page onto the stack.                                          |
| `Pop`                    | Pop the current page from the stack, equivalent to `GoBack(1)`.          |
| `GoBack`                 | Return to the specified number of pages.                                 |
| `GoBackAndReplace`       | Return to the specified number of pages, and the invoke `Replace` method.|
| `GoBackToPage`           | Return to the specified page.                                            |
| `GoBackToPageAndReplace` | Return to the specified page, and then invoke `Replace` method.          |
| `Replace`                | Replace the current page.                                                |
| `Clear`                  | Clear the current page stack.                                            |
| `GoBackToTab`            | Clear the current stack and jump to the specified tab.                   |

```razor
@inject PageStackNavController NavController

<MButton OnClick="@(() => NavController.Push("/stack-page"))">Go to stack page</MButton>
```

#### Events

| Event name            | Description                                                                                                        | Usage scenario                                                        |
|-----------------------|--------------------------------------------------------------------------------------------------------------------|-----------------------------------------------------------------------|
| `PageClosed`          | Triggered when the stack page is closed.                                                                           | -                                                                     |
| `TabChanged`          | Triggered when the tab is switched, including system-level return. It will not be triggered when pushed or popped. | Reset some states of the old tab page when switching, such as popups. |
| `TabRefreshRequested` | Tab refresh request.                                                                                               | Used with [Pull to refresh](/blazor/mobiles/pull-refresh) component.  |

### data-page-stack-strategy(not recommended) {#data-page-stack-strategy}

<app-alert type="warning" content="It is recommended to use the **PPageStackLink** component to avoid multiple triggers due to continuous clicks."></app-alert>

For the `Push` operation, you can add the built-in `data-page-stack-strategy="push"` attribute to the `a` tag or
component to automatically push onto the stack when clicked.

> The value of the `data-page-stack-strategy` attribute can be left blank, and the default is `push`.

```razor
<a href="/stack-page" data-page-stack-strategy="push">Stack page</a>

<MButton Href="/stack-page" data-page-stack-strategy>Stack page</MButton>
```

### PPageStackLink {released-on=v1.7.0}

The `a` tag has an unavoidable problem: continuous clicks will cause multiple triggers. `PPageStackLink` is a link component specifically for page stacks that can avoid this problem.

```razor
<PPageStackLink Href="/stack-page">Stack page</PPageStackLink>

<PPageStackLink Href="/stack-page">
    <CustomContent Context="push">
        <MButton OnClick="push">Stack page</MButton>
    </CustomContent>
</PPageStackLink>
```

### PPageStackTab {released-on=v1.10.0}

**PPageStackTab** component provides two features:

- The default `a` tag or component navigation behavior adds a record to the browser history, which increases the history when switching tabs.
  In a MAUI Blazor Hybrid app, it is challenging to implement the back button on the tab page to exit the application.
  Wrapping the `a` tag or component with the **PPageStackTab** component changes the default navigation behavior to replace the current page with the `replace` method, thus not increasing the history.

  ```razor
  <PPagStackTab href="/tab-1">
      <a @attributes="@context.Attrs">Stack page</a>
  </PPagStackTab>
  ```

- When the activated tab is clicked again, it will trigger the `TabRefreshRequested` event. When used with the [Pull to refresh component](/blazor/mobiles/pull-refresh), it can achieve a pull-to-refresh effect.

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
          if (_pullToRefresh is null)
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

<app-alert type="error" content="The **PStackPageBarInit** component has been deprecated, please use the **PStackPageBar** component instead."></app-alert>

Used to initialize the AppBar at the top of the stack page. Most properties are consistent
with [MAppBar](/blazor/components/app-bars).

```razor MyStackPage.razor
<PStackPageBar Title="Order detail"
               CenterTitle 
               Flat
               Color="primary">
</PStackPageBar>
```

The following are the parameters of **PStackPageBar** that are different from **MAppBar**:

| Main parameters | Description                                                                                                |
|-----------------|------------------------------------------------------------------------------------------------------------|
| `Title`         | Page title.                                                                                                |
| `CenterTitle`   | Center the title.                                                                                          |
| `Image`         | Background image, consistent with the `Src` parameter of **MAppBar**.                                      |
| `BarContent`    | Custom the entire content.                                                                                 |
| `GoBackContent` | Custom the back button.                                                                                    |
| `ActionContent` | Set the right operation area.                                                                              |

### PStackPageBase

Optional base class for stack pages.

#### PageSelector

堆栈页面本质上还是一个 **MDialog**，其根元素并非 `window`，在某些场景下你需要拿到堆栈页面的跟元素的时候，
可以继承 **PStackPageBase**，并通过 `PageSelector` 属性拿到堆栈页面的根元素。

The stack page is essentially an **MDialog**, and its root element is not `window`. In some scenarios, when you need to get the root element of the stack page,
you can inherit **PStackPageBase** and get the root element of the stack page through the `PageSelector` property.

#### OnPageActivated[Async]

Triggered when the stack page is first loaded and when returning from other pages.

```razor MyStackPage.razor
@inherits PStackPageBase

@code {
    protected override void OnPageActivated(object? state)
    {
        // `state` comes from the parameter passed when the previous page calls GoBack
    }
}
```

#### OnPageDeactivated[Async]

Triggered when navigating to other pages.
