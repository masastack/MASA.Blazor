---
title: Overlays（遮罩层）
desc: "**Overlay** 组件用于强调特定元素或其中的一部分。 它向用户发出应用程序内状态更改的信号，可用于创建加载程序，对话框等。"
related:
  - /components/cards
  - /components/sheets
  - /components/dialogs
---

## 使用

以最简单的形式，**MOverlay** 组件将在您的应用程序上添加暗淡的图层。

<overlays-usage></overlays-usage>

## 示例

### 属性

#### 绝对定位

使用`Absolute`属性会使遮罩层被放置在绝对位置，并包含在父元素中。

<example file="" />

#### 透明度

使用`Opacity`属性允许您自定义组件的透明度。

<example file="" />

#### Z-index

使用`ZIndex`属性使您能够轻松地更改 **MOverlay** 组件的覆盖顺序。

<example file="" />

### 其他

#### 高级版

使用 [MHover](/components/hover) 作为背景，添加进度组件来轻松创建自定义加载器。

<example file="" />

#### 加载器

使用 **MOverlay** 作为背景，添加进度组件来轻松创建自定义加载器。

<example file="" />