---
title: Treeview（树形视图）
desc: "MTreeview 组件适用于显示大量嵌套数据。"
related:
  - /blazor/components/lists
  - /blazor/components/list-item-groups
  - /blazor/components/timelines
---

## 使用 {#usage}

<masa-example file="Examples.components.treeview.Usage"></masa-example>

## 示例 {#examples}

### 属性 {#props}

#### 可激活 {#activatable}

树形视图节点可以将其激活。

<masa-example file="Examples.components.treeview.Activatable"></masa-example>

#### 颜色 {#color}

设置 `Color` 属性，您可以控制活动的树形视图节点的文本和背景颜色。

<masa-example file="Examples.components.treeview.Color"></masa-example>

#### 密集 {#dense}

`Dense` 属性激活密集模式，提供了更紧凑的布局，同时降低了项目的高度。

<masa-example file="Examples.components.treeview.Dense"></masa-example>

#### 可悬停的 {#hoverable}

`Hoverable` 属性令树形视图节点可以具有悬停效果。

<masa-example file="Examples.components.treeview.Hoverable"></masa-example>

#### 禁用项目 {#itemdisabled}

设置 `ItemDisabled` 属性，控制节点在设置为 true 时禁用节点。

<masa-example file="Examples.components.treeview.ItemDisabled"></masa-example>

#### 加载子项 {#loadchildren}

您可以通过提供回调到 `LoadChildren` 属性，来动态加载子数据。 此回调将在用户首次试图扩展一个包含一个空数组的子属性的项目时执行。

<masa-example file="Examples.components.treeview.LoadChildren"></masa-example>

#### 打开全部 {#openall}

设置 `OpenAll` 属性，树形视图节点可以在页面加载时预先打开。

<masa-example file="Examples.components.treeview.OpenAll"></masa-example>

#### 圆角 {#rounded}

设置 `Rounded` 属性，您可以使树视图节点变成圆角。

<masa-example file="Examples.components.treeview.Rounded"></masa-example>

#### 可选择 {#selectable updated-in=v1.7.0}

您可以轻松选择树形视图节点和子节点。

> 从 v1.7.0 开始，增加了 `SelectOnRowClick` 属性，且默认值为 `true`，以便在单击行时选择节点。

<masa-example file="Examples.components.treeview.Selectable"></masa-example>

#### 选择颜色 {#selectcolor}

设置 `SelectedColor` 属性，您可以控制所选节点复选框的颜色。

<masa-example file="Examples.components.treeview.SelectColor"></masa-example>

#### 选择类型 {#select-type updated-in=v1.3.0}

属性视图支持三种不同的选择类型。默认类型是 `Leaf`，它在 `@bind-Value` 数组中只包含选中的叶节点。但会渲染父节点状态为全选或半选(这一点需要考量)。第二种模式是 `Independent`，允许选择父节点，但每个节点都独立于其父节点。第三种模式是 `LeafButIndependentParent`。

<masa-example file="Examples.components.treeview.SelectType"></masa-example>

#### 形状 {#shaped}

设置 `Shaped` 形状的树形视图在节点的一侧具有圆形边界。

<masa-example file="Examples.components.treeview.Shaped"></masa-example>

### 插槽 {#slots}

#### 附加和标签 {#append-and-label}

使用 `LabelContent` 和 `AppendContent` 插槽，我们能够创建一个直观的文件浏览器。

<masa-example file="Examples.components.treeview.AppendAndLabel"></masa-example>

### 其他 {#others}

#### 搜索&过滤 {#search-and-filter}

使用 `Search` 属性，轻松过滤您的树形视图。 如果您需要区分大小写或模糊过滤，通过设置 `Filter` 树形，您可以轻松地应用您的自定义过滤功能。 这类似于 **MAutocomplete**。

<masa-example file="Examples.components.treeview.SearchAndFilter"></masa-example>

#### 可选择的图标 {#selectable-icons}

为你的可选择树形视图自定义 `选中(on)`, `未选中(off)` 以及 `半选(indeterminate)` 图标。与诸如API加载项目等其他高级功能合并。

<masa-example file="Examples.components.treeview.SelectableIcons"></masa-example>