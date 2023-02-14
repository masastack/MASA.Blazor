---
title: Tabs（选项卡）
desc: "**MTabs** 组件用于将内容隐藏在可选择的项目后面。 这也可以用作页面的伪导航，其中选项卡是链接，选项卡项是内容。"
related:
  - /blazor/components/icons
  - /blazor/components/toolbars
  - /blazor/components/windows
---

## 使用

**MTabs** 组件是 [MItemGroup](/blazor/components/item-groups) 的样式扩展。 它提供了一个易于使用的接口来组织内容组。

<masa-example file="Examples.components.tabs.Usage"></masa-example>

## 注意

<app-alert type="warning" content="当使用 `Dark` 属性且不提供自定义 `Color` 时，**MTabs**组件会将其颜色默认为 white。"></app-alert>

<app-alert type="warning" content="当使用包含必填输入字段的 **MTabItem** 时，你必须使用 `eager` 属性来验证尚未显示的必填字段。"></app-alert>

## 示例

### 属性

#### 对齐标题

使用 `AlignWithTitle` 属性将 **MTabs** 与 **MToolbarTitle** 对齐（**MAppBarNavIcon** 或 **MButton** 必须在 **MToolbar** 中使用)。

<masa-example file="Examples.components.tabs.AlignWithTitle"></masa-example>

#### 激活项居中

`CenterActive` 属性将使活动标签始终居中。

<masa-example file="Examples.components.tabs.CenterActive"></masa-example>

#### 自定义分隔符

`PrevIcon` 和 `NextIcon` 可以用于应用自定义分页图标。

<masa-example file="Examples.components.tabs.CustomIcons"></masa-example>

#### 固定选项卡

`FixedTabs` 属性迫使 **MTab** 占用所有可用的空间，直到最大宽度(300px)。

<masa-example file="Examples.components.tabs.FixedTabs"></masa-example>

#### 增长

`Grow` 属性将使选项卡项目占用所有可用的空间，最大宽度为300px。

<masa-example file="Examples.components.tabs.Grow"></masa-example>

#### 图标和文本

使用 `IconsWithText` 属性将 **MTabs** 的高度增加到72px，以便图标和文本都能被使用。

<masa-example file="Examples.components.tabs.IconAndText"></masa-example>

#### 分页

如果选项卡项溢出它们的容器，分页控件将出现在桌面上。对于移动设备，箭头只会与 `ShowArrows` 属性一起显示。

<masa-example file="Examples.components.tabs.Pagination"></masa-example>

#### 右对齐

`Right` 属性将选项卡对齐到右边。

<masa-example file="Examples.components.tabs.Right"></masa-example>

#### 垂直标签页

`Vertical` 属性允许 **MTab** 组件垂直堆叠。

<masa-example file="Examples.components.tabs.VerticalTabs"></masa-example>

### 其他

#### 内容

在 **MToolbar** 的扩展槽中放置 **MTabs** 是很常见的。使用 **MToolbar** 的tabs属性自动将其高度调整为48px以匹配 **MTabs**。

<masa-example file="Examples.components.tabs.Content"></masa-example>

#### 桌面选项卡

您可以使用单个图标表示 **MTabs** 动作。当很容易将内容关联到每个选项卡时，这非常有用。

<masa-example file="Examples.components.tabs.DesktopTabs"></masa-example>

#### 动态高度

更改 **MTabItem**，内容区域将平滑缩放到新大小。

<masa-example file="Examples.components.tabs.DynamicHeight"></masa-example>

#### 动态标签

可以动态添加和删除选项卡。这允许您更新到任何数字，**MTabs** 组件将做出反应。在本例中，当我们添加一个新选项卡时，我们会自动更改模型以匹配。当我们添加更多选项卡并溢出容器时，所选项目将自动滚动到视图中。删除所有`MTAB`，滑块将消失。

<masa-example file="Examples.components.tabs.DynamicTabs"></masa-example>

#### 溢出到菜单

您可以使用菜单来保存其他选项卡，并在运行时将其交换。

<masa-example file="Examples.components.tabs.OverflowToMenu"></masa-example>

#### 标签项

**MTabsItems** 组件允许您自定义每个选项卡的内容。使用共享变量，**MTabsItems**将与当前选定的**MTab**同步。

<masa-example file="Examples.components.tabs.TabItems"></masa-example>