---
title: App bars（应用栏）
desc: "**MAppBar** 组件对于任何图形用户界面 (GUI) 都是至关重要的，因为它通常是网站导航的主要来源。**MAppBar** 和 **MNavigationDrawer** 结合在一起为应用程序提供站点导航。"
related:
  - /components/buttons
  - /components/icons
  - /components/toolbars
---

## 使用

**MAppBar** 组件用于应用程序范围的操作和信息。

<app-bars-usage></app-bars-usage>

## API

- [MAppBar](/api/MAppBar)
- [MAppBarNavIcon](/api/MAppBarNavIcon)
- [MAppBarTitle](/api/MAppBarTitle)

## 功能组件

#### MAppBarNavIcon

专门为与 [MToolbar](/components/toolbars) 和 **MAppBar** 一起使用而创建的样式化图标按钮组件。 在工具栏的左侧显示为汉堡菜单，它通常用于控制导航抽屉的状态。 默认插槽可以用于自定义此组件的图标和功能。

#### MAppBarTitle
修改过的 [MToolbarTitle](/components/toolbars) 组件 ，用于配合 `ShrinkOnScroll` 属性使用。 在小屏幕上，**MToolbarTitle**
将被截断，但这个组件在展开时使用了绝对定位使其内容可见。 我们不建议您在没有使用 `ShrinkOnScroll` 属性时使用 **MAppBarTitle** 组件。确实是因为向此组件添加了resize事件，并进行了很多额外的计算。

## 注意事项

<!--alert:warning-->
当在 **MToolbar** 和 **MAppBar** 内部使用带有 `Icon` 属性的 **MButton** 时，它们将自动增大其尺寸并应用负边距，以确保根据Material设计规范的适当间距。
如果您选择将按钮包装在任何容器例如`div`中，则需要对该容器应用负边距，以便正确对齐它们。
<!--/alert:warning-->

## 示例

### 属性

#### 可折叠栏

借助 `Collapse` 和 `CollapseOnScroll` 属性，简单的实现toolbar与用户的交互。

<example file="" />

#### 紧凑

您可以使 **MAppBar** 更加紧凑。 紧凑应用栏的高度低于普通应用栏。

<example file="" />

#### 滚动时的高度(z轴)

使用 `ElevateOnScroll` 属性时，**MAppBar** 默认的高度为 0dp，当用户开始向下滚动时，高度会升至 4dp。

<example file="" />

#### 滚动时淡入淡出图像

**MAppBar** 的背景图像可以在滚动时淡出。使用 `FadeImgOnScroll` 属性进行此操作。

<example file="" />


#### 滚动隐藏

当设置了 `HideOnScroll` 属性，**MAppBar** 向下滚动时会被隐藏。

<example file="" />


#### 图像

MAppBar 可以包含背景图像。您可以通过 `Src` 属性设置。 如果您需要自定义 **MImage** ，应用栏将为您提供一个 **ImgContent** 插槽。

<example file="" />


#### 滚动反转

当使用 `InvertedScroll` 属性时，该条将隐藏，直到用户滚动超过指定的阈值。一旦超过阈值，**MAppBar** 将继续显示，直到用户向上滚动超过阈值。如果未提供 `ScrollThreshold` 值，将使用默认值0。

<example file="" />

#### 突出

一个带有 `Prominent` 属性的 **MAppBar** 可以被设置为随着用户滚动而收缩。 当用户滚动浏览内容时，这提供了一个平滑的过渡，以减少视觉空间占用。 收缩高度有 `Dense`（紧密，48px）和 `Short` （短，56px）两种选择。

<example file="" />

#### 滚动阈值

**MAppBar** 可以有滚动阈值。只有在通过 `ScrollThreshold` 属性定义像素量后，它才会开始对滚动做出反应。

<example file="" />

### 其他

#### 菜单

您可以通过添加 **MMenu** 来轻松地扩展应用栏的功能。 单击最后一个图标以查看其运行情况。

<example file="" />

#### 切换导航抽屉

使用函数式组件 **MAppBarNavIcon** 可以切换其他组件的状态，例如 **MNavigationDrawer**。

<example file="" />