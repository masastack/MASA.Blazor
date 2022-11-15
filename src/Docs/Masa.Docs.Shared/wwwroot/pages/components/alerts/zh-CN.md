---
title: Alerts（提示框）
desc: "该组件用于通过使用上下文类型，图标和颜色向用户传达重要信息。这些默认类型有4种变化：`Success`、`Info`、`Warning` 和 `Error`。默认图标有助于表示每种类型所描述的不同动作。也可以自定义提示框的许多部分，例如 `Border` 、`Icon` 和 `Color` 等，也可以自定义以适应绝大多数情况。"
related:
- /components/buttons
- /components/icons
- /components/dialogs
---

## 使用

最简单的警报形式是显示消息的平板纸。

<alerts-usage></alerts-usage>

## 解剖学

## 示例

### 属性

#### 边框

`Border` 属性支持将一个简单的边框添加到提示框的4个边。这个属性可以和例如： `Color`、`Dark`、`Type` 等这些属性一起来使用，共同为提示框组件呈现出独特的设计。

<masa-example file="Examples.alerts.Border"></masa-example>

#### 彩色边框

`ColoredBorder` 属性会移除警报背景，以突出 `Border` 属性 。如果设置了 `Type` 属性，它将使用类型的默认颜色。如果没有设置 `Color` 或 `Type` 属性
，颜色将默认为所应用的主题的反色（黑色代表浅色，白色/灰色代表深色）。

<masa-example file="Examples.alerts.ColoredBorder"></masa-example>

#### 紧凑

`Dense` 属性会降低提示框的高度来制造出一个简单且紧凑的风格。如果和 `Border` 属性一起使用，那么边界高度也会一起降低来保持风格的统一。

<masa-example file="Examples.alerts.Dense"></masa-example>

#### 可关闭

`Dismissible` 属性将会在提示框的尾部添加一个关闭按钮。点击此按钮将会将它的值设置为 false 且隐藏提示框。你也能够通过绑定 **@bind-Value** 的值为 true 来恢复提示框。关闭图标会自动应用
`aria-label`，可以通过修改 `CloseLabel` 属性或者改变本地设置的 close 的值来更改它。

<masa-example file="Examples.alerts.Dismissible"></masa-example>

#### 图标

`Icon` 属性允许你在提示框的开头添加图标。如果提供了 `Type` 属性，那么将会覆盖默认图标。 此外设置 `Icon` 属性为 false 时将会完全移除图标。

<masa-example file="Examples.alerts.Icon"></masa-example>

#### 轮廓

`Outlined` 属性将会反转提示框的风格，它会继承当前应用的 `Color` 并应用与文本和边框且将其背景透明化。

<masa-example file="Examples.alerts.Outlined"></masa-example>

#### 突出

`Prominent` 属性通过增加高度并向图标施加光晕来提供更明显的提示。当同时应用 `Prominent` 和 `Dense` 时，提示框将会呈现出普通的风格但是会应用 `Prominent` 属性图标特效。

<masa-example file="Examples.alerts.Prominent"></masa-example>

#### 文本

`Text` 属性是一个简单的提示框变量，它对所提供的 `Color` 属性使用不透明的背景。类似于其他样式的属性，可与 `Dense`, `Prominent`, `Outlined`  和  `Shaped`
等其他属性结合使用，以便创建出独特又个性化的 **Alert** 组件。

<masa-example file="Examples.alerts.Text"></masa-example>

#### 形状

`Shaped` 属性将在 *Alert** 的左上角和右下角加上较大的边界半径。与其他样式的道具类似，`Shaped` 可以与其他属性（如 `Dense`, `Prominent`, `Outlined` 和 `Text` ）组合，以便创建出独特又个性化的 *Alert** 组件。

<masa-example file="Examples.alerts.Shaped"></masa-example>

#### 过度

`Transition` 属性可让你向提示框应用一个过渡，该动画在隐藏和显示组件时可见。 你可以在 [内建过渡](/stylesandanimations/transitions) 浏览更多信息或者了解如何创建自己的`Transition` 属性样式的 **Alert** 组件。

<masa-example file="Examples.alerts.Transition"></masa-example>

#### Twitter

通过将 `Color`、`Dismissible`、`Border`、`Elevation`、`Icon` 和 `ColoredBorder` 属性组合在一起，你可以创建时髦的自定义 **Alert** 组件，比如下面这个 Twitter（推特）通知风格 **Alert** 组件。

<masa-example file="Examples.alerts.Twitter"></masa-example>

#### 类型

`Type` 属性提供 4 种默认的样式：`Success`, `Info`, `Warning`, 和 `Error`。每个样式都提供默认图标和颜色。

<masa-example file="Examples.alerts.Type"></masa-example>
