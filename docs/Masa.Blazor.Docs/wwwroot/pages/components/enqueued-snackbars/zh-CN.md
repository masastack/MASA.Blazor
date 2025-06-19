---
title: Enqueued snackbars（消息队列）
desc: "允许消息条堆叠在一起。"
tag: "预置"
related:
  - /blazor/components/snackbars
  - /blazor/components/dialogs
  - /blazor/components/popup-service
---

## 使用 {#usage}

允许您设置消息队列的`Position`，是否可以关闭和最大队列数量。

<enqueued-snackbars-usage></enqueued-snackbars-usage>

## 示例 {#examples}

### 属性 {#props}

#### 类型 {#type}

**PEnqueuedSnackbars** 使用了一个增强的消息条，其中嵌套了一个 **MAlert** 组件，可以设置`Title`、`Content`和`Type`。

<masa-example file="Examples.components.enqueued_snackbars.Type"></masa-example>

#### 重复消息过滤 {#filter-duplicate released-on=v1.10.0}

默认过滤重复消息，可以防止短时间内显示多个内容相同的提示消息，从而避免界面出现消息堆积的情况。

通过设置 `DuplicateMessageFilterDuration` 参数可以控制重复消息的过滤时间窗口（单位为毫秒）。在这个时间窗口内，如果出现标题和内容相同的消息，将会被自动过滤掉。默认值为 1000 毫秒（1秒）。

如果设置为 0，则会禁用重复消息过滤功能，所有消息都会被显示。

<masa-example file="Examples.components.enqueued_snackbars.FilterDuplicate"></masa-example>

### 事件 {#events}

#### 操作 {#onaction}

当用户操作完之后你可以弹出了一个提示，并且提供可选操作，比如示例里的*撤销（Undo）*。

<masa-example file="Examples.components.enqueued_snackbars.Action"></masa-example>
