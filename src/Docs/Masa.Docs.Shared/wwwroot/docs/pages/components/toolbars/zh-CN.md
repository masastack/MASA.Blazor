---
title: Toolbars（工具栏）
desc: "**MToolbar** 组件对于任何 GUI 都是至关重要的，因为它通常是站点导航的主要来源。 工具栏组件与 [**MNavigationDrawer**](/components/navigation-drawers) 和 [**MCard**](/components/cards) 配合使用非常有效。"
related:
  - /components/buttons
  - /components/footers
  - /components/tabs
---

## 使用

工具栏是一个灵活的容器，可以通过多种方式使用。 默认情况下，工具栏在桌面上是64px高，在移动设备上是56px高。 有许多辅助组件可供工具栏使用。 **MToolbarTitle** 用于显示标题并且 **MToolbarItems** 允许 **MButton** 扩展全高度。

<toolbars-usage></toolbars-usage>

## 解剖学

## 注意

<!--alert:warning-->
当在 **MToolbar** 和 **MAppBar** 内部使用带有 **Icon** prop的 **MButton** 时，它们将自动增大其尺寸并应用负边距，以确保根据Material设计规范的适当间距。
如果您选择将您的按钮包装在任何容器中，例如 `div`， 您需要对容器应用负边距，以便正确对齐。
<!--/alert:warning-->

## 示例

### 属性

#### 背景

工具栏可以使用 src 属性显示背景而不是纯色。 这可以通过使用 img 插槽并提供您自己的 [**MImage**](/components/images) 组件来修改。
可以使用 [MAppBar](/components/app-bars) 使背景淡入淡出

<example file="" />

#### 折叠

可以折叠工具栏以节省屏幕空间。

<example file="" />

#### 紧凑工具栏

紧凑工具栏将其高度降低到 48px。 当与 prominent prop 同时使用时，将会将高度降低到 96px。

<example file="" />

#### 扩展

工具栏可以在不使用扩展插槽的情况下扩展。

<example file="" />

#### 扩展高度

扩展的高度可以定制。

<example file="" />

#### 搜索时浮动

浮动工具栏变成内联元素，只占用所需空间的。 将工具栏放置在内容上时这将特别有用。

<example file="" />

#### 浅色和深色

工具栏有 2 种变体，浅色和深色。 浅色工具栏具有深色按钮和深色文本，而深色工具栏具有白色按钮和白色文本。

<example file="" />

#### 突出的工具栏

突出的工具栏将 **MToolbar** 的高度增加到 128px ，并将 **MToolbarTitle** 放置到容器底部。 在 [**MApp**](/components/application) 中扩展了这个功能，能够将
prominent 的工具栏缩小到 **紧凑** 工具栏或 **短** 工具栏。

<example file="" />

### 其他

#### 上下文操作栏

可以更新工具栏的外观以响应应用程序状态的改变。 在本例中，工具栏的颜色和内容会随着用户在 **MSelect** 中的选择而改变。

<example file="" />

#### 灵活的卡片工具栏

在本例中，我们使用 `extended` prop 将卡片偏移到工具栏的扩展内容区域。

<example file="" />

#### 变量

**MToolbar** 有多个变量，可以应用主题和辅助类。 这些主题包括浅色和深色的主题、彩色和透明。

<example file="" />