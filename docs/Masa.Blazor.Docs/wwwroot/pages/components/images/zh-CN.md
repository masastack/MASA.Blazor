---
title: Images（图像）
desc: "**MImage**组件包含支持丰富媒体的功能。"
related:
  - /blazor/components/grid-system
  - /blazor/components/aspect-ratios
  - /blazor/components/parallax
---

## 使用

**MImage** 组件用于显示具有延迟加载和占位符的响应图像。

<images-usage></images-usage>

## 注意

<app-alert type="info" content="MImage 组件使用 Intersect 指令，IE11 和 Safari需要 Polyfill。 如果检测到浏览器不支持此功能, 图像仍将以正常方式加载。"></app-alert>

## 示例

### 属性

#### 宽高比

如果你想改变图像的长宽比，你可以设置`Width`属性来一个固定的宽高比。

<masa-example file="Examples.components.images.AspectRatio"></masa-example>

#### 包含

如果提供的长宽比与实际图像不匹配，则默认行为是填充尽可能多的空间，裁剪图像的边。启用 `Contain` 属性可以防止这种情况，但会导致两边出现空白。

<masa-example file="Examples.components.images.Contain"></masa-example>

#### 包含

`Gradient` 属性可用于对图像应用简单的渐变叠加。更复杂的渐变应该作为一个class写在内容插槽上。

<masa-example file="Examples.components.images.Gradients"></masa-example>

#### 高度

**MImage** 将自动增长到 `Src` 的大小，保持正确的纵横比。你可以用`Height`和`MaxHeight`的属性来限制。

<masa-example file="Examples.components.images.Height"></masa-example>

### 插槽

#### 占位符

**MImage** 有一个特别的 **PlaceholderContent** 插槽，在图像加载时占位显示。 注意：下面的例子有一个错误的 `Src`，因此图片不会加载，以方便你观察占位符。

<masa-example file="Examples.components.images.Placeholder"></masa-example>

### 其他

#### 栅格

您可以使用 **MImage** 来制作图片库。

<masa-example file="Examples.components.images.Grid"></masa-example>
