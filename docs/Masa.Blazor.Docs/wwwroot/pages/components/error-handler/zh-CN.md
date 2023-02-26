---
title: Error handler（异常处理）
desc: "`MErrorHandler`组件用来管理未经处理的异常。"
---

## 使用

<masa-example file="Examples.components.error_handler.Usage"></masa-example>

<app-alert type="warning" content="非MASABlazor组件内发生的异常能捕获但不能阻止状态刷新，如：`<button @onclick=''>throw exception</button>`。"></app-alert>

<app-alert content="生命周期里发生异常时会尝试使用 `ErrorContent` 代替。"></app-alert>

## 示例

### 属性

#### 错误提示类型

发生异常时默认使用 **Toasts** 显示错误提示。可以通过 `PopupType` 设置提示的类型。当设置 `ErrorPopupType.Error` 时则会使用 `ErrorContent` 处理错误。

<masa-example file="Examples.components.error_handler.PopupType"></masa-example>

#### 显示错误详情

通过 `ShowDetail` 控制是否显示异常详情。

<masa-example file="Examples.components.error_handler.ShowDetail"></masa-example>

### 插槽

#### 错误内容

<masa-example file="Examples.components.error_handler.ErrorContent"></masa-example>

### 事件

#### 自定义处理

`OnHandle` 会替代默认的处理程序，如果你需要使用自定义的错误处理程序时可以使用。`OnAfterHandle` 是异常处理之后调用的事件。

<masa-example file="Examples.components.error_handler.CustomHandler"></masa-example>
