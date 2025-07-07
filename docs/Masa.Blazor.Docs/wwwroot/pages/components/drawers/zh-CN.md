---
title: Drawers（抽屉）
desc: ""
tag: "预置"
related:
  - /blazor/components/dialogs
  - /blazor/components/popup-service
  - /blazor/components/modals
---

## 使用 {#usage}

<masa-example file="Examples.components.drawers.Usage"></masa-example>

## 示例 {#examples}

### 属性 {#props}

#### 操作属性 {#action-props}

<masa-example file="Examples.components.drawers.ActionProps"></masa-example>

#### 操作 {#actions}

<masa-example file="Examples.components.drawers.Actions"></masa-example>

#### 表单对象 {#form-model updated-in=v1.10.0}

`FormModel` 属性同 [MForm](/blazor/components/forms) 组件的 `Model` 属性，
允许在抽屉中内置表单验证功能，`OnSave` 事件会在表单验证通过后触发。
`OnValidating` 事件可以拿到表单验证结果。

<masa-example file="Examples.components.drawers.FormModel"></masa-example>

#### 左侧抽屉 {#left}

<masa-example file="Examples.components.drawers.Left"></masa-example>

#### 自动滚动到顶部 {#autoScrollToTop}

<masa-example file="Examples.components.drawers.ScrollToTopOnHide"></masa-example>

#### 没有触发器 {#without-activator}

<masa-example file="Examples.components.drawers.WithoutActivator"></masa-example>

### 插槽 {#contents}

#### 自定义操作 {#custom-actions}

<masa-example file="Examples.components.drawers.CustomActions"></masa-example>
