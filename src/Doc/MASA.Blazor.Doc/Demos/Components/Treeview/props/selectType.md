---
order: 1
title:
  zh-CN: 选择类型
  en-US: Select type
---

## zh-CN

属性视图支持两种不同的选择类型。 默认类型是 **Leaf**，它在 **@bind-Value** 数组中只包含选中的叶节点。 但会渲染父节点状态为全选或半选(这一点需要考量)。 另一种模式是 **Independent**
，允许选择父节点，但每个节点都独立于其父节点。

## en-US

Treeview now supports two different selection types. The default type is **Leaf**, which will only include leaf nodes in
the **@bind-Value** array, but will render parent nodes as either partially or fully selected. The alternative mode is
**Independent**, which allows one to select parent nodes, but each node is independent of its parent and children.
