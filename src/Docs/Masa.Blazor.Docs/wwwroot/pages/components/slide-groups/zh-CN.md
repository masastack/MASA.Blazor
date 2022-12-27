---
title: Slide Groups（幻灯片组）
desc: "**MSlideGroup** 组件用于显示伪分页信息。它以 [MItemGroup](/blazor/components/item-groups) 为核心，为 [MTabs](/blazor/components/tabs) 和 [MChipGroup](/blazor/components/chip-groups) 等组件提供基础。"
related:
  - /blazor/components/icons
  - /blazor/components/carousels
  - /blazor/components/tabs
---

## 使用

类似于 **MWindow** 组件，**MSideGroup** 允许项目根据需要占用尽可能多的空间，允许用户在提供的信息中水平移动。

<masa-example file="Examples.components.slide_groups.Usage"></masa-example>

## 示例

### 属性

#### 激活类

使用 `ActiveClass` 属性将允许您在激活的项上设置自定义的 CSS 类。

<masa-example file="Examples.components.slide_groups.ActiveClass"></masa-example>

#### 激活项居中

您可以使用 `CenterActive` 属性使活动的项目永远居中。

<masa-example file="Examples.components.slide_groups.CenterActive"></masa-example>

#### 自定义图标

您可以使用 `NextIcon` 和 `PrevIcon` 添加自定义分页图标代替箭头。

<masa-example file="Examples.components.slide_groups.CustomIcons"></masa-example>

#### 必填项

您可以使用 `Mandatory` 属性使幻灯片组需要至少选择一个项目。

<masa-example file="Examples.components.slide_groups.Mandatory"></masa-example>

#### 多选

您可以通过设置 `Multiple` 来选择多个项目。

<masa-example file="Examples.components.slide_groups.Multiple"></masa-example>

### 其他

#### 伪轮播

自定义幻灯片组以在图表上创造性地显示信息。 使用此选项，可以方便地为用户显示辅助信息。

<masa-example file="Examples.components.slide_groups.PseudoCarousel"></masa-example>