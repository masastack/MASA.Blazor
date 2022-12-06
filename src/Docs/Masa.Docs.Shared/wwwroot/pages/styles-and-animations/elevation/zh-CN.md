# Elevation（海拔）

该工具可以让你控制两平面之间沿 z 轴方向的相对深度，或者说距离。 总共有25个高度。 您可以通过使用 `elevation-{n}`类设置元素的海拔， 其中 `n` 是0-24之间与所需海拔对应的整数。

## 使用

**Elevation** 助手类允许您为任何元素分配一个自定义 `z-deep`。

<masa-example file="Examples.styles_and_animations.elevation.Basic"></masa-example>

## 示例

### 属性

#### 动态海拔

许多组件使用 elevable 混合并被赋予了 elevation 属性。对于不支持的组件，可以动态更改类

<masa-example file="Examples.styles_and_animations.elevation.Attributes"></masa-example>