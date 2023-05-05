---
title: Overflow（溢出）
desc: "配置内容超出容器边界时如何溢出。"
---

### 其他

#### 溢出属性

**如何运行**

指定元素 `overflow`, `overflow-x` 或`overflow-y` 属性。 可以使用以下格式应用这些类：`{overflow}-{value}`。 其中**溢出**是指类型：`overflow`, `overflow-x` 或`overflow-y`，**值**可以是以下之一：`auto`, `hidden`或`visible`。

这是属性列表:

* **overflow-auto**
* **overflow-hidden**
* **overflow-visible**
* **overflow-x-auto**
* **overflow-x-hidden**
* **overflow-x-visible**
* **overflow-y-auto**
* **overflow-y-hidden**
* **overflow-y-visible**

`overflow-auto` 用于在元素内容溢出边界时向元素添加滚动条。 而 `overflow-hidden` 则用于剪辑任何溢出边界的内容。 `overflow-visible`将防止内容被剪裁，即使它溢出了边界。

<masa-example file="Examples.styles_and_animations.overflow.Property"></masa-example>

#### 横坐标溢出属性

如果需要，**overflow-x** 可用于指定元素的水平溢出。

<masa-example file="Examples.styles_and_animations.overflow.XProperty"></masa-example>