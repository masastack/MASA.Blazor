---
title: Enqueued snackbars（消息队列）
desc: "允许消息条堆叠在一起。"
tag: "预置"
related:
  - /blazor/components/snackbars
  - /blazor/components/dialogs
  - /blazor/components/popup-service
---

## 使用

允许您设置消息队列的`Position`，是否可以关闭和最大队列数量。

<enqueued-snackbars-usage></enqueued-snackbars-usage>

## 示例

### 属性

#### 类型

**PEnqueuedSnackbars** 使用了一个增强的消息条，其中嵌套了一个 **MAlert** 组件，可以设置`Title`、`Content`和`Type`。

<masa-example file="Examples.components.enqueued_snackbars.Type"></masa-example>

### 事件

#### 操作

当用户操作完之后你可以弹出了一个提示，并且提供可选操作，比如示例里的*撤销（Undo）*。

<masa-example file="Examples.components.enqueued_snackbars.Action"></masa-example>
