---
title: Menus（菜单）
desc: "**MMenu** 组件在激活它的元素的位置上展示一个菜单。"
related:
  - /components/dialogs
  - /components/tooltips
  - /stylesandanimations/transitions
---

## 使用

请记住将激活菜单的元素放置在 **ActivatorContent** 中。

<menus-usage></menus-usage>

## 示例

### 属性

#### 绝对定位

菜单可以使用 `Absolute` 属性，将其绝对放置在激活器元素的顶部。 尝试点击图像上的任意位置。

<example file="" />

#### 没有激活器的绝对定位

菜单也可以通过使用 `Absolute` 以及 `PositionX` 和 `PositionY` 参数来在没有激活器的情况下使用。 尝试右键点击图像上的任何地方。

<example file="" />

#### 点击关闭

使用`CloseOnClick`属性失去焦点时可以关闭菜单。

<example file="" />

#### 点击内容关闭

使用`CloseOnContentClick`属性您可以配置在点击内容后是否关闭 **MMenu**。

<example file="" />

#### 禁用

使用`CloseOnContentClick`属性您可以禁用菜单。禁用的菜单无法打开。

<example file="" />

#### X偏移

菜单可以被X轴偏移，以使激活器可见。

<example file="" />

#### Y偏移

菜单可以被Y轴偏移，以使激活器可见。

<example file="" />

#### 悬停

设置使用 `OpenOnHover` 属性时，菜单会悬停时打开而不是点击时打开。

<example file="" />

#### 圆角

菜单可以通过 `Rounded` 属性设置为圆角。关于圆角样式的其他信息在 [Border Radius](/stylesandanimations/border-radius) 页。

<example file="" />

### 插槽

#### 同时使用激活器和提示

在 `RenderFragment` 语法中，嵌套的激活器，例如用 **MMenu** 和 **MTooltip** 附加到同一激活按钮，需要特定的设置才能正常运行。注意：此语法同样用于其他嵌套激活器，例如 **MDialog**
/ **MTooltip**。

<example file="" />

### 其他

#### 自定义过渡

Masa.Blazor 有三个标准过渡，**scale**，**slide-x** 和 **slide-y**。您也可以创建自己的过渡并作为过渡参数传递。

<example file="" />

#### 弹出菜单

菜单可以配置为在打开时为静态菜单，使其充当弹出菜单。当菜单内容中有多个交互式项目时，这很有用。

<example file="" />

#### 在组件中使用

菜单可以放在几乎任何组件中。

<example file="" />
