---
category: Components
subtitle: 导航抽屉
type: 导航抽屉
title: Navigation drawers
cols: 1
cover: https://gw.alipayobjects.com/zos/alicdn/5rWLU27so/NavigationDrawer.svg
related:
  - /components/lists
  - /components/icons
  - /getting-started/wireframes
---

`MNavigationDrawer` 是用于导航应用程序的组件。 通常被包装在 `MCard` 元素中使用。

## API

- [MNavigationDrawer](/api/MNavigationDrawer)

## 注意

<!--alert:error-->
如果使用启用了 **App** 属性的 `MNavigationDrawer`，则不需要像示例中那样使用 **Absolute** 属性。
<!--/alert:error-->

<!--alert:info-->
**ExtensionOnHover** 参数不会改变**MMain**的内容区域。 要使内容区域响应**ExtensionOnHover**，请绑定**OnMiniVariantUpdate** 到数据。
<!--/alert:info-->