---
title: Dialogs（对话框）
desc: "**Dialog** 组件通知用户有关特定任务，且可能包含关键信息、需要决策或涉及多个任务。减少使用对话框的频率，因为它们具有干扰性。"
related:
  - /components/buttons
  - /components/cards
  - /components/menus
---

## 使用

对话框包含两个插槽，一个用于它的激活器，另一个用于它的内容（默认）。 有利于隐私政策。

<dialogs-usage></dialogs-usage>

## 示例

### 属性

#### 全屏对话框

由于屏幕空间有限，相对于使用普通对话框的大屏设备，全屏对话框更适合移动设备。

<example file="" />

#### 过渡动画

您可以让对话框从顶部或底部出现。

<example file="" />

#### 保留

与普通对话框相似，但当用户点击对话框外部或按下 esc 键时，对话框不会关闭。

<example file="" />

#### 可滚动

一个可滚动内容的对话框示例。

<example file="" />

### 其他

#### 表单

一个简单的表单对话框的例子。

<example file="" />

#### 加载器

The **MDialog** component makes it easy to create a customized loading experience for your application.

<example file="" />

#### 嵌套

对话框可以嵌套：可以从一个对话框打开另一个对话框。

<example file="" />

#### 溢出

若对话框内容溢出，将在对话框内显示滚动条。

<example file="" />

#### 没有激活器

如果因为某些原因不能使用激活器插槽，请确保将 `StopPropagation` 修饰符添加到触发对话框的事件。

<example file="" />