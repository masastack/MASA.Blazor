---
category: Components
subtitle: 时间轴
type: 时间轴
title: Timelines
cols: 1
related:
  - /components/cards
  - /components/icons
  - /components/grid-system
---

# Timelines（时间轴）

**MTimeline** 对于显示时间顺序信息非常有用。

## 使用

**MTimeline** 以最简单的形式显示了一个垂直时间轴, 它至少应该包含一个 **MTimelineItem**。

<timelines-usage></timelines-usage>

## API

- [MTimeline](/api/MTimeline)
- [MTimelineItem](/api/MTimelineItem)

## 示例

### 属性

#### 颜色

使用 **Color** 属性指定颜色可以创建可视的断点，使您的时间轴更容易阅读。

<example file="" />

#### 密集

**Dense** 的时间轴将所有内容置于右侧。在这个示例中，**MAlert** 代替卡片以提供不同的设计。

<example file="" />

#### 图标点

使用 **MTimelineItem**圆点内的图标来提供额外的上下文。

<example file="" />

#### 反转

您可以使用 `Reverse` 属性来反转时间轴项目的方向。这在默认模式和 `Dense` 模式下都可以工作。

<example file="" />

#### 小号

`Small` 属性允许不同的样式提供独特的设计。

<example file="" />


### 插槽

#### IconContent

使用 `IconContent`  和 **MAvatar** 将头像插入到圆点中。

<example file="" />

#### OppositeContent

`OppositeContent` 插槽在您的时间线内提供额外的自定义层。

<example file="" />

#### 默认

如果您将 **MCard** 放在 **MTimelineItem** 内，则卡的侧面会出现一个插入符号。

<example file="" />