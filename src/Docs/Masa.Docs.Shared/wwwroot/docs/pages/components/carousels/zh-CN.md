---
title: 轮播
desc: "`MCarousel` 组件用于在循环计时器上显示大量可视内容。"
related:
  - /components/parallax
  - /components/images
  - /components/windows
---

## 使用

**MCarousel** 组件通过提供旨在显示图像的附加功能来扩展 **MWindow**。

<carousels-usage></carousels-usage>

## 示例

### 属性

#### 自定义分隔符

使用任何可用的图标作为轮播的幻灯片分隔符。

<masa-example file="Examples.carousels.CustomDelimiters"></masa-example>

#### 自定义箭头

可以使用 **PrevContent** 和 **NextContent** 内容自定义箭头。

<masa-example file="Examples.carousels.CustomizedArrows"></masa-example>

#### 自定义过渡

**MCarouselItem** 组件可以更改其转换/反向转换。

<masa-example file="Examples.carousels.CustomTransition"></masa-example>

#### 循环

使用 `Cycle` 参数，您可以让幻灯片每 6 秒自动转换到下一个可用（默认）。

<masa-example file="Examples.carousels.Cycle"></masa-example>

#### 隐藏控件

您可以使用 `ShowArrows="false"` 隐藏轮播导航控件。

<masa-example file="Examples.carousels.HideControls"></masa-example>

#### 隐藏分隔符

您可以使用 **HideDelimiters** 参数隐藏底部控件。

<masa-example file="Examples.carousels.HideDelimiters"></masa-example>

#### 模型

您可以使用 `Value` 控制轮播。

<masa-example file="Examples.carousels.Model"></masa-example>
