---
title: Switches（开关）
desc: "**MSwitch** 组件使用户能够在两个不同的值之间进行选择。 它们非常类似于一个切换或者开关，虽然视觉上不同于一个复选框。"
related:
  - /blazor/components/checkboxes
  - /blazor/components/forms
  - /blazor/components/radio
---

## 使用

最简单形式的 **MSwitch** 提供两个值之间的切换。

<switches-usage></switches-usage>

## 示例

### 属性

#### 颜色

**MSwitch** 可以使用 `Color` 属性设置颜色 颜色可以是[内置颜色](/blazor/styles-and-animations/colors)或自定义来着色。

<masa-example file="Examples.components.switches.Color"></masa-example>

#### 自定义真假值

**MSwitch** 将有一个类型化的值作为其 `Value`。

<masa-example file="Examples.components.switches.CustomState"></masa-example>

#### 扁平

您可以使用 `Flat` 属性渲染没有高度(z轴)的开关。

<masa-example file="Examples.components.switches.Flat"></masa-example>

#### 嵌入

您可以在 `Inset` 模式下进行切换渲染。

<masa-example file="Examples.components.switches.Inset"></masa-example>

#### 状态

**MSwitch** 可以有不同的状态，例如 `default`, `Disabled` , 以及 `Loading`.

<masa-example file="Examples.components.switches.State"></masa-example>

### 插槽

#### LabelContent

文本字段标签可以在 **LabelContent** 中定义。

<masa-example file="Examples.components.switches.Label"></masa-example>

### 其他

#### 自定义文本

**MSwitch** 可以自定义文本

<masa-example file="Examples.components.switches.CustomText"></masa-example>