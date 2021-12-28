---
order: 9
title:
  zh-CN: Flex 增长系数和收缩系数
  en-US: GrowShrink
---

## zh-CN

MASA Blazor 有用于手动应用增长和收缩系数的辅助类. 通过添加 `flex-{condition}-{value}` 格式的辅助类来使用。 condition 可以是 `grow` 或 `shrink` 两者之一, value可以是 `0` 或 `1` 两者之一。 `grow` 将允许元素增长以填充可用的空间, 然而 `shrink` 将允许项目收缩到它的内容所需要的空间. 但是，只有当项目必须收缩以适合其容器时才会发生这种情况，例如容器大小调整或受到 `flex-grow-1` 的影响。 值`0`将阻止该条件的发生，而`1`则允许出现这种情况。 以下类可用：

* **flex-grow-0**
* **flex-grow-1**
* **flex-shrink-0**
* **flex-shrink-1**

这些辅助类同样可以基于断点以 `flex-{breakpoint}-{condition}-{state}` 的格式创建更多的弹性变量. 以下组合可用：

* **flex-sm-grow-0**
* **flex-md-grow-0**
* **flex-lg-grow-0**
* **flex-xl-grow-0**
* **flex-sm-grow-1**
* **flex-md-grow-1**
* **flex-lg-grow-1**
* **flex-xl-grow-1**
* **flex-sm-shrink-0**
* **flex-md-shrink-0**
* **flex-lg-shrink-0**
* **flex-xl-shrink-0**
* **flex-sm-shrink-1**
* **flex-md-shrink-1**
* **flex-lg-shrink-1**
* **flex-xl-shrink-1**

## en-US

MASA Blazor has helper classes for manually applying growth and shrinkage factors. It can be used by adding helper classes in the format of `flex-{condition}-{value}`. The condition can be either `grow` or `shrink`, value Can be either `0` or `1`. `grow` will allow the element to grow to fill the available space, while `shrink` will allow the item to shrink to the space required by its content. But only if the item must This happens only when it shrinks to fit its container, such as when the container is resized or affected by `flex-grow-1`. The value `0` will prevent this condition from happening, while `1` will allow it. The following classes are available:

* **flex-grow-0**
* **flex-grow-1**
* **flex-shrink-0**
* **flex-shrink-1**

These auxiliary classes can also create more flexible variables in the format of `flex-{breakpoint}-{condition}-{state}` based on breakpoints. The following combinations are available:

* **flex-sm-grow-0**
* **flex-md-grow-0**
* **flex-lg-grow-0**
* **flex-xl-grow-0**
* **flex-sm-grow-1**
* **flex-md-grow-1**
* **flex-lg-grow-1**
* **flex-xl-grow-1**
* **flex-sm-shrink-0**
* **flex-md-shrink-0**
* **flex-lg-shrink-0**
* **flex-xl-shrink-0**
* **flex-sm-shrink-1**
* **flex-md-shrink-1**
* **flex-lg-shrink-1**
* **flex-xl-shrink-1**