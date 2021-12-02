---
category: Components
subtitle: 应用栏
type: 应用栏
title: App bars
cols: 1
cover: https://gw.alipayobjects.com/zos/alicdn/5rWLU27so/AppBars.svg
related:
  - /components/buttons
  - /components/icons
  - /components/toolbars
---

`MAppBar` 组件对于任何图形用户界面 (GUI) 都是至关重要的，因为它通常是网站导航的主要来源。`MAppBar` 和 `MNavigationDrawer` 结合在一起为应用程序提供站点导航。

## API

- [MAppBar](/api/MAppBar)
- [MAppBarNavIcon](/api/MAppBarNavIcon)
- [MAppBarTitle](/api/MAppBarTitle)

## 功能组件

- `MAppBarNavIcon`：专门为与 [MToolbar](/components/toolbars) 和 `MAppBar` 一起使用而创建的样式化图标按钮组件。 在工具栏的左侧显示为汉堡菜单，它通常用于控制导航抽屉的状态。 默认插槽可以用于自定义此组件的图标和功能。
- `MAppBarTitle`：修改过的 [MToolbarTitle](/components/toolbars) 组件 ，用于配合 **ShrinkOnScroll** 属性使用。 在小屏幕上，**MToolbarTitle**
  将被截断，但这个组件在展开时使用了绝对定位使其内容可见。 我们不建议您在没有使用 **ShrinkOnScroll** 属性时使用 `MAppBarTitle` 组件。确实是因为向此组件添加了resize事件，并进行了很多额外的计算。

## 注意事项

<!--alert:warning-->
当在 `MToolbar` 和 `MAppBar` 内部使用带有 **Icon** 属性的 `MButton` 时，它们将自动增大其尺寸并应用负边距，以确保根据Material设计规范的适当间距。
如果您选择将按钮包装在任何容器例如`div`中，则需要对该容器应用负边距，以便正确对齐它们。
<!--/alert:warning-->
