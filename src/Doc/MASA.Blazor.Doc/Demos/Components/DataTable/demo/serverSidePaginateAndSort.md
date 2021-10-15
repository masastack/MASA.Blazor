---
order: 3
title:
  zh-CN: 服务器端分页和排序
  en-US: Server-sidePaginateAndSort
---

## zh-CN

如果你正在从后端服务器加载已经分页和排序的数据，你可以使用 ServerItemsLength 属性。 使用这个属性会禁用内置的排序和分页，因此，你需要用特定事件（OnPageUpdate，OnSortByUpdate，OnOptionsUpdate 等）来得知什么时候要向后端服务器请求新页面。 获取数据时，使用 loading 属性显示进度条。

## en-US

TODO...
