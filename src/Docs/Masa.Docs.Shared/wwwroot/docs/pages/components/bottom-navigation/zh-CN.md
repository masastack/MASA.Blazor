---
title: Bottom navigation (底部导航栏)
desc: "`MBottomNavigation` 组件是侧边栏的替代品。 它主要用于移动应用程序，并且有三个变体：**icons**，**text** 和 **shift**。"
related:
  - /components/buttons
  - /components/icons
  - /components/tabs
---

## 使用

虽然 **MBottomNavigation** 导航旨在与路由一起使用，但您也可以通过使用**Value**属性以编程方式控制按钮的活动状态。使用 **MBottomNavigation** 为按钮指定其*索引*的缺省值。

<bottom-navigation-usage></bottom-navigation-usage>

## 示例

### 属性

#### 颜色

`Color` 参数将颜色应用于底部导航的背景。我们建议使用 `Light` 和 `Dark` 参数来适当对比文本颜色。

<example file="" />

#### 增长

使用 `Grow` 参数强制 [MButton](/components/buttons) 组件填充所有可用空间。根据[底部导航MD规范](https://material.io/components/bottom-navigation#specs)，按钮的最大宽度为 **168px**。

<example file="" />

#### 滚动时隐藏

使用 `HideOnScroll` 参数时，**MBottomNavigation** 组件在向上滚动时隐藏。这类似于[MAppBar](/components/app-bars)中支持的[滚动技术](https://material.io/archive/guidelines/patterns/scrolling-techniques.html)。在下面的示例中，上下滚动以查看此行为。

<example file="" />

#### 水平布局

使用 `Horizontal` 参数调整按钮和图标的样式。此选项将按钮文本定位为与提供的[MIcon](/components/icons)内联。

<example file="" />

#### 滚动阈值

修改 `ScrollThreshold` 参数以增加用户在隐藏 **MBottomNavigation** 之前必须滚动的距离。

<example file="" />

#### 位移

未激活时，`Shift` 参数会隐藏按钮文本。这为 **MBottomNavigation** 提供了另一种视觉样式。

<example file="" />

#### 切换

可以使用 `InputValue` 参数切换 **MBottomNavigation** 的显示状态。您还可以使用 **@bind-Value** 控制当前活动的按钮。

<example file="" />