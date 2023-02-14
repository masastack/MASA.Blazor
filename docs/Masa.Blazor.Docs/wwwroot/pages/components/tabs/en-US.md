---
title: Tabs
desc: "The **MTabs** component is used for hiding content behind a selectable item. This can also be used as a pseudo-navigation for a page, where the tabs are links and the tab-items are the content."
related:
  - /blazor/components/icons
  - /blazor/components/toolbars
  - /blazor/components/windows
---

## Usage

The **MTabs** component is a styled extension of [MItemGroup](/blazor/components/item-groups). It provides an easy to use
interface for organizing groups of content.

<masa-example file="Examples.components.tabs.Usage"></masa-example>

## Caveats

<app-alert type="warning" content="When using the `Dark` prop and `NOT` providing a custom `color`, the **MTabs** component will default its color to white. "></app-alert>

<app-alert type="warning" content="When using **MTabItem**'s that contain required input fields you must use the `eager` prop in order to validate the required fields that are not yet visible."></app-alert>

## Examples

### Props

#### Align with title

Make **MTabs** lined up with the **MToolbarTitle** component using the `AlignWithTitle` prop (**MAppBarNavIcon**
or **MButton** must be used in **MToolbar**).

<masa-example file="Examples.components.tabs.AlignWithTitle"></masa-example>

#### Center active

The `CenterActive` prop will make the active tab always centered.

<masa-example file="Examples.components.tabs.CenterActive"></masa-example>

#### Custom icons

`PrevIcon` and `NextIcon` can be used for applying custom pagination icons.

<masa-example file="Examples.components.tabs.CustomIcons"></masa-example>

#### Fixed tabs

The `FixedTabs` prop forces **MTab** to take up all available space up to the maximum width (300px).

<masa-example file="Examples.components.tabs.FixedTabs"></masa-example>

#### Grow

The `Grow` prop will make the tab items take up all available space up to a maximum width of 300px.

<masa-example file="Examples.components.tabs.Grow"></masa-example>

#### Icon ant text

Using `IconsWithText` prop increases the **MTabs**s height to 72x to allow for both icons as well as text to be used.

<masa-example file="Examples.components.tabs.IconAndText"></masa-example>

#### Pagination

If the tab items overflow their container, pagination controls will appear on desktop. For mobile devices, arrows will
only display with the `ShowArrows` prop.

<masa-example file="Examples.components.tabs.Pagination"></masa-example>

#### Right

The `Right` prop aligns the tabs to the right.

<masa-example file="Examples.components.tabs.Right"></masa-example>

#### VerticalTabs

The `Vertical` prop allows for **MTab** components to stack vertically.

<masa-example file="Examples.components.tabs.VerticalTabs"></masa-example>

### Misc

#### Content

It is common to put **MTabs** inside the extension slot of **MToolbar**. Using **MToolbar**'s tabs prop auto adjusts its height to 48px to match `MTabs`.

<masa-example file="Examples.components.tabs.Content"></masa-example>

#### Desktop Tabs

You can represent **MTabs** actions by using single icons. This is useful when it is easy to correlate content to each tab.

<masa-example file="Examples.components.tabs.DesktopTabs"></masa-example>

#### Dynamic Height

When changing your **MTabItem**, the content area will smoothly scale to the new size.

<masa-example file="Examples.components.tabs.DynamicHeight"></masa-example>

#### Dynamic Tabs

Tabs can be dynamically added and removed. This allows you to update to any number and the **MTabs** component will react. In this example when we add a new tab, we automatically change our model to match. As we add more tabs and overflow the container, the selected item will be automatically scrolled into view. Remove all **MTabs** and the slider will disappear.

<masa-example file="Examples.components.tabs.DynamicTabs"></masa-example>

#### Overflow to Menu

You can use a menu to hold additional tabs, swapping them out on the fly.

<masa-example file="Examples.components.tabs.OverflowToMenu"></masa-example>

#### TabItems

The **MTabsItems** component allows for you to customize the content per tab. Using a shared variable, the **MTabsItems** will sync with the currently selected **MTab**.

<masa-example file="Examples.components.tabs.TabItems"></masa-example>