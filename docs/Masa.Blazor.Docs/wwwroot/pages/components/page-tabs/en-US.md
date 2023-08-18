---
title: Page tabs
desc: "The PageTabs is used to record the visited pages."
tag: Preset
related:
  - /blazor/components/tabs
  - /blazor/components/menus
---

## Component list

### PPageTabs

A set of visited tab components that can be switched to the corresponding page by clicking the tab.

| Primary Parameter | Description                                                                                                                                                                                                            |
|-------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `TabOptions`      | Customize the title and icon of the tab. If you change the title through the `UpdateTabTitle` method provided by the **PPageTabsProvider** component, the title set by the `TabOptions` parameter will be overwritten. |
| `SelfPatterns`    | Used for fuzzy matching paths. All successful paths are displayed in the same tab. The behavior is similar to `_self` of the `href` attribute of the `<a></a>` tag.                                                    |
| `OnClose`         | Customize the behavior of closing the tab. Return `true` to close the tab and return `false` to not close the tab.                                                                                                     |

> It needs to be used with the **PPageContainer** component.

### PPageContainer

After wrapping `@Body`, the contents of the page will be cached. Works like the `<keep-alive></keep-alive>` component in
Vue.

| Primary Parameter | Description                                                                                                                                                             |
|-------------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `SelfPatterns`    | Used for fuzzy matching paths. All successful paths are displayed in the same content. The behavior is similar to `_self` of the `href` attribute of the `<a></a>` tag. |
| `ExcludePattners` | Accepts a list of regular expressions that match the path, and the content of the page corresponding to the successful match will not be cached in the DOM.             |
| `IncludePatterns` | When this parameter is not set, all pages are cached by default.                                                                                                        |
| `Max`             | Maximum number of cached pages.                                                                                                                                         |

> The component can be used alone.

### PPageTabsProvider

A provider with levels above **PPageTabs** and **PPageContainer**. Sub-pages can change the title of the tab through
the `UpdateTabTitle` method provided by the cascading
parameter `IPageTabsProvider`.

## Usage

The following is an example of using `iframe`. The **MBottomNavigation** component at the bottom is used to navigate to
a
new page and is not the focus of this example.

The specific source codes are listed below:

- [PageTabsLayout.razor](https://github.com/masastack/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Shared/PageTabsLayout.razor)
  Layout: demonstrate the use of **PPageTabs**, **PPageContainer** and **PPageTabsProvider** components, and how to
  use `TabOptions` to customize the title and icon of the tab.
- [PageTabs1.razor](https://github.com/masastack/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Pages/PageTabs1.razor)
  Page1: demonstrate the ability to save page state.
- [PageTabs2.razor](https://github.com/masastack/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Pages/PageTabs2.razor)
  Page2: demonstrate the ability to not save page state after using the `ExcludedPatterns` attribute.
- [PageTabs3.razor](https://github.com/masastack/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Pages/PageTabs3.razor)
  Page3: demonstrates the ability to change the title of a tab.
- [PageTabs4.razor](https://github.com/masastack/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Pages/PageTabs4.razor)
  Page4: demonstrates the ability of fuzzy matching paths to display only one tab.

<masa-example file="Examples.components.page_tabs.Usage" no-actions="true"></masa-example>
