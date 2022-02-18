---
category: Components
subtitle: 工具栏
type: 工具栏
title: Toolbars
cols: 1
related:
  - /components/buttons
  - /components/footers
  - /components/tabs
---

`MToolbar` 组件对于任何 GUI 都是至关重要的，因为它通常是站点导航的主要来源。 工具栏组件与 [**MNavigationDrawer**](/components/navigation-drawers) 和 [**MCard**](/components/cards) 配合使用非常有效。

## API

- [MToolbar](/api/MToolbar)
- [MToolbarItems](/api/MToolbarItems)
- [MToolbarTitle](/api/MToolbarTitle)

## 注意

<!--alert:warning-->
当在 `MToolbar` 和 `MAppBar` 内部使用带有 `Icon` prop的 `MButton` 时，它们将自动增大其尺寸并应用负边距，以确保根据Material设计规范的适当间距。
如果您选择将您的按钮包装在任何容器中，例如`div`， 您需要对容器应用负边距，以便正确对齐。
<!--/alert:warning-->