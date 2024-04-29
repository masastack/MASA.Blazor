---
title: Page Stack
desc: "**PPageStack** component provides a container similar to a page stack, mainly for mobile."
related:
  - /blazor/components/page-tabs
  - /blazor/components/application
  - /blazor/components/bottom-navigation
---

> [Masa.Blazor.MauiDemo](https://github.com/masastack/Masa.Blazor.MauiDemo) has more usage examples.

## Example

- [PageStackLayout.razor](https://github.com/masastack/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Shared/PageStackLayout.razor)

  Layout: Put the **PPageStack** component in the **MMain** component, and `@Body` as the child content.

- [PageStackTab1.razor](https://github.com/masastack/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Pages/PageStackTab1.razor), [PageStackTab2.razor](https://github.com/masastack/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Pages/PageStackTab2.razor), [PageStackTab3.razor](https://github.com/masastack/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Pages/PageStackTab3.razor)

  Through `TabbedPatterns` to specify the tabbed pages, the content of the page will be cached after the first visit.

- [PageStackPage1.razor](https://github.com/masastack/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Pages/PageStackPage1.razor),
  [PageStackPage2.razor](https://github.com/masastack/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Pages/PageStackPage2.razor),
  [PageStackPage3.razor](https://github.com/masastack/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Pages/PageStackPage3.razor),
  [PageStackPage4.razor](https://github.com/masastack/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Pages/PageStackPage4.razor)

  Except for the tabbed page list, other pages will enter the page stack with an animation effect from right to left,
  and exit will destroy the page.

<masa-example file="Examples.labs.page_stack.Usage" no-actions="true"></masa-example>

## Built-in components and classes

### PageStackNavController

Used to control the navigation of the page stack.

| Method name | Description                                                     |
|-------------|-----------------------------------------------------------------|
| `Push`      | Push a new page onto the stack.                                 |
| `Pop`       | Pop the current page from the stack, equivalent to `GoBack(1)`. |
| `GoBack`    | Return to the specified number of pages.                        |
| `Replace`   | Replace the current page.                                       |
| `Clear`     | Clear the current page stack.                                   |
| `GoToTab`   | Clear the current stack and jump to the specified tab.          |

```razor
@inject PageStackNavController NavController

<MButton OnClick="@(() => NavController.Push("/stack-page"))">Go to stack page</MButton>
```

### data-page-stack-strategy

For the `Push` operation, you can add the built-in `data-page-stack-strategy="push"` attribute to the `a` tag or
component to automatically push onto the stack when clicked.

> The value of the `data-page-stack-strategy` attribute can be left blank, and the default is `push`.

```razor
<a href="/stack-page" data-page-stack-strategy="push">Stack page</a>

<MButton Href="/stack-page" data-page-stack-strategy>Stack page</MButton>
```

### PStackPageBarInit

Used to initialize the AppBar at the top of the stack page. Most properties are consistent
with [MAppBar](/blazor/components/app-bars).

```razor MyStackPage.razor
<PStackPageBarInit Title="Order detail"
                   CenterTitle 
                   Flat
                   Color="primary">
</PStackPageBarInit>
```

The following are the parameters of **PStackPageBarInit** that are different from **MAppBar**:

| Main parameters | Description                                                                                                |
|-----------------|------------------------------------------------------------------------------------------------------------|
| `Title`         | Page title.                                                                                                |
| `CenterTitle`   | Center the title.                                                                                          |
| `Image`         | Background image, consistent with the `Src` parameter of **MAppBar**.                                      |
| `BarContent`    | Custom the entire content.                                                                                 |
| `GoBackContent` | Custom the back button.                                                                                    |
| `ActionContent` | Set the right operation area.                                                                              |
| `RerenderKey`   | Used for forced rendering. When the content of `BarContent` cannot be refreshed after use, you can use it. |

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
@inherit PStackPageBase

@code {
    protected override void OnPageActivated(object? state)
    {
        // `state` comes from the parameter passed when the previous page calls GoBack
    }
}
```

#### OnPageDeactivated[Async]

Triggered when navigating to other pages.
