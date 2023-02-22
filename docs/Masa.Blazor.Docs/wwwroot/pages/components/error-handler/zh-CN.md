---
title: Error handler（异常处理）
desc: "`MErrorHandler`组件用来管理未经处理的异常。"
---

## 使用

<masa-example file="Examples.components.error_handler.Usage"></masa-example>

<app-alert type="warning" content="非MASABlazor组件内发生的异常能捕获但不能维持原有的状态，如：`<button @onclick=''>throw exception</button>`。"></app-alert>

<app-alert content="生命周期里发生异常时会尝试使用 `ErrorContent` 代替。"></app-alert>

## 示例

### 属性

#### 是否显示告警

发生异常时默认会以弹窗警告来处理。禁用 `ShowAlert` 时则会使用 `ErrorContent` 处理。

<masa-example file="Examples.components.error_handler.ShowAlert"></masa-example>

#### 是否显示异常详情

通过 `ShowDetail` 控制是否显示异常详情。

<masa-example file="Examples.components.error_handler.ShowDetail"></masa-example>

### 事件

#### 自定义处理

`OnHandle` 会替代默认的处理程序，如果你需要使用自定义的错误处理程序时可以使用。`OnAfterHandle` 是异常处理之后调用的事件。

<masa-example file="Examples.components.error_handler.CustomHandler"></masa-example>
