---
title: Banners（横幅）
desc: "**MBanner** 组件被用来作为向用户发送1-2动作的中间段描述信息。 它有两个变量单行and多行(默认多行展示)。 这些图标可以与您的消息和操作一起使用。" 
related:
  - /components/alerts
  - /components/icons
  - /components/snackbars
---

## 使用

横幅可以有 1-2 行文本、动作和图标。

<banners-usage></banners-usage>

## 解剖学

## 示例

### 属性

#### 单行亮色主题

单行文字的 **MBanner** 组件用于给用户传达少量信息，推荐在桌面端使用。为此， 您可以选择启用 `sticky` 属性，使您的 **MBanner**  组件可以被固定在屏幕上的某个位置上（注意：此项功能不适用于IE 11浏览器）。

<example file="" />

### 事件

#### 图标事件

横幅上的图标在点击时触发 `OnIconClick` 事件，该事件带有自定义图标插槽。

<example file="" />

### 插槽

#### 行为

**Actions** 插槽在其范围内具有 `dismiss` 功能，你可以使用它来轻松地隐藏横幅。

<example file="" />

#### 图标

图标插槽允许你明确控制其包含的内容和功能。

<example file="" />

### 其他

#### 两行

两行 **MBanner** 可以用来放置更多的数据或数据，以向用户传达性质较大的信息。推荐在移动端使用。

<example file="" />




