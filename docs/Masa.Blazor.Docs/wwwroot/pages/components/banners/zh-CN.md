---
title: Banners（横幅）
desc: "**MBanner** 组件被用来作为向用户发送1-2动作的中间段描述信息。 它有两个变量单行和多行（默认展示多行）。 这些图标可以与您的消息和操作一起使用。" 
related:
  - /blazor/components/alerts
  - /blazor/components/icons
  - /blazor/components/snackbars
---

## 使用

横幅可以有 1-2 行文本、动作和图标。

<banners-usage></banners-usage>

## 组件结构解剖

建议在 `MBanner` 内部放置元素：

* 在最左边放置一个 `MBannerAvatar` 或 `MBannerIcon` 图标
* 将 `MBannerText` 放在任何可视内容的右侧
* 将 `MBannerActions` 放在文本内容的最右侧，底部偏移

![Banner Anatomy](https://cdn.masastack.com/stack/doc/masablazor/anatomy/banner-anatomy.png)

| 元素 / 区域 | 描述 |
| - | - |
| 1. 容器 | Banner容器包含所有 `MBanner` 组件 |
| 2. 头像 / 图标（可选） | 旨在改善视觉环境的主流媒体内容 |
| 3. 文本 | 用于显示文本和其他内联元素的内容区域 |
| 4. 操作（可选） |通常包含一个或多个 [MButton](blazor/components/buttons) 组件的内容区域 |

## 示例

### 属性

#### 单行亮色主题

单行文字的 **MBanner** 组件用于给用户传达少量信息，推荐在桌面端使用。为此， 您可以选择启用 `sticky` 属性，使您的 **MBanner**  组件可以被固定在屏幕上的某个位置上（注意：此项功能不适用于IE 11浏览器）。

<masa-example file="Examples.components.banners.SingleLine"></masa-example>

### 事件

#### 图标事件

横幅上的图标在点击时触发 `OnIconClick` 事件，该事件带有自定义图标插槽。

<masa-example file="Examples.components.banners.IconClick"></masa-example>

### 插槽

#### 行为

**Actions** 插槽在其范围内具有 `dismiss` 功能，你可以使用它来轻松地隐藏横幅。

<masa-example file="Examples.components.banners.Actions"></masa-example>

#### 图标

图标插槽允许你明确控制其包含的内容和功能。

<masa-example file="Examples.components.banners.Icon"></masa-example>

### 其他

#### 两行

两行 **MBanner** 可以用来放置更多的数据或数据，以向用户传达性质较大的信息。推荐在移动端使用。

<masa-example file="Examples.components.banners.TwoLine"></masa-example>




