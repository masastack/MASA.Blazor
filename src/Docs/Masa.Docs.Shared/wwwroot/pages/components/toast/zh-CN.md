---
category: Components
subtitle: 吐司
type: 吐司
title: Toast
cols: 1
tag: preset
related:
  - /components/buttons
  - /components/icons
  - /components/snackbars
---

该组件用于通过使用上下文类型，图标和颜色向用户传达重要信息。这些默认类型有4种变体：`Success`、`Info`、`Warning` 和 `Error`。默认图标有助于表示每种类型所描述的不同动作，也可以自定义提示框的许多部分。

## 使用

**PToast** 组件可以通过PopupService来使用，具体请查看 [PopupService](/components/popup-service) 的文档。

<toast-usage></toast-usage>

## 示例

### 属性

#### 自动关闭时间（默认4000ms）

<example file="" />

#### 显示最大条数

`MaxCount` 属性设置最大展示数量。

<example file="" />

#### 显示位置

`Value` 属性设置消息弹出的位置。

<example file="" />

### 事件

#### 自定义 Toast 内容

<example file="" />