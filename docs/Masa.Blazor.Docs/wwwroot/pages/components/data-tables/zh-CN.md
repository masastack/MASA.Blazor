---
title: Data tables（数据表格）
desc: "`MDataTable` 组件用于显示表格数据。 功能包括排序，搜索，分页，内容编辑和行选择。"
related:
  - /blazor/components/data-iterators
  - /blazor/components/simple-tables
  - /blazor/components/lists
---

## 使用

标准数据表格默认会将数据呈现为简单的行。

<masa-example file="Examples.components.data_tables.Usage"></masa-example>

## 示例

### 属性

#### 自定义过滤器

你可以向 `CustomFilter` 属性提供一个函数，覆盖 `Search` 属性的默认过滤。 如果你需要自定义特定列的过滤，你可以给表头数据项的 `Filter` 属性提供一个函数。
类型是`Func<object, string, TItem, bool>`。 即使没有提供 `Search` 属性，这个函数也会一直运作。 因此，你需要确保在不应用过滤器的情况下，函数会返回 true。

<masa-example file="Examples.components.data_tables.CustomFilter"></masa-example>

#### 紧凑

使用 `Dense` 属性，可以让数据表格表现为另一种样式。

<masa-example file="Examples.components.data_tables.Dense"></masa-example>

#### 可过滤

在搜索表格内容时，你可以设置表头数据项的 `Filterable` 属性为 false，禁止对应列的内容被包含在搜索结果内。 下面的示例中，不会被搜索到 Dessert 列的内容。

<masa-example file="Examples.components.data_tables.Filterable"></masa-example>

#### 表脚属性

**MDataTable** 使用 **MDataFooter** 组件渲染一个默认的表脚。 你可以使用 `FooterProps` 将属性传递给这个组件。

<masa-example file="Examples.components.data_tables.Footer"></masa-example>

#### 分组

你可以使用 `GroupBy` 和 `GroupDesc` 属性分组。 `ShowGroupBy` 属性将在默认表头中显示分组按钮。 你可以设置表头数据项的 `Groupable` 属性为 false ，禁用对应属性的分组。

<masa-example file="Examples.components.data_tables.Group"></masa-example>

#### 隐藏默认表头和表脚

你可以应用 `HideDefaultHeader` 和 `HideDefaultFooter` 属性，分别移除默认表头和表脚。

<masa-example file="Examples.components.data_tables.HideDefaultHeaderAndFooter"></masa-example>

#### 加载中

你可以使用 `Loading` 属性来表示正在加载表格数据。 即使表格中没有数据，也会显示一条加载信息。 加载信息可以使用 `LoadingText` 属性或 `LoadingContent` 插槽来自定义。

<masa-example file="Examples.components.data_tables.Loading"></masa-example>

#### 多列排序

使用 `MultiSort` 属性可以根据多列同时排序。 启用后，你可以向 `SortBy` 和 `SortDesc` 传递数组以控制排序。

<masa-example file="Examples.components.data_tables.MultiSort"></masa-example>

#### 行选择

`ShowSelect` 属性将在默认表头中渲染一个复选框以切换所有行是否被选择，同时也为每个默认行渲染一个复选框。你还可以使用 `SingleSelect` 属性，指定能同时选择多行还是只能选择一行。

<masa-example file="Examples.components.data_tables.RowSelection"></masa-example>

#### 搜索

数据表格还提供了 `Search` 属性以过滤数据。

<masa-example file="Examples.components.data_tables.Search"></masa-example>

#### 固定列 {released-on=v1.2.0}

使用 `Headers` 的 `Fixed` 属性可以固定列。

<masa-example file="Examples.components.data_tables.Fixed"></masa-example>

#### 斑马纹

明暗相间的条纹。

<masa-example file="Examples.components.data_tables.Stripe"></masa-example>

#### 调整大小 {released-on=v1.0.4}

使用 `ResizeMode` 属性可以调整列的大小。

<masa-example file="Examples.components.data_tables.ResizeMode"></masa-example>

### 属性

#### 表头

你可以使用插槽 `HeaderColContent` 来自定义某些列。

<masa-example file="Examples.components.data_tables.Header"></masa-example>

#### 项目

你可以使用动态插槽 **ItemColContent** 来自定义某些列。

<masa-example file="Examples.components.data_tables.Item"></masa-example>

#### 简单复选框

如果想要在数据表格的插槽模板中使用复选框，请使用 **MSimpleCheckbox** 组件，而不是 **MCheckbox** 组件。 **MSimplleChecbox** 组件被内部使用，跟随表头对齐方式。

<masa-example file="Examples.components.data_tables.SimpleCheckbox"></masa-example>

### 其他

#### CRUD操作

带 CRUD 操作的 **MDataTable** 使用 **MDialog** 组件来编辑每行数据。

<masa-example file="Examples.components.data_tables.CRUDActions"></masa-example>

#### 编辑用对话框

**MEditDialog** 组件可用于直接在 **MDataTable** 中编辑数据。 如果点击 **MEditDialog** 外部时不想关闭对话框，可以添加 `Persistent` 属性。

<masa-example file="Examples.components.data_tables.EditDialog"></masa-example>

#### 可展开行

`ShowExpand` 属性会在每个默认行上渲染一个展开图标。 你可以使用 `ItemDataTableExpandContent` 插槽来自定义。 添加一列 `Value="data-table-expand"` 到 **
Headers**
数组，就能自定义这个插槽的位置。 你还可以使用 `SingleExpand` 属性，指定能同时展开多行还是只能展开一行。 行需要唯一的Key才能进行展开，使用**ItemKey**进行指定。

<masa-example file="Examples.components.data_tables.ExpandableRow"></masa-example>

#### 外部分页

要在外部控制分页，可以使用单独的属性或使用 `Options` 属性。

<masa-example file="Examples.components.data_tables.ExternalPagination"></masa-example>

#### 外部排序

要在外部控制排序，可以使用单独的属性或使用 `Options` 属性。

<masa-example file="Examples.components.data_tables.ExternalSorting"></masa-example>

#### 服务器端分页和排序

如果你正在从后端服务器加载已经分页和排序的数据，你可以使用 `ServerItemsLength` 属性。 使用这个属性会禁用内置的排序和分页，因此，你需要用特定事件（`OnPageUpdate`，`
OnSortByUpdate`，`OnOptionsUpdate` 等）来得知什么时候要向后端服务器请求新页面。 获取数据时，使用 `Loading` 属性显示进度条。

<masa-example file="Examples.components.data_tables.ServerSidePaginateAndSort"></masa-example>