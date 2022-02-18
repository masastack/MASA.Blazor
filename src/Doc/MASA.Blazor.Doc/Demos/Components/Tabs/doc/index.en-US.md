---
category: Components
type: Tabs
title: Tabs
cols: 1
cover: https://gw.alipayobjects.com/zos/alicdn/5rWLU27so/Tabs.svg
related:
  - /components/icons
  - /components/toolbars
  - /components/windows
---

The `MTabs` component is used for hiding content behind a selectable item. This can also be used as a pseudo-navigation
for a page, where the tabs are links and the tab-items are the content.

## API

- [MTabs](/api/MTabs)
- [MTab](/api/MTab)
- [MTabItem](/api/MTabItem)
- [MTabsItems](/api/MTabsItems)

## Caveats

<!--alert:warning--> 
When using the **Dark** prop and **NOT** providing a custom **color**, the `MTabs` component will default its color to white.
<!--alert:warning--> 

<!--alert:warning--> 
When using `MTabItem`'s that contain required input fields you must use the **eager** prop in order to validate the required fields that are not yet visible.
<!--alert:warning--> 