---
order: 3
title:
  zh-CN: 服务器端分页和排序
  en-US: Server-side paginate and sort
---

## zh-CN

如果你正在从后端服务器加载已经分页和排序的数据，你可以使用 **ServerItemsLength** 属性。 使用这个属性会禁用内置的排序和分页，因此，你需要用特定事件（**OnPageUpdate**，**
OnSortByUpdate**，**OnOptionsUpdate** 等）来得知什么时候要向后端服务器请求新页面。 获取数据时，使用 **Loading** 属性显示进度条。

## en-US

If you’re loading data already paginated and sorted from a backend, you can use the **ServerItemsLength** prop. Defining
this prop will disable the built-in sorting and pagination, and you will instead need to use the available events (
**OnPageUpdate**, **OnSortByUpdate**, **OnOptionsUpdate**, etc) to know when to request new pages from your backend. Use
the **Loading** prop to display a progress bar while fetching data.
