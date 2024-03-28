---
title: Page Stack
desc: "**PPageStack** component provides a container similar to a page stack, mainly for mobile."
related:
  - /blazor/components/page-tabs
  - /blazor/components/application
  - /blazor/components/bottom-navigation
---

> [Masa.Blazor.MauiDemo](https://github.com/masastack/Masa.Blazor.MauiDemo) has more usage examples.

## Usage

- [PageStackLayout.razor](https://github.com/masastack/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Shared/PageStackLayout.razor)

  Layout: Put the **PPageStack** component in the **MMain** component, and `@Body` as the child content.

- [PageStackTab1.razor](https://github.com/masastack/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Pages/PageStackTab1.razor), [PageStackTab2.razor](https://github.com/masastack/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Pages/PageStackTab2.razor), [PageStackTab3.razor](https://github.com/masastack/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Pages/PageStackTab3.razor)

  Through `TabbedPatterns` to specify the tabbed pages, the content of the page will be cached after the first visit.

- [PageStackPage1.razor](https://github.com/masastack/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Pages/PageStackPage1.razor), [PageStackPage2.razor](https://github.com/masastack/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Pages/PageStackPage2.razor), [PageStackPage3.razor](https://github.com/masastack/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Pages/PageStackPage3.razor)

  Except for the tabbed page list, other pages will enter the page stack with an animation effect from right to left, and exit will destroy the page.


<masa-example file="Examples.labs.page_stack.Usage" no-actions="true"></masa-example>

## Components

### PPageStackItemInit

Used to initialize the top AppBar configuration of the stack page and the style of the page container.

| Main parameters | Description |
|-----------------|-------------|
| `Title`         | Page title, conflicts with `BarContent`. |
| `BarContent`    | Custom content of the top AppBar. |
| `ActionContent` | Custom content on the right side of the top AppBar. |
| `RerenderKey`   | Used for forced rendering. When the content of `BarContent` cannot be refreshed, you can use it. |

## Base class

### PStackPageBase

The stack page is essentially an **MDialog**, and its root element is not `window`. In some scenarios, you need to get the root element of the stack page, you can inherit **PStackPageBase** and get the root element of the stack page through the `PageSelector` property.
