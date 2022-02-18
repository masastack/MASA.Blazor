---
category: Components
type: NavigationDrawer
title: Navigation drawers
cols: 1
cover: https://gw.alipayobjects.com/zos/alicdn/5rWLU27so/NavigationDrawer.svg
related:
  - /components/lists
  - /components/icons
  - /getting-started/wireframes
---

`MNavigationDrawer` is a component used to navigate applications. It is usually wrapped and used in the `MCard` element.

## Caveats

<!--alert:error-->
If you are using `MNavigationDrawer` with **App** property enabled, you don't need to use **Absolute** prop as in examples.
<!--/alert:error-->

<!--alert:info-->
The **ExpandOnHover** prop does not alter the content area of **MMain**. To have content area respond to **ExpandOnHover**, bind **OnMiniVariantUpdate** to a data prop.
<!--/alert:info-->