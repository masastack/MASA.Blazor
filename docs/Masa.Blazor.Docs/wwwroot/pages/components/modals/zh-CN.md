---
title: Modals（模态框）
desc: "需要用户处理事务，又不希望跳转页面以致打断工作流程时，可以使用 **Modal** 在当前页面正中打开一个浮层，承载相应的操作。"
tag: "预置"
related:
  - /blazor/components/dialogs
  - /blazor/components/popup-service
  - /blazor/components/drawers
---

## 使用 {#usage}

<masa-example file="Examples.components.modals.Usage"></masa-example>

## 示例 {#examples}

### 属性 {#props}

#### 操作属性 {#action-props}

<masa-example file="Examples.components.modals.ActionProps"></masa-example>

#### 操作 {#actions}

<masa-example file="Examples.components.modals.Actions"></masa-example>

#### 表单对象 {#form-model updated-in=v1.10.0}

`FormModel` 属性同 [MForm](/blazor/components/forms) 组件的 `Model` 属性，
允许在模态框中内置表单验证功能，`OnSave` 事件会在表单验证通过后触发。
`OnValidating` 事件可以拿到表单验证结果。

<masa-example file="Examples.components.modals.FormModel"></masa-example>

#### 自动滚动到顶部 {#autoScrollToTop}

<masa-example file="Examples.components.modals.ScrollToTopOnHide"></masa-example>

#### 没有触发器 {#without-activator}

<masa-example file="Examples.components.modals.WithoutActivator"></masa-example>

### 插槽 {#contents}

#### 自定义操作 {#custom-actions}

<masa-example file="Examples.components.modals.CustomActions"></masa-example>
