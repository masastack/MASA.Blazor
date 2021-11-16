---
order: 3
title:
  zh-CN: 可展开行
  en-US: ExpandableRow
---

## zh-CN

`ShowExpand` 属性会在每个默认行上渲染一个展开图标。 你可以使用 `ItemDataTableExpandContent` 插槽来自定义。 添加一列 `Value="data-table-expand"` 到 **
Headers**
数组，就能自定义这个插槽的位置。 你还可以使用 `SingleExpand` 属性，指定能同时展开多行还是只能展开一行。 行需要唯一的Key才能进行展开，使用**ItemKey**进行指定。

## en-US

The `ShowExpand`  prop will render an expand icon on each default row. You can customize this with the
**ItemDataTableExpandContent** slot. The position of this slot can be customized by adding a column
with `Value="data-table-expand"` to the **Headers** array. You can also switch between allowing multiple expanded rows
at the same time or just one with the `SingleExpand` prop. Row items require a unique key property for expansion to
work, use **ItemKey** prop to specify.
