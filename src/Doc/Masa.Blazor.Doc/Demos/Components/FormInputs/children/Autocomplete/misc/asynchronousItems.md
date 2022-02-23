---
order: 0
title:
  zh-CN: 异步项目
  en-US: AsynchronousItems
---

## zh-CN

有时您需要基于搜索查询加载外部的数据。 在赋值autocomplete的属性时使用search-input属性与**.sync**修饰符 我们还使用新的 'cache-items' 属性。 这将保持一个唯一的列表，它的所有项目都被传递到items属性。当使用异步项目和 多个 属性时是必须的 。

## en-US

Sometimes you need to load data externally based upon a search query. Use the 'search-input' prop with the **.sync** modifier when using the autocomplete prop. We also make use of the new 'cache-items' prop. This will keep a unique list of all items that have been passed to the items prop and is **REQUIRED** when using asynchronous items and the **multiple** prop.
