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

- You can customize the title and icon of the tab through the `TabOptions` parameter. If you change the title through
  the `UpdateTabTitle` method provided with the `IPageTabsProvider` parameter, the title set by the `TabOptions` parameter
  will be overwritten.
- The `SelfPatterns` parameter is used for fuzzy matching paths. All successful paths are displayed in the same tab. The
  behavior is similar to the `href` attribute of the `<a></a>` tag.
- Use the `OnClose` parameter to customize the behavior of closing the tab. Return `true` to close the tab and return
  `false` to not close the tab.

### PPageContainer

After wrapping `@Body`, the contents of the page will be cached.

### PPageTabsProvider

A provider with levels above **PPageTabs** and **PPageContainer**.

- Sub-pages can change the title of the tab through the `UpdateTabTitle` method provided by the cascading
  parameter `IPageTabsProvider`.

## Usage

The following is an example of using `iframe`. The **MBottomNavigation** component at the bottom is used to navigate to a
new page and is not the focus of this example.

The specific source codes are listed below:

- [PageTabsLayout.razor](https://github.com/masastack/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Shared/PageTabsLayout.razor)
  Layout: demonstrate the use of **PPageTabs**, **PPageContainer** and **PPageTabsProvider** components, and how to use `TabOptions` to customize the title and icon of the tab.
- [PageTabs1.razor](https://github.com/masastack/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Pages/PageTabs1.razor)
  Page1: demonstrate the ability to save page state.
- [PageTabs2.razor](https://github.com/masastack/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Pages/PageTabs2.razor)
  Page2: demonstrate the ability to save page state.
- [PageTabs3.razor](https://github.com/masastack/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Pages/PageTabs3.razor)
  Page3: demonstrates the ability to change the title of a tab.
- [PageTabs4.razor](https://github.com/masastack/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Pages/PageTabs4.razor)
  Page4: demonstrates the ability of fuzzy matching paths to display only one tab.

<masa-example file="Examples.components.page_tabs.Usage" no-actions="true"></masa-example>
