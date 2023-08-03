---
title: Lists（列表）
desc: "**MList** 组件用于显示信息。 它可以包含头像、内容、操作、列表组标题等等。 列表以易于在集合中识别特定项目的方式显示内容。 它们为组织一组文本和图像提供了一致的样式。"
related:
  - /blazor/components/item-groups
  - /blazor/components/list-item-groups
  - /blazor/components/subheaders
---

## 使用

列表有三种基本形式。 单行 (默认), 双行 和 三行. 行声明指定了项目的最小高度，也可以使用相同的属性从 **MList** 中进行控制。

<lists-usage></lists-usage>

## 注意

> 如果你想让 **MListItem** 的 `Href` 属性与路由联动，需要在 **MList** 上应用 `Routable` 属性。

> 如果您要查找有状态列表项，请查看 [**MListItemGroup**](/blazor/components/list-item-groups)。

## 示例

### 属性

#### 密集

可以通过 `Dense` 属性变密集。

<masa-example file="Examples.components.lists.Dense"></masa-example>

#### 禁用

您不能与已禁用的 **MList** 交互。

<masa-example file="Examples.components.lists.Disabled"></masa-example>

#### 扁平

在应用了 `Flat` 属性的 **MList** 组件中选择时，项目不会发生变化。

<masa-example file="Examples.components.lists.Flat"></masa-example>

#### 导航列表

列表可以接受一个替代的 `Nav` 样式，它减少了 **MListItem** 的宽度，并增加了一个边框半径。

<masa-example file="Examples.components.lists.Nav"></masa-example>

#### 圆角

您可以使用 `Rounded` 属性让 **MList** 组件选中状态样式变成圆角。

<masa-example file="Examples.components.lists.Rounded"></masa-example>

#### 形状列表

形状列表在 **MListItem** 的一侧具有圆形边界。

<masa-example file="Examples.components.lists.ShapedLists"></masa-example>

#### 嵌套列表

使用 **MListGroup** 组件，您可以使用 `SubGroup` 属性创建多达 2 级的深度。

<masa-example file="Examples.components.lists.SubGroup"></masa-example>

#### 三行

对于三行列表，字幕将垂直夹在 2 行，然后省略。

<masa-example file="Examples.components.lists.ThreeLine"></masa-example>

#### 两行和副标题

列表组件可以包含列表组标题，分割线，以及1行或者更多行，如果副标题文本溢出则会以省略号的形式截断文本。

<masa-example file="Examples.components.lists.TwoLinesAndSubheader"></masa-example>

### 插槽

#### 可展开的列表

列表可以包含一组项目，这些项目将在单击**MListGroup**的`ActivatorContent`时显示。扩展列表也用于 [MNavigationDrawer](/blazor/components/navigation-drawers) 组件中。

<masa-example file="Examples.components.lists.ExpansionLists"></masa-example>

### 其他

#### 操作和项目组

可操作的 **ThreeLine** 列表。 利用 [MListItemGroup](/blazor/components/list-item-groups)，轻松将动作连接到图块。

<masa-example file="Examples.components.lists.ActionsAndItemGroups"></masa-example>

#### 操作栈

列表组件可以包含一个操作栈。当你需要在你的动作项目旁边显示数据文本时，这是非常有用的。

<masa-example file="Examples.components.lists.ActionStack"></masa-example>

#### 卡片列表

一个列表可以和一张卡片结合起来。

<masa-example file="Examples.components.lists.CardList"></masa-example>

#### 简单头像列表

一个简单的列表利用 **MistItemIcon**, **MListItemTitle** 和 **MListItemAvatar**。

<masa-example file="Examples.components.lists.SimpleAvatarList"></masa-example>

#### 单行列表

在这里，我们结合了单行列表中的 **MListItemAvatar** 和 **MistItemIcon**。

<masa-example file="Examples.components.lists.SingleLineList"></masa-example>

#### 列表组标题和分割线

列表可以包含多个子标头和分隔符。

<masa-example file="Examples.components.lists.SubheadingsAndDividers"></masa-example>