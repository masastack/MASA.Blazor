---
title: Data iterators（数据迭代器）
desc: "`MDataIterator` 组件用于显示数据，并且与 `MDataTable` 组件共享其大部分功能。 功能包括排序、搜索、分页和选择。"
related:
  - /components/data-tables
  - /components/simple-tables
  - /components/toolbars
---

## 使用

<data-iterators-usage></data-iterators-usage>

## 示例

### 属性

#### 默认插槽

**MDataIterator** 的内部具有选择和扩展状态，就像 **MDataTable** 一样。 在这个示例中，我们在默认插槽上使用使用 `isExpanded` 和 `expand` 。

<example file="" />

#### 页眉和页脚

**MDataIterator** 有用于添加额外内容的 `**`HeaderContent` 和 `FooterContent` 插槽。

<example file="" />

### 其它

### 过滤器

排序，过滤和分页可以从外部使用单独的属性进行控制。

<example file="" />