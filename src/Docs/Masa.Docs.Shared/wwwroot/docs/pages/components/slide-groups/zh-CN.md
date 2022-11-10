---
title: Slide Groups（幻灯片组）
desc: "**MSlideGroup** 组件用于显示伪分页信息。它以 [**MItemGroup**](/components/item-groups) 为核心，为 [**MTabs**](/components/tabs) 和 [**MChipGroup**](/components/chip-groups) 等组件提供基础。"
related:
  - /components/icons
  - /components/carousels
  - /components/tabs
---

## 使用

类似于 **MWindow** 组件，**MSideGroup** 允许项目根据需要占用尽可能多的空间，允许用户在提供的信息中水平移动。

<slide-groups-usage></slide-groups-usage>

## 示例

### 属性

#### 激活类

`ActiveClass` 属性允许您在激活的项上设置自定义的 CSS 类。

<example file="" />

#### 激活项居中

使用 `CenterActive` 参数将使活动的项目永远居中。

<example file="" />

#### 自定义图标

您可以使用 `NextIcon` 和 `PrevIcon` 添加自定义分页图标代替箭头。

<example file="" />

#### 必填项

`Mandatory` 将使幻灯片组需要至少选择一个项目。

<example file="" />

#### 多选

您可以通过设置 `Multiple` 来选择多个项目。

<example file="" />

### 其他

#### 伪轮播

自定义幻灯片组以在图表上创造性地显示信息。 使用此选项，可以方便地为用户显示辅助信息。

<example file="" />