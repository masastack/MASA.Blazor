---
title: Timelines（时间轴）
desc: "**MTimeline** 对于显示时间顺序信息非常有用。"
related:
  - /blazor/components/cards
  - /blazor/components/icons
  - /blazor/components/grid-system
---

# Timelines（时间轴）

**MTimeline** 对于显示时间顺序信息非常有用。

## 使用

**MTimeline** 以最简单的形式显示了一个垂直时间轴, 它至少应该包含一个 **MTimelineItem**。

<masa-example file="Examples.components.timelines.Usage"></masa-example>

## 示例

### 属性

#### 颜色

使用 `Color` 属性指定颜色可以创建可视的断点，使您的时间轴更容易阅读。

<masa-example file="Examples.components.timelines.Color"></masa-example>

#### 密集

`Dense` 的时间轴将所有内容置于右侧。在这个示例中，**MAlert** 代替卡片以提供不同的设计。

<masa-example file="Examples.components.timelines.Dense"></masa-example>

#### 图标点

使用 **MTimelineItem**圆点内的图标来提供额外的上下文。

<masa-example file="Examples.components.timelines.IconDots"></masa-example>

#### 反转

您可以使用 `Reverse` 属性来反转时间轴项目的方向。这在默认模式和 `Dense` 模式下都可以工作。

<masa-example file="Examples.components.timelines.Reverse"></masa-example>

#### 小号

`Small` 属性允许不同的样式提供独特的设计。

<masa-example file="Examples.components.timelines.Small"></masa-example>

### 插槽

#### IconContent

使用 **IconContent**  和 **MAvatar** 将头像插入到圆点中。

<masa-example file="Examples.components.timelines.IconContent"></masa-example>

#### OppositeContent

`OppositeContent` 插槽在您的时间线内提供额外的自定义层。

<masa-example file="Examples.components.timelines.OppositeContent"></masa-example>

#### 默认

如果您将 **MCard** 放在 **MTimelineItem** 内，则卡的侧面会出现一个插入符号。

<masa-example file="Examples.components.timelines.TimelineItemDefault"></masa-example>

### 其他

#### 高级用法

模块化组件允许您创建高度定制的解决方案。

<masa-example file="Examples.components.timelines.Advanced"></masa-example>
