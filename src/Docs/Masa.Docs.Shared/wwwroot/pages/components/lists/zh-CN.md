---
title: Lists（列表）
desc: "**MList** 组件用于显示信息。 它可以包含头像、内容、操作、列表组标题等等。 列表以易于在集合中识别特定项目的方式显示内容。 它们为组织一组文本和图像提供了一致的样式。"
related:
  - /components/item-groups
  - /components/list-item-groups
  - /components/subheaders
---

## 使用

列表有三种基本形式。 单行 (默认), 双行 和 三行. 行声明指定了项目的最小高度，也可以使用相同的属性从 **MList** 中进行控制。

<lists-usage></lists-usage>

## 注意

<!--alert:info-->
如果您要查找有状态列表项，请查看 [**MListItemGroup**](/components/list-item-groups)。

## 示例

### 属性

#### 密集

`List` 可以通过 `Dense` 属性变密集。

<example file="" />

#### 禁用

您不能与已禁用的 **MList** 交互。

<example file="" />

#### 扁平

在 `Flat` 属性的 **MList** 中选择时，项目不会发生变化。

<example file="" />

#### 导航列表

列表可以接受一个替代的 `Nav` 样式，它减少了 **MListItem** 的宽度，并增加了一个边框半径。

<example file="" />

#### 圆角

你可以让 **MList** 项变成圆角。

<example file="" />

#### 形状列表

形状列表在 **MListItem** 的一侧具有圆形边界。

<example file="" />

#### 嵌套列表

使用 **MListGroup** 组件，您可以使用 `SubGroup` 属性创建多达 2 级的深度。

<example file="" />

#### 三行

对于三行列表，字幕将垂直夹在 2 行，然后省略。

<example file="" />

#### 两行和副标题

列表组件可以包含列表组标题，分割线，以及1行或者更多行，如果副标题文本溢出则会以省略号的形式截断文本。

<example file="" />

### 插槽

#### 可展开的列表

列表可以包含一组项目，这些项目将在单击**MListGroup**的`ActivatorContent`时显示。扩展列表也用于 [MNavigationDrawer](/components/navigation-drawers) 组件中。

<example file="" />

### 其他

#### 操作和项目组

可操作的 **ThreeLine** 列表。 利用 [**MListItemGroup**](/components/list-item-groups)，轻松将动作连接到图块。

<example file="" />

#### 操作栈

列表组件可以包含一个操作栈。当你需要在你的动作项目旁边显示数据文本时，这是非常有用的。

<example file="" />

#### 卡片列表

一个列表可以和一张卡片结合起来。

<example file="" />

#### 简单头像列表

一个简单的列表利用 **MistItemIcon**, **MListItemTitle** 和 **MListItemAvatar**。

<example file="" />

#### 单行列表

在这里，我们结合了单行列表中的 **MListItemAvatar** 和 **MistItemIcon**。

<example file="" />

#### 列表组标题和分割线

列表可以包含多个子标头和分隔符。

<example file="" />